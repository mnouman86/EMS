using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Activities
{
    public class UpdateCurrencyDTO
    {

        public int ID { get; set; }
        public string? Name { get; set; }
        public int? CurrencyCode { get; set; }
        public int? Rate { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
        public int? CultureId { get; set; }
        public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedAt { get; set; }
    }
}
