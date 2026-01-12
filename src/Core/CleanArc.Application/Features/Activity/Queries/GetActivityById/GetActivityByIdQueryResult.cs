using CleanArc.Domain.Entities.ActivityDisabilityOption;
using CleanArc.Domain.Entities.ActivityIncludedOption;
using CleanArc.Domain.Entities.ActivitySeason;
using CleanArc.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Activity.Queries.GetActivityById
{
    public class GetActivityByIdQueryResult
    //(int Id, string Name, string Description, bool IsDeleted, bool IsActive, int CreatedBy, DateTime CreatedAt, int UpdatedBy, DateTime UpdatedAt);
    {


        public int Id { get; set; }
        public int BusinessId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        //public int? LanguageLookUpID { get; set; }
        public List<LanguageLookUp>? Languages { get; set; }
        public List<IncludedOptionsLookup>? IncludedOptions { get; set; }
        public List<DisabilityOptionsLookUp>? DisabilityOptions { get; set; }
        public List<ActivitySeasonLookUp>? Seasons { get; set; }

        //public string? LanguageName { get; set; }
        public int? ServiceCategoryId { get; set; }
        public int? SubServiceCategoryId { get; set; }
        public string? OtherSubService { get; set; }
        public string? SubServiceName { get; set; }
        public string? ServiceName { get; set; }

        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public int? ActivityTypeLookUpId { get; set; }
        public int? ActivityNatureLookUpId { get; set; }
        // public int? MinGroupSize { get; set; }
        public int? MaxGroupSize { get; set; }
        //public bool? IsPrivateActivity { get; set; }
        //public int? PrivateParticipantLookUpID { get; set; }
        public string WhoCanParticipate { get; set; }
        public string WhoCannotParticipate { get; set; }
        public int? ActivitySupervisorLookUpId { get; set; }
        //public string? OtherManageActivity { get; set; }
        public int? Days { get; set; }
        public int? Hours { get; set; }
        public string Description { get; set; }
        public bool? IsTransportation { get; set; }
        public int? TransportationLookUpId { get; set; }
        public bool? IsDisability { get; set; }
        public string AllowedItems { get; set; }
        public string NotAllowedItems { get; set; }
        public int? CurrencyLookUpId { get; set; }
        public string? SeasonLookUpId { get; set; }
        

        //public string? DisabilityOptionLookUpId { get; set; }
        public int? CultureId { get; set; }
        public int? GenericTitleId { get; set; }
		public Decimal? Price { get; set; }
		public Decimal? PerPersonPrice { get; set; }
		public int? TotalParticipant { get; set; }
		// public int? PerGroupPrice { get; set; }
		public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

		public string? StartTime { get; set; } // Nullable TimeSpan for StartTime
		public string? EndTime { get; set; }   // Nullable TimeSpan for EndTime
		public DateTime? StartDate { get; set; } // Nullable DateTime for StartDate
		public DateTime? EndDate { get; set; }   // Nullable DateTime for EndDate

        public bool? IsPartiallyRefundable { get; set; } 
        public bool? IsFullyRefundable { get; set; }
        public string? RefundPolicy { get; set; }
        public string? NonRefundPolicy { get; set; }
        public string? CancellationPolicy { get; set; }


    }
}
