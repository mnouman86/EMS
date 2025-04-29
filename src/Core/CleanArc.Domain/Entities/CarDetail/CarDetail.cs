using CleanArc.Domain.Entities.Amenity;
using CleanArc.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.CarDetail
{
    public class CarDetail: Car
    {
        
        public List<Car> Cars { get; set; }
        public List<AmenityLookUp> Amenities { get; set; }
        public List<LanguageLookUp> Languages { get; set; }
        public List<FAQs.FAQs> FAQs { get; set; }
        public List<CustomerReview.CustomerReview> Reviews { get; set; }
    }
}
