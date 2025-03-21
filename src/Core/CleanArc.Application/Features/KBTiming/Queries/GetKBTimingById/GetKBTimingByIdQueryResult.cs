using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBTiming.Queries.GetKBTimingById
{
    public class GetKBTimingByIdQueryResult
    //(int Id, string Name, string Description, bool IsDeleted, bool IsActive, int CreatedBy, DateTime CreatedAt, int UpdatedBy, DateTime UpdatedAt);
    {
        public int Id { get; set; }
        public int? GenericTitleID { get; set; }
        public string? Day { get; set; }
        public string? TimeFrom { get; set; }
        public string? TimeTo { get; set; }
        public bool? IsAlwaysOpen { get; set; }
        public bool? IsClosed { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
    }
}
