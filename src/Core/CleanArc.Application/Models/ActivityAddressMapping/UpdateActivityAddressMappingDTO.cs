using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.ActivityAddressMapping
{
    public class UpdateActivityAddressMappingDTO
    {


        //public int ID { get; set; }
        //public int? ActivityID { get; set; }
        //public string Title { get; set; }
        //public int? UpdatedBy { get; set; }
        // public DateTime? UpdatedAt { get; set; }
        public int? ID { get; set; }
        public int? ActivityID { get; set; }
        public string? ImagePath { get; set; }
        public string? ImageTitle { get; set; }
        public bool? IsMain { get; set; }
        //public int CategoryID { get; set; }
        //public int BusinessID { get; set; }
        //public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }
        //public int CreatedBy { get; set; }
        //public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public int? Message { get; set; }
    }
}
