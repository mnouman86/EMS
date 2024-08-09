using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Advertisement
{
    public class UpdateAdvertisementDTO
    {
        public int ID { get; set; }
        public int? PageID { get; set; }
        public int? PlaceID { get; set; }
        public string? ImageTitle { get; set; }
        public string? ImagePath { get; set; }
        public string? Url { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsShow { get; set; }
        public int? UpdatedBy { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }



    }
}
