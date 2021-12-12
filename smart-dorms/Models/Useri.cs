using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using smart_dorms.Utils;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace smart_dorms.Models
{
    public partial class Useri
    {
        public Useri()
        {
            Dosar = new HashSet<Dosar>();
            Request = new HashSet<Request>();
        }

        public int Id { get; set; }
        public int? IdRol { get; set; }
        public bool? Gdpr { get; set; }
        public int? IdCamera { get; set; }
        public int? IdDateUser { get; set; }
        public bool IsAdmin => IdRol == (int)UserRole.Administrator;
        public string FullName => $"{IdDateUserNavigation.Nume} {IdDateUserNavigation.Prenume}".Trim();

        [JsonIgnore]
        public virtual Camera IdCameraNavigation { get; set; }
        [JsonIgnore]
        public virtual DateUser IdDateUserNavigation { get; set; }
        [JsonIgnore]
        public virtual Roluri IdRolNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<Dosar> Dosar { get; set; }
        [JsonIgnore]
        public virtual ICollection<Request> Request { get; set; }
    }
}
