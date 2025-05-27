using CleanArc.Domain.Entities.Amenity;
using CleanArc.Domain.Entities.CustomerReview;
using CleanArc.Domain.Entities.Language;
using CleanArc.Domain.Entities.RoomDetails;
using CleanArc.Domain.Entities.RoomView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.Hotel
{
    public class HotelDetail:Hotel
    {
        public List<RoomDetails.RoomDetails> Rooms { get; set; }
        public List<GenericMedia.GenericMedia> Medias { get; set; }
        public List<AmenityMapping.AmenityMapping> Amenities { get; set; }
        public List<FAQs.FAQs> FAQs { get; set; }
        public List<CustomerReview.CustomerReview> Reviews { get; set; }
        public List<RoomView.RoomView> RoomView { get; set; }
        public List<OutDoor.OutDoor> OutDoor { get; set; }

    }
}
