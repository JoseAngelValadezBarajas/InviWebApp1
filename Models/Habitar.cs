using System;
using System.Collections.Generic;

namespace inviWebApp.Models
{
    public partial class Habitar
    {
        public int Id { get; set; }
        public string Curp { get; set; }
        public int? Vivienda { get; set; }

        public virtual Propietario CurpNavigation { get; set; } 
        public virtual Viviendum ViviendaNavigation { get; set; }
    }
}
