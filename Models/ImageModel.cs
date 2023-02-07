   using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceAgency__.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string ImageTitle { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}