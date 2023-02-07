using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceAgency__.Models
{
    public class PersonalInfo
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public DateTime BirthDate { get; set; }
        public int PassportNumber { get; set; }
        public int PassportSeries { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
        public int ClientId { get; set; }
    }
}
