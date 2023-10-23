using EjercicioColas.CRUD;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EjercicioColas.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ManejoColasContext _context;
        public string m_clients = string.Empty;

        public IndexModel(ILogger<IndexModel> logger, ManejoColasContext colasContext)
        {
            _logger = logger;
            _context = colasContext;
        }

        public void OnPost()
        {
            string? clientId = Request.Form["ClientId"];
            string? clientName = Request.Form["ClientName"];

            CreateOps cOps = new CreateOps(_logger, _context);
            cOps.InsertClient(clientId, clientName);

            // Check which queue is shortest for the client
            ReadOps rOps = new ReadOps(_logger, _context);
            var queue = rOps.GetQueue();

            int queueKind2 = queue.Count(q => q.IdTipoCola == 2);
            int queueKind3 = queue.Count(q => q.IdTipoCola == 3);

            if (queueKind2 * 2 > queueKind3 * 3)
            {
                // Assign to queue of type 3
                cOps.InsertQueue(clientId, 3);

            }
            else if (queueKind2 * 2 <= queueKind3 * 3)
            {
                // Assign to queue of type 2
                cOps.InsertQueue(clientId, 2);
            }
        }
    }
}