using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.CheckProfileStatus.Queries.GetAllCheckProfileStatus;

public class GetAllCheckProfileStatusQueryResult
{
    public decimal? PercentageFilled { get; set; }
    //// public int ID { get; set; }
    //public int ID { get; set; }
    //public int? UserID { get; set; } // Nullable foreign key
    //public string? UserIntrestIDs { get; set; } // Nullable string to store interest IDs

    //// Fields from the User (U) table
    //public string? Name { get; set; } // User's name
    //public string? UserName { get; set; } // Username
    //public string? Email { get; set; } // User's email
    //public string? PhoneNumber { get; set; } // User's phone number
    //public bool IsDeleted { get; set; }
    //public bool IsActive { get; set; }
    //public int CreatedBy { get; set; }
    //public DateTime CreatedAt { get; set; }
    //public int UpdatedBy { get; set; }
    //public DateTime UpdatedAt { get; set; }
    //public int? CultureId { get; set; }
    //public int? Code { get; set; }
    //public int? Message { get; set; }
}

//    public GetAllCheckProfileStatusQueryResult(int iD, string name, string description, bool isDeleted, bool isActive, int createdBy, DateTime createdAt, int updatedBy, DateTime updatedAt)
//    {
//        ID = iD;
//        Name = name;
//        Description = description;
//        IsDeleted = isDeleted;
//        IsActive = isActive;
//        CreatedBy = createdBy;
//        CreatedAt = createdAt;
//        UpdatedBy = updatedBy;
//        UpdatedAt = updatedAt;
//    }
//}
