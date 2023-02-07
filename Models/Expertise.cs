using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceAgency__.Models
{
    public class Expertise
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public int PassportNumber { get; set; }
        public int PassportSeries { get; set; }
        public string InsuranceType { get; set; }
        public virtual ImageModel Image { get; set; }
    }
}
