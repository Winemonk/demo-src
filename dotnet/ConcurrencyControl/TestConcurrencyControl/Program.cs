namespace TestConcurrencyControl
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await TestSemaphoreSlim.TestSemaphoreSlim.Run();
            //await TestParallelForEach.TestParallelForEach.Run();
            //await TestTPLDataflow.TestTPLDataflow.Run();
            await TestTaskScheduler.TestTaskScheduler.Run();
        }
    }
}
