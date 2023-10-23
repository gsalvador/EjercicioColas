using EjercicioColas.Data;
using EjercicioColas.Pages;

namespace EjercicioColas.CRUD
{
    public class ReadOps
    {
        private readonly ManejoColasContext _colasContext;
        private readonly ILogger<IndexModel> _logger;
        public ReadOps(ILogger<IndexModel> logger, ManejoColasContext colasContext)         
        {
            _logger = logger;
            _colasContext = colasContext;
        }

        public Cliente GetClients(string idClient)
        {
            return _colasContext.Clientes.Where(c => c.Id.Contains(idClient)).First<Cliente>();
        }

        public List<Cola> GetQueue() 
        {
            return _colasContext.Colas.ToList<Cola>();
        }

        public Cola GetNextItemQueue(int type)
        {
            return _colasContext.Colas.Where(q => q.IdTipoCola == type).First();
        }
    }
}
