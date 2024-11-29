using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.GroupActivityParticipants
{
    public class UpdateGroupActivityParticipantsDTO
    {

        public int ID { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public int? GroupActivityID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? Lead { get; set; }
        //public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        // public int? CreatedBy { get; set; }
        //public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
        //public DateTime? UpdatedAt { get; set; }
       

    }
}
