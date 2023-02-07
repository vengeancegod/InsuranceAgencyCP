﻿using System;
using Microsoft.OData.Edm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceAgency__.Models
{
    public class InsuranceAgent
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public double Wage { get; set; }
    }
}
