using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceAgency__.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int ReceiptNumber { get; set; }
        public string FIO { get; set; }
        public DateTime DatePayment { get; set; }
        public int ClientId { get; set; }
    }
}
