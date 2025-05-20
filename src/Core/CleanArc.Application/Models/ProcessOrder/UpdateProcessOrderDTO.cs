using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.ProcessOrder
{
    public class UpdateProcessOrderDTO
    {
        public int Id { get; set; } // IDENTITY(1,1) NOT NULL
        public int? CultureId { get; set; }
        public int? OrderStatusEnumId { get; set; }
        public int? UpdatedBy { get; set; } 
    }
}
