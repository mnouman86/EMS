using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.BusinessType
{
    public class UpdateBusinessTypeDTO
    {
        public int? ID { get; set; }
        public bool? Company { get; set; }
        public bool? IndividualPerson { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
        // public int? CreatedBy { get; set; }
        // public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
       // public DateTime? UpdatedAt { get; set; }
    }
}
