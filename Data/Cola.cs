using System;
using System.Collections.Generic;

namespace EjercicioColas.Data
{
    public partial class Cola
    {
        public int Id { get; set; }
        public int? IdTipoCola { get; set; }
        public string? IdCliente { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }
        public virtual TipoCola? IdTipoColaNavigation { get; set; }
    }
}
