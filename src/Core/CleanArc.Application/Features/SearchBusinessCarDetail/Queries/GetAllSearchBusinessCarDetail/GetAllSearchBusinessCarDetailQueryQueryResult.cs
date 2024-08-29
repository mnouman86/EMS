using CleanArc.Domain.Entities.SearchBusinessCarDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchBusinessCarDetail.Queries.GetAllSearchBusinessCarDetail
{
    public class GetAllSearchBusinessCarDetailQueryResult
    {
        public int? ID { get; set; }
        public int BusinessID { get; set; }
        public string BusinessName { get; set; }
        public int CityID { get; set; }
        public string CarModel { get; set; }
        public string CarModelYear { get; set; }
        public decimal CarRentPrice { get; set; }
        public decimal CarDetailPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int TotalDays { get; set; }

        public List<CleanArc.Domain.Entities.SearchBusinessCarDetail.SearchCarImage> SearchCarImage { get; set; }
        public List<CleanArc.Domain.Entities.SearchBusinessCarDetail.SearchCarAmenities> SearchCarAmenities { get; set; }
    }
}
