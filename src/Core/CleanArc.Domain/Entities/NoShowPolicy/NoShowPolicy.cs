using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.NoShowPolicy
{
    public class NoShowPolicy
    {
        public int Id { get; set; }
        public int? RatePlanTypeID { get; set; }
        public string Description { get; set; }

        public decimal? DeductionPercentage { get; set; }
        public bool? OneNight { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
		public int? CultureId { get; set; }
	}
    public class NoShowPolicyLookUp
    {
        public int NoShowPolicyLookUpId { get; set; }
        public string? NoShowPolicy { get; set; }
    }
}
