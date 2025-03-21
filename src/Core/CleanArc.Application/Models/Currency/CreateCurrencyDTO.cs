using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Activities
{
    public class CreateCurrencyDTO
    {
        //public int Id { get; set; }
        public string? Name { get; set; }
        public string? CurrencyCode { get; set; }
        public decimal? Rate { get; set; }
        public int? CultureId { get; set; }
        //public Decimal? PerGroupPrice { get; set; }
        public int? CreatedBy { get; set; }
        

    }
}
