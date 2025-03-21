using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Advertisement
{
    public class CreateAdvertisementDTO
    {
        public int? AdsPageLookUpId { get; set; }
        public int? AdsPlaceLookUpId { get; set; }
        public string? ImageTitle { get; set; }
        //public List<string>? ImagePaths { get; set; }
        public string? Url { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? CultureId { get; set; }



    }
}
