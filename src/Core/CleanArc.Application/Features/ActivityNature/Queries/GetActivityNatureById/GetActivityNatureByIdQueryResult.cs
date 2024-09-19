using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityNature.Queries.GetActivityNatureById
{
    public class GetActivityNatureByIdQueryResult
    //(int Id, string Name, string Description, bool IsDeleted, bool IsActive, int CreatedBy, DateTime CreatedAt, int UpdatedBy, DateTime UpdatedAt);
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
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
