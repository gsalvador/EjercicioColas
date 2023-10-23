using System;
using System.Collections.Generic;

namespace EjercicioColas.Data
{
    public partial class Cliente
    {
        public Cliente()
        {
            Colas = new HashSet<Cola>();
        }

        public string Id { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Cola> Colas { get; set; }
    }
}
