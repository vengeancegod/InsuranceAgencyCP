using System;
using Microsoft.OData.Edm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceAgency__.Models
{
    public class Treaty
    {
        public int Id { get; set; }
        public string TypeTreaty { get; set; }
        public string TypeOfInsuranceTreaty { get; set; }
        public int PolicySeries { get; set; }
        public int PolicyNumber { get; set; }
        public double InsurancePremium { get; set; }
        public double InsuranceSum { get; set; }
        public DateTime DateOfPaymentTreaty { get; set; }
        public string TreatyCurrency { get; set; }
        public string StateOfTreaty { get; set; }
        public int IdClient { get; set; }
    }
}
