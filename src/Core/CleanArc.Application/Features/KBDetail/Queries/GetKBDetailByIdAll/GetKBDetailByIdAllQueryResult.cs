using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBDetail.Queries.GetKBDetailByIdAll
{
    public class GetKBDetailByIdAllQueryResult
    //(int Id, string Name, string Description, bool IsDeleted, bool IsActive, int CreatedBy, DateTime CreatedAt, int UpdatedBy, DateTime UpdatedAt);
    {
        public int? ID { get; set; }
        public int? ServiceCategoryID { get; set; }
        public string ServiceCategory { get; set; }
        public string Title { get; set; }
        public KnowledgeBaseDetail Detail { get; set; }
        public IEnumerable<KnowledgeBaseDescription> Description { get; set; }
        public IEnumerable<KnowledgeBaseAddress> Address { get; set; }
        public IEnumerable<KnowledgeBaseMedia> Media { get; set; }
        public IEnumerable<KnowledgeBaseTiming> Timing { get; set; }
    }
}
