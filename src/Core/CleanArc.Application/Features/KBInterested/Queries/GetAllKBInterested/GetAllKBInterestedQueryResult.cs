using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBInterested.Queries.GetAllKBInterested;

public class GetAllKBInterestedQueryResult
{
    // public int ID { get; set; }
    public int ID { get; set; }
    public string? Type { get; set; }               // Maps to   NULL
    public string? Name { get; set; }               // Maps to   NULL
    public string? Description { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CultureId { get; set; }
    public int? Code { get; set; }
    public int? Message { get; set; }
}

//    public GetAllKBInterestedQueryResult(int iD, string name, string description, bool isDeleted, bool isActive, int createdBy, DateTime createdAt, int updatedBy, DateTime updatedAt)
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
