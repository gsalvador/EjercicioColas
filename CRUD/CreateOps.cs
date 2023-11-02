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
            try
            {
                Cliente cliente = new Cliente() { Id = id, Nombre = name };
                _colasContext.Clientes.Add(cliente);
                _colasContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public void InsertQueue(string idClient, int queueKind)
        {
            try
            {
                Cola cola = new Cola() { IdCliente = idClient, IdTipoCola = queueKind };
                _colasContext.Colas.Add(cola);
                _colasContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
