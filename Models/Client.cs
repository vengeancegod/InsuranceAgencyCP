using System;
using Microsoft.OData.Edm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceAgency__.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string TypeClient { get; set; }
        public string FIO { get; set; }
        public int PassportSeries { get; set; }
        public int PassportNumber { get; set; }
        public DateTime BirthDate { get; set; } 
        public string PlaceOfResidence { get; set; }
    }
}
