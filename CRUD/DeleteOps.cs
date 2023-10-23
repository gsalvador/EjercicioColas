namespace EjercicioColas.CRUD
{
    public class DeleteOps
    {
        private readonly ManejoColasContext _colasContext;
        private readonly ILogger<DeleteOps> _logger;

        public DeleteOps(ManejoColasContext colasContext, ILogger<DeleteOps> logger)
        {
            _colasContext = colasContext;
            _logger = logger;
        }

        public void DelClient(string idCliente)
        {
            var itemToRemove = _colasContext.Clientes.SingleOrDefault(c => c.Id == idCliente);
            if (itemToRemove != null)
            {
                _colasContext.Clientes.Remove(itemToRemove);
                _colasContext.SaveChanges();
            }
        }

        public void DelQueue(string idCliente)
        {
            var itemToRemove = _colasContext.Colas.SingleOrDefault(q => q.IdCliente == idCliente);
            if (itemToRemove != null)
            {
                _colasContext.Colas.Remove(itemToRemove);
                _colasContext.SaveChanges();
            }
        }
    }
}
