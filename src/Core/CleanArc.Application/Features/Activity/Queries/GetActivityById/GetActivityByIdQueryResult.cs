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

        public int ID { get; set; }
        public string Title { get; set; }
        public int? LanguageID { get; set; }
        public string? LanguageName { get; set; }

        public int? ServiceID { get; set; }
        public string? ServiceName { get; set; }

        public int? SubServiceID { get; set; }
        public string? SubServiceName { get; set; }

        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public int? ActivityTypeID { get; set; }
        public string? ActivityTypeName { get; set; }
        public int? ActivityNatureID { get; set; }
        public string? ActivityNatureName { get; set; }

        public int? MinGroupSize { get; set; }
        public int? MaxGroupSize { get; set; }
        public bool? PrivateActivity { get; set; }
        public string? WhoCanParticipate { get; set; }
        public string? WhoCannotParticipate { get; set; }
        public int? ManageActivityID { get; set; }
        public string? ManageActivityName { get; set; }

        public int? Days { get; set; }
        public int? Hours { get; set; }
        public string Description { get; set; }
        public bool? IsTransportation { get; set; }
        public int? TransportationID { get; set; }
        public string? TransportationName { get; set; }

        public bool? IsDisability { get; set; }
        public string? DisabilityName { get; set; }

        public string AllowedItems { get; set; }
        public string NotAllowedItems { get; set; }
        public int? CurrencyID { get; set; }
        public string CurrencyName { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public int? Message { get; set; }
        public Decimal? PerPersonPrice { get; set; }
        //public int? PerGroupPrice { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
