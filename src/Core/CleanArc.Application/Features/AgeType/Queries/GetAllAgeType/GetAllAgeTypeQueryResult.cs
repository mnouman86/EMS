using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;

public record GetAllAgeTypeQueryResult(int ID, string Name, string Description, bool IsDeleted, bool IsActive, int CreatedBy, DateTime CreatedAt, int UpdatedBy, DateTime UpdatedAt);
//public class GetAllAgeTypeQueryResult
//{
//    public int ID { get; }
//    public string Name { get; }
//    public string Description { get; }
//    public bool IsDeleted { get; }
//    public bool IsActive { get; }
//    public int CreatedBy { get; }
//    public DateTime CreatedAt { get; }
//    public int UpdatedBy { get; }
//    public DateTime UpdatedAt { get; }

//    public GetAllAgeTypeQueryResult(int iD, string name, string description, bool isDeleted, bool isActive, int createdBy, DateTime createdAt, int updatedBy, DateTime updatedAt)
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
