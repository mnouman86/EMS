using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.UserAssignRewards;

public  class UserAssignRewards

{
    public int ID { get; set; }
    public int? RewardRulesID { get; set; } = 2;
    public int? UserID { get; set; } // Foreign Key or reference to another table
    public string? UserIntrestIDs { get; set; } // Stores interest IDs as a string (nvarchar(max))
    //public string? URL { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? CultureId { get; set; }
    public int? Code { get; set; }
    public string? Message { get; set; }
    // Fields from the User (U) table
    public string? Name { get; set; } // User's name
    public string? UserName { get; set; } // Username
    public string? Email { get; set; } // User's email
    public string? PhoneNumber { get; set; } // User's phone number

}
