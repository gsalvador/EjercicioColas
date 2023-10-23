using EjercicioColas;
using EjercicioColas.CRUD;

namespace PeriodicBackgroundTaskSample;

class SimpleService
{
    private readonly ILogger<SimpleService> _logger;
    private readonly ManejoColasContext _context;

    public SimpleService(ILogger<SimpleService> logger, ManejoColasContext colasContext)
    {
        _logger = logger;
        _context = colasContext;
    }

    public async Task DoSomethingAsync()
    {
        //await Task.Delay(100);
        //_logger.LogInformation(
        //    "Sample Service did something.");
        DeleteOps dOps = new DeleteOps(_context, (ILogger<DeleteOps>)_logger);
        ReadOps rOps = new ReadOps((ILogger<EjercicioColas.Pages.IndexModel>)_logger, _context);
        int swich = 0;
        Task task = Task.Run(() =>
        {
            var item = rOps.GetNextItemQueue(2+(swich++%2));
            dOps.DelQueue(item.IdCliente);
            dOps.DelClient(item.IdCliente);
        });

        await task;
        _logger.LogInformation("Se proceso al siguiente cliente de la cola");
    }
}
