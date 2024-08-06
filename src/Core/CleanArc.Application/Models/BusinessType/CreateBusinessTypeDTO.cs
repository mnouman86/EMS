using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.BusinessType
{
    public class CreateBusinessTypeDTO
    {
        //public int? ID { get; set; }
        //public bool? Company { get; set; }

        public string? BusinessTypeName { get; set; }
        //public bool? IndividualPerson { get; set; }
        public int? CreatedBy { get; set; }
       // public DateTime? CreatedAt { get; set; }
       // public int? UpdatedBy { get; set; }
       // public DateTime? UpdatedAt { get; set; }
    }
}
