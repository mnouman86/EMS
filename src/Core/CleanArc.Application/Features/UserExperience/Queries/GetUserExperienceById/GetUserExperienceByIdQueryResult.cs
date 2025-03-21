using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.UserExperience.Queries.GetUserExperienceById
{
    public class GetUserExperienceByIdQueryResult
    //(int Id, string Name, string Description, bool IsDeleted, bool IsActive, int CreatedBy, DateTime CreatedAt, int UpdatedBy, DateTime UpdatedAt);
    {
        public int Id { get; set; }
        public int? UserID { get; set; } // Nullable foreign key
        public string? UserIntrestIDs { get; set; } // Nullable string to store interest IDs

        // Fields from the User (U) table
        public string? Name { get; set; } // User's name
        public string? UserName { get; set; } // Username
        public string? Email { get; set; } // User's email
        public string? PhoneNumber { get; set; } // User's phone number
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
    }
}
