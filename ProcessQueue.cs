using EjercicioColas.CRUD;
using EjercicioColas.Data;

namespace EjercicioColas
{
    public class ProcessQueue
    {
        private readonly ILogger<ProcessQueue> _logger;
        private readonly ManejoColasContext _context;
        private int _iter = 1;
        public ProcessQueue(ILogger<ProcessQueue> logger, ManejoColasContext colasContext)
        {
            _logger = logger;
            _context = colasContext;
        }

        public async Task DoSomethingAsync()
        {
            //await Task.Delay(100);
            //_logger.LogInformation(
            //    "Sample Service did something.");
            
            Task task = Task.Run(() =>
            {
                DeleteOps delete = new DeleteOps(_context, (ILogger<DeleteOps>)_logger);
                ReadOps read = new ReadOps((ILogger<Pages.IndexModel>)_logger, _context);
                Cola lastItem = read.GetNextItemQueue(_iter%2+1);
                delete.DelQueue(lastItem.IdCliente);
                _iter++;

                _logger.LogInformation($"Element {lastItem.IdCliente} has been processed!!");
            });
        }
    }
}
