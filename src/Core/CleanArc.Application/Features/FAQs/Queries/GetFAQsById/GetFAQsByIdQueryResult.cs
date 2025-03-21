using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.FAQs.Queries.GetFAQsById
{
    public class GetFAQsByIdQueryResult
    //(int Id, string Name, string Description, bool IsDeleted, bool IsActive, int CreatedBy, DateTime CreatedAt, int UpdatedBy, DateTime UpdatedAt);
    {
        public int Id { get; set; }
		public int CategoryServiceID { get; set; }
		public int ServiceID { get; set; }
		public int SubServiceID { get; set; }
		public string ServiceName { get; set; }
		public string Question { get; set; }
		public string Answer { get; set; }
		public string CategoryServiceName { get; set; }
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
