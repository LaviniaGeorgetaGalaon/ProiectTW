using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace smart_dorms.Models
{
    public partial class Obiecte
    {
        public int? IdTipObiect { get; set; }
        public int? Cantitate { get; set; }
        public int? IdCamera { get; set; }
        public int Id { get; set; }

        public virtual Camera IdCameraNavigation { get; set; }
        public virtual TipObiect IdTipObiectNavigation { get; set; }
    }
}
