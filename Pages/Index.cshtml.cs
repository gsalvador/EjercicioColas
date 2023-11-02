using EjercicioColas.CRUD;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using System.ComponentModel.DataAnnotations;

namespace EjercicioColas.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IToastNotification _toastNotification;

        private readonly ManejoColasContext _context;
        public string m_clients = string.Empty;

        public IndexModel(ILogger<IndexModel> logger,
            ManejoColasContext colasContext,
            IToastNotification toastNotification)
        {
            _logger = logger;
            _context = colasContext;
            _toastNotification = toastNotification;
        }
        
        public void OnPost()
        {
            try
            {
                string? clientId = Request.Form["ClientId"];
                string? clientName = Request.Form["ClientName"];

                if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientName))
                {
                    throw new ValidationException("Los campos Cliente Id y Nombre son obligatorios");
                }

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
                    cOps.InsertQueue(clientId, 2);
                    _toastNotification.AddSuccessToastMessage("El usuario fue asignado a la cola 3");

                }
                else if (queueKind2 * 2 <= queueKind3 * 3)
                {
                    // Assign to queue of type 2
                    cOps.InsertQueue(clientId, 1);
                    _toastNotification.AddSuccessToastMessage("El usuario fue asignado a la cola 2");
                }
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage($"Se produjo un error al asignar una cola: {ex.Message}");
                _logger.LogError($"Se produjo un error al asignar una cola: {ex.Message}");
            }
        }
    }
}