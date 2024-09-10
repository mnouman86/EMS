using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBRelatedUrlLink.Queries.GetKBRelatedUrlLinkById
{
    public class GetKBRelatedUrlLinkByIdQueryResult
    //(int Id, string Name, string Description, bool IsDeleted, bool IsActive, int CreatedBy, DateTime CreatedAt, int UpdatedBy, DateTime UpdatedAt);
    {
        public int ID { get; set; }
        public int? KBDetailID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public int? Message { get; set; }
    }
}
