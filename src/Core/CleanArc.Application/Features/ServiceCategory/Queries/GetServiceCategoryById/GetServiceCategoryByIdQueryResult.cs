using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ServiceCategory.Queries.GetServiceCategoryById
{
    public class GetServiceCategoryByIdQueryResult
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
