using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace smart_dorms.Models
{
    public partial class Camin
    {
        public Camin()
        {
            Camera = new HashSet<Camera>();
        }

        public int Id { get; set; }
        public string NameCamin { get; set; }
        public string LocatieCamin { get; set; }

        public virtual ICollection<Camera> Camera { get; set; }
        public Camera camera;
        public String tipCamera;
    }
}
