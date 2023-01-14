using System;
using System.Collections.Generic;

namespace inviWebApp.Models
{
    public partial class Propietario
    {
        public Propietario()
        {
            Habitars = new HashSet<Habitar>();
        }

        public string Curp { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellidopaterno { get; set; } = null!;
        public string Apellidomaterno { get; set; } = null!;
        public string Sexo { get; set; } = null!;
        public int Edad { get; set; }

        public virtual ICollection<Habitar> Habitars { get; set; }
    }
}
