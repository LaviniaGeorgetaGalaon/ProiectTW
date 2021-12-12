using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace smart_dorms.Models
{
    public partial class TipObiect
    {
        public TipObiect()
        {
            Obiecte = new HashSet<Obiecte>();
        }

        public int Id { get; set; }
        public string Nume { get; set; }
        public string Descriere { get; set; }
        public int? Cantitate { get; set; }

        public virtual ICollection<Obiecte> Obiecte { get; set; }
    }
}
