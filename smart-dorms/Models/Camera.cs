using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace smart_dorms.Models
{
    public partial class Camera
    {
        public Camera()
        {
            Obiecte = new HashSet<Obiecte>();
            Request = new HashSet<Request>();
            Useri = new HashSet<Useri>();
        }

        public int Id { get; set; }
        public int? IdTipCamera { get; set; }
        public int? NrPersoane { get; set; }
        public int? NrEtaj { get; set; }
        public int? IdCamin { get; set; }
        public int? NrCamera { get; set; }

        public virtual Camin IdCaminNavigation { get; set; }
        public virtual TipCamera IdTipCameraNavigation { get; set; }
        public virtual ICollection<Obiecte> Obiecte { get; set; }
        public virtual ICollection<Request> Request { get; set; }
        public virtual ICollection<Useri> Useri { get; set; }
    }
}
