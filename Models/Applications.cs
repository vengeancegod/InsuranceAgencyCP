using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceAgency__.Models
{
    public class Applications
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public int PassportNumber { get; set; }
        public int PassportSeries { get; set; }
        public string ContractValidity { get; set; }
        public string InsuranceType { get; set; }
        public int InsuranceAmount { get; set; }
        public string ApplicationStatus { get; set; }
        public int ClientId { get; set; }
    }
}
