using System;
using System.Collections.Generic;

namespace inviWebApp.Models
{
    public partial class Viviendum
    {
        public Viviendum()
        {
            Habitars = new HashSet<Habitar>();
        }

        public int Id { get; set; }
        public string? Dirreccion { get; set; }
        public string? Tipo { get; set; }
        public string? Alcaldia { get; set; }

        public virtual Alcaldium? AlcaldiaNavigation { get; set; }
        public virtual ICollection<Habitar> Habitars { get; set; }
    }
}
