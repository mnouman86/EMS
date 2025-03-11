using CleanArc.Domain.Entities.CarRentalSearchFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.CarRentalSearchFilter.Queries.GetAllCarRentalSearchFilter
{
    public class GetAllCarRentalSearchFilterQueryResult
    {
        public int ID { get; set; }
        public int? CarID { get; set; }
        public int BusinessID { get; set; }
        public string Name { get; set; }
        public int CityID { get; set; }
        public string CarModel { get; set; }
        public string CarModelYear { get; set; }
        public decimal CarRentPrice { get; set; }
        public decimal CarDetailPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int TotalDays { get; set; }

        public List<CleanArc.Domain.Entities.CarRentalSearchFilter.SearchCarImage> SearchCarImage { get; set; }
        public List<CleanArc.Domain.Entities.CarRentalSearchFilter.SearchCarAmenities> SearchCarAmenities { get; set; }
    }
}
