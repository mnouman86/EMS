using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Activities
{
    public class UpdateActivityDTO
    {
        public int Id { get; set; }
        public int? BusinessId { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        //public string? LanguageName { get; set; }
        public int? ServiceCategoryId { get; set; }
        public int? SubServiceCategoryId { get; set; }
        public string? OtherSubService { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public int? ActivityTypeLookUpId { get; set; }
        public int? ActivityNatureLookUpId { get; set; }
        public int? MaxGroupSize { get; set; }
        //public bool? IsPrivateActivity { get; set; }
        public string WhoCanParticipate { get; set; }
        public string WhoCannotParticipate { get; set; }
        public int? ActivitySupervisorLookUpId { get; set; }
        public string? OtherManageActivity { get; set; }
        public int? Days { get; set; }
        public int? Hours { get; set; }
        public string Description { get; set; }
        public bool? IsTransportation { get; set; }
        public int? TransportationLookUpId { get; set; }
        public bool? IsDisability { get; set; }
        public string AllowedItems { get; set; }
        public string NotAllowedItems { get; set; }
        public int? CurrencyLookUpId { get; set; }
        //public Decimal? Price { get; set; }
        //public Decimal? PerPersonPrice { get; set; }
        //public int? TotalParticipant { get; set; }
        //public Decimal? ActivityPrice { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CultureId { get; set; }
        //public int? GenericTitleId { get; set; }
        //public string? Duration { get; set; }
        //public string? Cancellation { get; set; }
        public string? StartTime { get; set; } // Nullable TimeSpan for StartTime
        public string? EndTime { get; set; }   // Nullable TimeSpan for EndTime
        public string? StartDate { get; set; } // Nullable DateTime for StartDate
        public string? EndDate { get; set; }   // Nullable DateTime for EndDate
        public bool? IsPartiallyRefundable { get; set; }
        public bool? IsFullyRefundable { get; set; }
        public string? RefundPolicy { get; set; }
        public string? NonRefundPolicy { get; set; }
        public string? CancellationPolicy { get; set; }
    }
}
