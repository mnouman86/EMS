using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Activities
{
    public class CreateActivityDTO
    {
        // public int ID { get; set; }
        public string? Title { get; set; }
        public int? LanguageID { get; set; }
        public int? ServiceID { get; set; }
        public int? BusinessID { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public int? ActivityTypeID { get; set; }
        public int? ActivityNatureID { get; set; }
        public int? MinGroupSize { get; set; }
        public int? MaxGroupSize { get; set; }
        public int? PrivateParticipantID { get; set; }
        public string? WhoCanParticipate { get; set; }
        public string? WhoCannotParticipate { get; set; }
        public int? ManageActivityID { get; set; }
        public int? Days { get; set; }
        public int? Hours { get; set; }
        public int? AddressID { get; set; }
        public string? Description { get; set; }
        public bool? IsTransportation { get; set; }
        public int? TransportationID { get; set; }
        public int? ActivityIncludeID { get; set; }
        public bool? IsDisability { get; set; }
        public int? DisabilitiesID { get; set; }
        public string? Recommendation { get; set; }
        public string? AllowedItems { get; set; }
        public int? CurrencyID { get; set; }
        public decimal? PerPersonPrice { get; set; }
        public decimal? PerGroupPrice { get; set; }
        public int? SeasonID { get; set; }
        public int? CreatedBy { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedAt { get; set; }

    }
}
