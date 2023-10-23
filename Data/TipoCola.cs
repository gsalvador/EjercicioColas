using System;
using System.Collections.Generic;

namespace EjercicioColas.Data
{
    public partial class TipoCola
    {
        public TipoCola()
        {
            Colas = new HashSet<Cola>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public int Tiempo { get; set; }

        public virtual ICollection<Cola> Colas { get; set; }
    }
}
