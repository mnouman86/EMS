using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.PackageType.Queries.GetAllPackageType;

public class GetAllPackageTypeQueryResult
{
    public int ID { get; set; }              // Corresponds to [ID] [int] IDENTITY(1,1) NOT NULL
    public string? Title { get; set; }        // Corresponds to [Title] [varchar](max) NULL
    public bool? IsActive { get; set; }      // Corresponds to [IsActive] [bit] NULL
    public bool? IsDeleted { get; set; }     // Corresponds to [IsDeleted] [bit] NULL
    public int? CreatedBy { get; set; }      // Corresponds to [CreatedBy] [int] NULL
    public DateTime? CreatedAt { get; set; } // Corresponds to [CreatedAt] [datetime] NULL
    public int? UpdatedBy { get; set; }      // Corresponds to [UpdatedBy] [int] NULL
    public DateTime? UpdatedAt { get; set; } // Corresponds to [UpdatedAt] [datetime] NULL
    public int? CultureId { get; set; }
    public int? Code { get; set; }
    public int? Message { get; set; }
}


//    public GetAllPackageTypeQueryResult(int iD, string name, string description, bool isDeleted, bool isActive, int createdBy, DateTime createdAt, int updatedBy, DateTime updatedAt)
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
