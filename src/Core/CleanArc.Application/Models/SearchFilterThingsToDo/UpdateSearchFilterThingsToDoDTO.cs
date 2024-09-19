using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.SearchFilterThingsToDo
{
    public class UpdateSearchFilterThingsToDoDTO
    {

        public int ID { get; set; }
        public int? CultureId { get; set; }
        public string Title { get; set; }
        public int? LanguageLookUpID { get; set; }
        public int? ServiceLookUpID { get; set; }
        public int? SubServiceLookUpID { get; set; }
        public string? OtherSubService { get; set; }

        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public int? ActivityTypeLookUpID { get; set; }
        public int? ActivityNatureLookUpID { get; set; }
        //public int? MinGroupSize { get; set; }
        public int? MaxGroupSize { get; set; }
        //public int? IsPrivateActivity { get; set; }
        public string WhoCanParticipate { get; set; }
        public string WhoCannotParticipate { get; set; }
        public int? ManageActivityLookUpID { get; set; }
        public string? OtherManageActivity { get; set; }

        public int? Days { get; set; }
        public int? Hours { get; set; }
        public string Description { get; set; }
        public bool? IsTransportation { get; set; }
        public int? TransportationLookUpID { get; set; }
        public bool? IsDisability { get; set; }
        public string AllowedItems { get; set; }
        public string NotAllowedItems { get; set; }
        public int? CurrencyLookUpID { get; set; }
        //public Decimal? PerPersonPrice { get; set; }
        public string? SeasonLookUpID { get; set; }
        public string? IncludeOptionLookUpID { get; set; }
        public string? DisabilityOptionLookUpID { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
        public int? UpdatedBy { get; set; }
        public string? StartTime { get; set; } // Nullable TimeSpan for StartTime
        public string? EndTime { get; set; }   // Nullable TimeSpan for EndTime
        public DateTime? StartDate { get; set; } // Nullable DateTime for StartDate
        public DateTime? EndDate { get; set; }   // Nullable DateTime for EndDate
        //public DateTime? UpdatedAt { get; set; }
    }
}
