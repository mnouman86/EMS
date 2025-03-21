using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.BusinessType.Queries.GetAllBusinessType
{
    public class GetAllBusinessTypeQueryResult
    {
        public int? Id { get; set; }
        //public string? Company { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
        public string? BusinessTypeName { get; set; }
        //public string? IndividualPerson { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
