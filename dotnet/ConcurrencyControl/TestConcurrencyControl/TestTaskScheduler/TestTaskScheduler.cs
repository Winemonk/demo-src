
namespace TestConcurrencyControl.TestTaskScheduler
{
    public class LimitedConcurrencyLevelTaskScheduler : TaskScheduler
    {
        private readonly int _maxDegreeOfParallelism;
        private readonly Queue<Task> _tasks = new Queue<Task>();
        private int _runningTasks = 0;
        private readonly object _lock = new object();

        public LimitedConcurrencyLevelTaskScheduler(int maxDegreeOfParallelism)
        {
            _maxDegreeOfParallelism = maxDegreeOfParallelism;
        }

        protected override IEnumerable<Task>? GetScheduledTasks()
        {
            lock (_lock)
            {
                return _tasks.ToArray();
            }
        }

        protected override void QueueTask(Task task)
        {
            lock (_lock)
            {
                _tasks.Enqueue(task);
                TryExecuteTaskInQueue();
            }
        }

        private void TryExecuteTaskInQueue()
        {
            lock (_lock)
            {
                if (_runningTasks < _maxDegreeOfParallelism && _tasks.Count > 0)
                {
                    Task task = _tasks.Dequeue();
                    _runningTasks++;
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        TryExecuteTask(task);
                        TaskCompleted();
                    });
                }
            }
        }

        private void TaskCompleted()
        {
            lock (_lock)
            {
                _runningTasks--;
                TryExecuteTaskInQueue();
            }
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            if (taskWasPreviouslyQueued)
            {
                return false;
            }
            return TryExecuteTask(task);
        }
    }

    public class DynamicLimitedConcurrencyLevelTaskScheduler : TaskScheduler
    {
        private readonly LinkedList<Task> _queuedTasks = new LinkedList<Task>();
        private readonly List<Task> _runningTasks = new List<Task>();
        private readonly object _lock = new object();
        private int _maxConcurrentUploads;

        public DynamicLimitedConcurrencyLevelTaskScheduler(int maxConcurrentUploads)
        {
            _maxConcurrentUploads = maxConcurrentUploads;
        }

        public void PrioritizeTask(Task task)
        {
            lock (_lock)
            {
                if (_queuedTasks.Contains(task))
                {
                    _queuedTasks.Remove(task);
                    _queuedTasks.AddFirst(task);
                }
            }
        }

        public void ChageMaxConcurrentUploads(int maxConcurrentUploads)
        {
            lock (_lock)
            {
                var previous = _maxConcurrentUploads;
                _maxConcurrentUploads = maxConcurrentUploads;
                AdjustConcurrency(previous);
            }
        }

        private void AdjustConcurrency(int previous)
        {
            lock (_lock)
            {
                if (previous == _maxConcurrentUploads)
                {
                    return;
                }
                while (_runningTasks.Count < _maxConcurrentUploads && _queuedTasks.Count > 0)
                {
                    var task = _queuedTasks.First.Value;
                    _queuedTasks.RemoveFirst();
                    StartTask(task);
                }
                if (_maxConcurrentUploads < previous)
                {
                    var excess = _runningTasks.Count - _maxConcurrentUploads;
                    if (excess <= 0) return;

                    foreach (var task in _runningTasks
                        .Where(t => t.AsyncState is CancellationTokenSource cts && !cts.IsCancellationRequested)
                        .Take(excess))
                    {
                        var cts = (CancellationTokenSource)task.AsyncState!;
                        cts.Cancel();
                    }
                }
            }
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            lock (_lock)
            {
                return _runningTasks.Concat(_queuedTasks).ToList();
            }
        }

        protected override void QueueTask(Task task)
        {
            lock (_lock)
            {
                if (_runningTasks.Count < _maxConcurrentUploads)
                {
                    StartTask(task);
                }
                else
                {
                    _queuedTasks.AddLast(task);
                }
            }
        }

        private void StartTask(Task task)
        {
            _runningTasks.Add(task);
            ThreadPool.QueueUserWorkItem(async _ =>
            {
                try
                {
                    if (base.TryExecuteTask(task))
                    {
                        // 循环解包嵌套Task
                        while (task.GetType().IsGenericType
                               && typeof(Task).IsAssignableFrom(task.GetType().GetGenericArguments()[0]))
                        {
                            // 同步获取内部Task（避免async/await的传染）
                            task = ((dynamic)task).Result;
                        }

                        // 异步等待最终的非嵌套Task
                        await task.ConfigureAwait(false);
                        //if (task is Task<Task> taskOfTask)
                        //{
                        //    var innerTask = await taskOfTask;
                        //    await innerTask;
                        //}
                        //else await task;
                    }
                }
                finally
                {
                    lock (_lock)
                    {
                        _runningTasks.Remove(task);
                        AdjustConcurrency(_runningTasks.Count);
                    }
                }
            });
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            if (taskWasPreviouslyQueued)
            {
                return false;
            }
            return TryExecuteTask(task);
        }
    }

    public class TestTaskScheduler
    {
        public static async Task Run()
        {
            DynamicLimitedConcurrencyLevelTaskScheduler taskScheduler = new DynamicLimitedConcurrencyLevelTaskScheduler(3);
            //LimitedConcurrencyLevelTaskScheduler taskScheduler = new LimitedConcurrencyLevelTaskScheduler(3);
            Console.WriteLine("初始化任务工厂...");
            TaskFactory taskFactory = new TaskFactory(taskScheduler);
            List<CancellationTokenSource> cancellationTokenSources = new List<CancellationTokenSource>();
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 20; i++)
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                cancellationTokenSources.Add(cts);
                Console.WriteLine($"创建任务 {i}，进入队列等待执行...");
                int index = i;
                Task task = taskFactory.StartNew(async () =>
                {
                    try
                    {
                        Console.WriteLine($"任务 {Task.CurrentId} 开始执行...");
                        await Task.Delay(5000, cts.Token);
                        Console.WriteLine($"任务 {Task.CurrentId} 执行完成！");
                    }
                    catch (TaskCanceledException ex)
                    {
                        
                    }
                }, cts.Token);
                //task.ContinueWith(t =>
                //{
                //    Task task = taskFactory.StartNew(async () =>
                //    {
                //        try
                //        {
                //            Console.WriteLine($"重新开始：任务 {Task.CurrentId} 重新开始执行...");
                //            await Task.Delay(5000);
                //            Console.WriteLine($"重新开始：任务 {Task.CurrentId} 执行完成！");
                //        }
                //        catch (TaskCanceledException ex)
                //        {
                //            Console.WriteLine($"重新开始：任务 {Task.CurrentId} 取消执行！");
                //        }
                //    }, new CancellationTokenSource().Token);
                //    Console.WriteLine($"重新开始：任务 {task.Id} 已进入队列...");
                //    taskScheduler.PrioritizeTask(task);
                //}, TaskContinuationOptions.OnlyOnCanceled);
                tasks.Add(task);
            }
            for (int i = 0; i < 10; i++)
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                Task task = taskFactory.StartNew(async () =>
                {
                    try
                    {
                        Console.WriteLine($"第二批：任务 {Task.CurrentId} 开始执行...");
                        await Task.Delay(5000, cts.Token);
                        Console.WriteLine($"第二批：任务 {Task.CurrentId} 执行完成！");
                    }
                    catch (TaskCanceledException ex)
                    {
                        Console.WriteLine($"第二批：任务 {Task.CurrentId} 取消执行！");
                    }
                }, cts.Token);
                Console.WriteLine($"第二批：任务 {task.Id} 已进入队列...");
            }


            await Task.Delay(3000);
            Console.WriteLine("修改最大并发数为 5...");
            taskScheduler.ChageMaxConcurrentUploads(5);
            await Task.Delay(10000);
            Console.WriteLine("取消任务...");
            cancellationTokenSources.ForEach(cts => cts.Cancel());
            await Task.Delay(50000);
            // Task.WaitAll(tasks.ToArray());

            Console.WriteLine("所有任务执行完成！");
        }

        /*
           初始化任务工厂...
           创建任务 0，进入队列等待执行...
           创建任务 1，进入队列等待执行...
           创建任务 2，进入队列等待执行...
           创建任务 3，进入队列等待执行...
           创建任务 4，进入队列等待执行...
           创建任务 5，进入队列等待执行...
           创建任务 6，进入队列等待执行...
           任务 0 开始执行...
           任务 2 开始执行...
           任务 1 开始执行...
           创建任务 7，进入队列等待执行...
           创建任务 8，进入队列等待执行...
           创建任务 9，进入队列等待执行...
           创建任务 10，进入队列等待执行...
           创建任务 11，进入队列等待执行...
           创建任务 12，进入队列等待执行...
           创建任务 13，进入队列等待执行...
           创建任务 14，进入队列等待执行...
           创建任务 15，进入队列等待执行...
           创建任务 16，进入队列等待执行...
           创建任务 17，进入队列等待执行...
           创建任务 18，进入队列等待执行...
           创建任务 19，进入队列等待执行...
           任务 0 执行完成！
           任务 1 执行完成！
           任务 4 开始执行...
           任务 3 开始执行...
           任务 2 执行完成！
           任务 5 开始执行...
           任务 4 执行完成！
           任务 3 执行完成！
           任务 6 开始执行...
           任务 7 开始执行...
           任务 5 执行完成！
           任务 8 开始执行...
           任务 6 执行完成！
           任务 7 执行完成！
           任务 9 开始执行...
           任务 10 开始执行...
           任务 8 执行完成！
           任务 11 开始执行...
           任务 10 执行完成！
           任务 9 执行完成！
           任务 12 开始执行...
           任务 13 开始执行...
           任务 11 执行完成！
           任务 14 开始执行...
           任务 12 执行完成！
           任务 13 执行完成！
           任务 16 开始执行...
           任务 15 开始执行...
           任务 14 执行完成！
           任务 17 开始执行...
           任务 15 执行完成！
           任务 16 执行完成！
           任务 18 开始执行...
           任务 19 开始执行...
           任务 17 执行完成！
           任务 19 执行完成！
           任务 18 执行完成！
           所有任务执行完成！
         */
    }
}
