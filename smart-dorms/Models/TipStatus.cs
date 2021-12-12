using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace smart_dorms.Models
{
    public partial class TipStatus
    {
        public TipStatus()
        {
            Request = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Nume { get; set; }
        public string Descriere { get; set; }

        public virtual ICollection<Request> Request { get; set; }
    }
}
