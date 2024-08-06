using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.BusinessProfile.Query.GetAllBusinessProfile
{
    public class GetAllBusinessProfileQueryResult
    {
        public int? ID { get; set; }
        public int? CultureId { get; set; }

        public string? Company { get; set; }
        public string? IndividualPerson { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
