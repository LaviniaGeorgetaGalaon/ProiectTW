using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace smart_dorms.Models
{
    public partial class Request
    {
        public int? IdTipCerere { get; set; }
        public int? IdCamera { get; set; }
        public int? IdTipStatus { get; set; }
        public int? Prioritate { get; set; }
        public string Detalii { get; set; }
        public int? IdUser { get; set; }
        public int Id { get; set; }

        public virtual Camera IdCameraNavigation { get; set; }
        public virtual TipCerere IdTipCerereNavigation { get; set; }
        public virtual TipStatus IdTipStatusNavigation { get; set; }
        public virtual Useri IdUserNavigation { get; set; }
    }
}
