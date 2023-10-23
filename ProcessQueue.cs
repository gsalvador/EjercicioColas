namespace EjercicioColas
{
    public class ProcessQueue
    {
        private readonly ILogger<ProcessQueue> _logger;

        public ProcessQueue(ILogger<ProcessQueue> logger)
        {
            _logger = logger;
        }

        public async Task DoSomethingAsync()
        {
            await Task.Delay(100);
            _logger.LogInformation(
                "Sample Service did something.");
        }
    }
}
