using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.WishList.Queries.GetAllWishList
{
    public class GetAllWishListQueryResult
    {
        public int Id { get; set; }
        public int ServiceTypeEnumId { get; set; }
        public int GenericTitleId { get; set; }
        public int WishListNameLookUpId { get; set; }
        public string? Priority { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int WishListLookUpId { get; set; }
        public string? HotelName { get; set; }
        public string? RoomType { get; set; }
        public string? WishListName { get; set; }

    }
}

