using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.PopularItemsVisit.Queries.GetAllPopularItemsVisit;

public class GetAllPopularItemsVisitQueryResult
{
    //public int? Id { get; set; }
    //public string? PageVisiteUrl { get; set; }
    //public string? SessionDuration { get; set; }
    //public int? VisitCount { get; set; }
    //public DateTime? FirstVisitAt { get; set; }
    //public DateTime? LastVisitAt { get; set; }
    //public bool IsDeleted { get; set; }
    //public bool IsActive { get; set; }
    //public int CreatedBy { get; set; }
    //public DateTime CreatedAt { get; set; }
    //public int UpdatedBy { get; set; }
    //public DateTime UpdatedAt { get; set; }
    //public int? CultureId { get; set; }
    //public int? Code { get; set; }
    //public string? Message { get; set; }
    //public int? UserId { get; set; }
    //public string? Name { get; set; }
    //public string? FamilyName { get; set; }
    //public string? GeneratedCode { get; set; }
    //public string? UserName { get; set; }
    //public string? NormalizedUserName { get; set; }
    //public string? Email { get; set; }
    //public string? NormalizedEmail { get; set; }
    //public bool? EmailConfirmed { get; set; }
    //public string? PasswordHash { get; set; }
    //public string? SecurityStamp { get; set; }
    //public string? ConcurrencyStamp { get; set; }
    //public string? PhoneNumber { get; set; }
    //public bool? PhoneNumberConfirmed { get; set; }
    //public bool? TwoFactorEnabled { get; set; }
    //public DateTimeOffset? LockoutEnd { get; set; }
    //public bool? LockoutEnabled { get; set; }
    //public int? AccessFailedCount { get; set; }
    public int? PageId { get; set; }
    public string? Title { get; set; }
    public int? ServiceId { get; set; }
    public Decimal? Price { get; set; }
    public string? ImagePath { get; set; }
    public string? Address { get; set; }
    public string? IpAddress { get; set; }
    public string? PageURL { get; set; }
   // public string? VisitAt { get; set; }
    public int? TotalVisitCount { get; set; }
}

//    public GetAllPopularItemsVisitQueryResult(int iD, string name, string description, bool isDeleted, bool isActive, int createdBy, DateTime createdAt, int updatedBy, DateTime updatedAt)
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
