using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.GroupActivityParticipants
{
    public class CreateGroupActivityParticipantsDTO
    {
        // public int Id { get; set; }
        public string Email { get; set; }
        public string? guid { get; set; }
        public string MobileNumber { get; set; }
        public int? GenericTitleId { get; set; }
        public int? GroupTypeId { get; set; }
        public int? GroupSize { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? Lead { get; set; }
        //public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? CultureId { get; set; }
       

    }
}
