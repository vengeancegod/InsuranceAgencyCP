using System;
using Microsoft.OData.Edm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceAgency__.Models
{
    public class UserSystem
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public DateTime DateRegistrator { get; set; }
    }
}
