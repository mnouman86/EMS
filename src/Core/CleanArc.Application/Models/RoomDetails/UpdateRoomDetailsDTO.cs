using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.RoomDetails
{
    public class UpdateRoomDetailsDTO
    {
         public int ID { get; set; }
        public int? HotelID { get; set; }
        public int? RoomTypeID { get; set; }
        public int? RoomSizeUnitID { get; set; }
        public string? RoomSize { get; set; }
        public bool? IsBathroomPrivate { get; set; }
        public decimal? Price { get; set; }
        public decimal? AdditionalMatricCharges { get; set; }
        public string? RoomNumber { get; set; }
        public bool? IsAvailable { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        //public int? CreatedBy { get; set; }
        //public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public bool? IsCancelation { get; set; }
        public bool? IsRefundable { get; set; }
    }
}
