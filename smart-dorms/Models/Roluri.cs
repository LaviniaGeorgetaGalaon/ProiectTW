using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace smart_dorms.Models
{
    public partial class Roluri
    {
        public Roluri()
        {
            Permisiuni = new HashSet<Permisiuni>();
            Useri = new HashSet<Useri>();
        }

        public int Id { get; set; }
        public string Nume { get; set; }
        public string Descriere { get; set; }

        public virtual ICollection<Permisiuni> Permisiuni { get; set; }
        public virtual ICollection<Useri> Useri { get; set; }
    }
}
