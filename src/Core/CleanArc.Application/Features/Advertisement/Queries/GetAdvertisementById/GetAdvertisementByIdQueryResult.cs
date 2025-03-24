using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Advertisement.Queries.GetAdvertisementById
{
    public class GetAdvertisementByIdQueryResult
    //(int Id, string Name, string Description, bool IsDeleted, bool IsActive, int CreatedBy, DateTime CreatedAt, int UpdatedBy, DateTime UpdatedAt);
    {

        public int Id { get; set; }
        public int? AdsPageLookUpId { get; set; }
        public int? AdsPlaceLookUpId { get; set; }
        public string? ImageTitle { get; set; }
        public string? ImagePath { get; set; }
        public string? Url { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
        //public bool? IsShow { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
