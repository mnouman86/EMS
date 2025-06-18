using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.CurrencyRate
{
    public class CurrencyRate
    {
        public string CurrencyCode { get; set; }
        public decimal Rate { get; set; }
        public string DisplaySymbol { get; set; }
        public string CountryFlagSymbol { get; set; }
    }
}
