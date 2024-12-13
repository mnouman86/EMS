using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.CoreArea
{
    public class CreateCoreAreaDTO
    {
        // public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Type { get; set; }
        public string? ImagePath { get; set; }

        public int? CreatedBy { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedAt { get; set; }

    }
}
