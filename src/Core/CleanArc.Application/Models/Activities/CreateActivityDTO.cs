using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Activities
{
    public class CreateActivityDTO
    {
        public string Title { get; set; }
        public int? LanguageLookUpID { get; set; }
        public int? ServiceLookUpID { get; set; }
        public int? SubServiceLookUpID { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public int? ActivityTypeLookUpID { get; set; }
        public int? ActivityNatureLookUpID { get; set; }
        public int? MinGroupSize { get; set; }
        public int? MaxGroupSize { get; set; }
        public int? IsPrivateActivity { get; set; }
        public string WhoCanParticipate { get; set; }
        public string WhoCannotParticipate { get; set; }
        public int? ManageActivityLookUpID { get; set; }
        public int? Days { get; set; }
        public int? Hours { get; set; }
        public string Description { get; set; }
        public bool? IsTransportation { get; set; }
        public int? TransportationLookUpID { get; set; }
        public bool? IsDisability { get; set; }
        public string AllowedItems { get; set; }
        public string NotAllowedItems { get; set; }
        public int? CurrencyLookUpID { get; set; }
        public int? PerPersonPrice { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public int? ActivityID { get; set; }
        public int? Message { get; set; }
        //public Decimal? PerGroupPrice { get; set; }
        public string? OtherSubService { get; set; }
        public string? OtherManageActivity { get; set; }
        public int? CreatedBy { get; set; }
        

    }
}
