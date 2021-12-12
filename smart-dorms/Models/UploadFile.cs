using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace smart_dorms.Models
{
    public class UploadFile
    {
        public string ImageCaption { set; get; }
        public string ImageDescription { set; get; }
        public IFormFile MyImage { set; get; }
    }
}
