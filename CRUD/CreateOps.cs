using EjercicioColas.Data;
using EjercicioColas.Pages;

namespace EjercicioColas.CRUD
{
    public class CreateOps
    {
        private readonly ManejoColasContext _colasContext;
        private readonly ILogger<IndexModel> _logger;

        public CreateOps(ILogger<IndexModel> logger, ManejoColasContext colasContext)
        {
            _logger = logger;
            _colasContext = colasContext;
        }

        public void InsertClient(string id, string name)
        {
            Cliente cliente = new Cliente() { Id = id, Nombre = name };
            _colasContext.Clientes.Add(cliente);
            _colasContext.SaveChanges();
        }

        public void InsertQueue(string idClient, int queueKind)
        {
            Cola cola = new Cola() { IdCliente = idClient, IdTipoCola = queueKind };
            _colasContext.Colas.Add(cola);
            _colasContext.SaveChanges();
        }
    }
}
