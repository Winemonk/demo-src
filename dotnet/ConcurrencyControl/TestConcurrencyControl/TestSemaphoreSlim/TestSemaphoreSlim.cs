namespace TestConcurrencyControl.TestSemaphoreSlim
{
    public class TestSemaphoreSlim
    {
        private static SemaphoreSlim _semaphore;
        private static List<Task> _tasks;

        public static async Task Run()
        {
            _semaphore = new SemaphoreSlim(0, 3);
            Console.WriteLine($"初始化 semaphore ，现有 {_semaphore.CurrentCount} 个信号量");
            _tasks = new List<Task>();
            for (int i = 0; i <= 20; i++)
            {
                int cur = i + 1;
                Task task = Task.Run(() =>
                {
                    Console.WriteLine($"任务 {cur} 进入等待状态...");
                    int semaphoreCount;
                    _semaphore.Wait();
                    try
                    {
                        Console.WriteLine($"任务 {cur} 开始执行...");
                        Thread.Sleep(20000);
                    }
                    finally
                    {
                        semaphoreCount = _semaphore.Release();
                    }
                    Console.WriteLine($"任务 {cur} 已执行完成！");
                });
                _tasks.Add(task);
            }
            await Task.Delay(10000);
            Console.WriteLine("主线程调用 Release(3) 释放3个信号量");
            _semaphore.Release(3);

            await Task.Delay(5000);
            Console.WriteLine("调整最大信号量数量为 8...");
            SemaphoreSlim oldSemaphore;
            SemaphoreSlim newSemaphore = new SemaphoreSlim(5, 5);
            lock (_semaphore)
            {
                oldSemaphore = _semaphore;
                _semaphore = newSemaphore;
            }
            oldSemaphore.Dispose();
            Console.WriteLine("已释放旧的 semaphore 并替换为新的 semaphore ！");

            Task.WaitAll(_tasks.ToArray());
            Console.WriteLine("所有任务执行完成！");
        }

        /*
           初始化 semaphore ，现有 0 个信号量
           任务 12 准备进入 semaphore
           任务 16 准备进入 semaphore
           任务 15 准备进入 semaphore
           任务 8 准备进入 semaphore
           任务 9 准备进入 semaphore
           任务 11 准备进入 semaphore
           任务 10 准备进入 semaphore
           任务 13 准备进入 semaphore
           任务 14 准备进入 semaphore
           任务 17 准备进入 semaphore
           任务 18 准备进入 semaphore
           任务 19 准备进入 semaphore
           任务 20 准备进入 semaphore
           任务 21 准备进入 semaphore
           任务 22 准备进入 semaphore
           任务 23 准备进入 semaphore
           任务 24 准备进入 semaphore
           任务 25 准备进入 semaphore
           任务 26 准备进入 semaphore
           任务 27 准备进入 semaphore
           任务 28 准备进入 semaphore
           主线程调用 Release(3) 释放3个信号量
           任务 16 进入 semaphore 开始执行...
           任务 15 进入 semaphore 开始执行...
           任务 12 进入 semaphore 开始执行...
           任务 12 执行完成，释放 semaphore 一个信号量；释放前信号量：0
           任务 16 执行完成，释放 semaphore 一个信号量；释放前信号量：1
           任务 15 执行完成，释放 semaphore 一个信号量；释放前信号量：2
           任务 9 进入 semaphore 开始执行...
           任务 11 进入 semaphore 开始执行...
           任务 8 进入 semaphore 开始执行...
           任务 8 执行完成，释放 semaphore 一个信号量；释放前信号量：0
           任务 9 执行完成，释放 semaphore 一个信号量；释放前信号量：1
           任务 11 执行完成，释放 semaphore 一个信号量；释放前信号量：2
           任务 10 进入 semaphore 开始执行...
           任务 14 进入 semaphore 开始执行...
           任务 13 进入 semaphore 开始执行...
           任务 14 执行完成，释放 semaphore 一个信号量；释放前信号量：0
           任务 10 执行完成，释放 semaphore 一个信号量；释放前信号量：1
           任务 13 执行完成，释放 semaphore 一个信号量；释放前信号量：2
           任务 18 进入 semaphore 开始执行...
           任务 17 进入 semaphore 开始执行...
           任务 19 进入 semaphore 开始执行...
           任务 19 执行完成，释放 semaphore 一个信号量；释放前信号量：0
           任务 18 执行完成，释放 semaphore 一个信号量；释放前信号量：1
           任务 22 进入 semaphore 开始执行...
           任务 17 执行完成，释放 semaphore 一个信号量；释放前信号量：2
           任务 21 进入 semaphore 开始执行...
           任务 20 进入 semaphore 开始执行...
           任务 22 执行完成，释放 semaphore 一个信号量；释放前信号量：0
           任务 21 执行完成，释放 semaphore 一个信号量；释放前信号量：1
           任务 24 进入 semaphore 开始执行...
           任务 23 进入 semaphore 开始执行...
           任务 25 进入 semaphore 开始执行...
           任务 20 执行完成，释放 semaphore 一个信号量；释放前信号量：2
           任务 26 进入 semaphore 开始执行...
           任务 25 执行完成，释放 semaphore 一个信号量；释放前信号量：0
           任务 23 执行完成，释放 semaphore 一个信号量；释放前信号量：0
           任务 27 进入 semaphore 开始执行...
           任务 28 进入 semaphore 开始执行...
           任务 24 执行完成，释放 semaphore 一个信号量；释放前信号量：0
           任务 28 执行完成，释放 semaphore 一个信号量；释放前信号量：0
           任务 26 执行完成，释放 semaphore 一个信号量；释放前信号量：2
           任务 27 执行完成，释放 semaphore 一个信号量；释放前信号量：1
           任务全部完成！
         */
    }
}
