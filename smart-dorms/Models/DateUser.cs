using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace smart_dorms.Models
{
    public partial class DateUser
    {
        public DateUser()
        {
            Useri = new HashSet<Useri>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }

        public virtual ICollection<Useri> Useri { get; set; }
    }
}
