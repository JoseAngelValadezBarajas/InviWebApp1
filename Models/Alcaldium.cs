using System;
using System.Collections.Generic;

namespace inviWebApp.Models
{
    public partial class Alcaldium
    {
        public Alcaldium()
        {
            Vivienda = new HashSet<Viviendum>();
        }

        public int Clave { get; set; }
        public string Nombre { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public int Poblacion { get; set; }

        public virtual ICollection<Viviendum> Vivienda { get; set; }
    }
}
