using CleanArc.Application.Features.Activity.Queries.GetAllActivity;
using CleanArc.Application.Features.ActivityAddress.Queries.GetAllGenericAddress;
using CleanArc.Application.Features.ActivityPerGroupPrice.Queries.GetActivityPerGroupPriceById;
using CleanArc.Application.Features.ActivityPerGroupPrice.Queries.GetAllActivityPerGroupPrice;
using CleanArc.Application.Features.ActivitySchedule.Commands.CreateActivityScheduleCommand;
using CleanArc.Application.Features.ActivitySchedule.Queries.GetActivityScheduleById;
using CleanArc.Application.Features.ActivitySchedule.Queries.GetAllActivitySchedule;
using CleanArc.Application.Features.CustomerReview.Queries.GetAllCustomerReviews;
using CleanArc.Application.Features.CustomerReview.Queries.GetCustomerReviewById;
using CleanArc.Application.Features.FAQs.Queries.GetAllFAQs;
using CleanArc.Application.Features.FAQs.Queries.GetFAQsById;
using CleanArc.Application.Features.GenericMedia.Queries.GetAllGenericMedia;
using CleanArc.Domain.Entities.ActivityDisabilityOption;
using CleanArc.Domain.Entities.ActivityIncludedOption;
using CleanArc.Domain.Entities.ActivitySeason;
using CleanArc.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Activity.Queries.GetActivityDetailById
{
    public class GetActivityDetailByIdQueryResult
    //(int Id, string Name, string Description, bool IsDeleted, bool IsActive, int CreatedBy, DateTime CreatedAt, int UpdatedBy, DateTime UpdatedAt);
    {


        public int Id { get; set; }
        public int BusinessId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        //public int? LanguageLookUpID { get; set; }
        public IEnumerable<LanguageLookUp>? Languages { get; set; }
        public IEnumerable<IncludedOptionsLookup>? IncludedOptions { get; set; }
        public IEnumerable<DisabilityOptionsLookUp>? DisabilityOptions { get; set; }
        public IEnumerable<ActivitySeasonLookUp>? Seasons { get; set; }
        public IEnumerable<GetAllActivityScheduleQueryResult>? Schedule { get; set; }
        public IEnumerable<GetAllActivityPerGroupPriceQueryResult>? GroupPrice { get; set; }
        public IEnumerable<GetAllFAQsQueryResult>? FAQs { get; set; }
        public IEnumerable<GetAllCustomerReviewsQueryResult>? Reviews { get; set; }

        public IEnumerable<GetAllGenericMediaQueryResult> ActivityImages { get; set; }
        public List<GetAllGenericAddressQueryResult> ActivityAddress { get; set; }
        public List<GetAllActivityQueryResult> RelatedActivities { get; set; }

        //public string? LanguageName { get; set; }
        public int? ServiceCategoryId { get; set; }
        public int? SubServiceCategoryId { get; set; }
        public string? OtherSubService { get; set; }
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
        public string? RefundStatement { get; set; }
        public string? NonRefundPolicy { get; set; }
        public string? CancellationPolicy { get; set; }
        public string? LanguageName { get; set; }
        public string? AgeGroup { get; set; }
        public int? ReviewsCount { get; set; }
        public int? Rating { get; set; }
        public string? OtherManageActivity { get; set; }
        public int[]? IncludeOptionLookUpId { get; set; }
        public int[]? DisabilityOptionLookUpId { get; set; }
        public Decimal? ActivityPrice { get; set; }
        public string? Duration { get; set; }
        public string? Cancellation { get; set; }
    }
}
