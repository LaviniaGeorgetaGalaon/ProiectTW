using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace smart_dorms.Models
{
    public partial class Dosar
    {
        public int Id { get; set; }
        public int? IdTipDocumente { get; set; }
        public int? IdUser { get; set; }

        public virtual TipDocumente IdTipDocumenteNavigation { get; set; }
        public virtual Useri IdUserNavigation { get; set; }
    }
}
