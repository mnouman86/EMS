using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.RatePlanType.Queries.GetAllRatePlanTypes
{
    public class GetAllRatePlanTypesQueryResult
    {
        public int Id { get; set; }
        public int? RoomTypeId { get; set; }
        public int? GuestQuantity { get; set; }
        public decimal? DefaultRate { get; set; } = decimal.Zero;
        public string RatePlanName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CultureId { get; set; }
    }
}
