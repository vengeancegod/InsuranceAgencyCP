using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceAgency__.Models
{
    public class PaymentDocument
    {
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public string BeneficiaryBankName { get; set; }
        public DateTime InvoicingDate { get; set; }
        public string NameOfService { get; set; }
        public int ServiceAmount {get; set;}
        public string FIOPolicyholder { get; set; }
        public string PaymentMethod { get; set; }
        public int ClientId { get; set; }
    }
}
