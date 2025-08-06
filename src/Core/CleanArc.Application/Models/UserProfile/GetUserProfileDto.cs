using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.UserProfile
{
    public class GetUserProfileDto
    {
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? GenderId { get; set; }
        public int? NationalityId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
    }
}
