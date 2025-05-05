using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.GroupActivityParticipants.Queries.GetGroupActivityParticipantsById
{
    public class GetGroupActivityParticipantsByIdQueryResult
    //(int Id, string Name, string Description, bool IsDeleted, bool IsActive, int CreatedBy, DateTime CreatedAt, int UpdatedBy, DateTime UpdatedAt);
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? guid { get; set; }
        public string MobileNumber { get; set; }
        public int? GenericTitleId { get; set; }
        public int? GroupSize { get; set; }
        public int? GroupTypeId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? Lead { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CultureId { get; set; }
    }
}
