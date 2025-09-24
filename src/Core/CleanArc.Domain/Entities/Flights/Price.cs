using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.Flights
{
    public class Price
    {
        public string CurrencyCode { get; set; } = default!;
        public decimal TotalAmount { get; set; }
        public decimal PricePerAdult { get; set; }
        public decimal PricePerChild { get; set; }
        public decimal PricePerInfant { get; set; }
        public bool IsRefundable { get; set; }
    }
}
