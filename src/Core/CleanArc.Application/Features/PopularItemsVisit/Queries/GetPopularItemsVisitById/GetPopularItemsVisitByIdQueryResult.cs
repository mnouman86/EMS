using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.PopularItemsVisit.Queries.GetPopularItemsVisitById
{
    public class GetPopularItemsVisitByIdQueryResult
    //(int Id, string Name, string Description, bool IsDeleted, bool IsActive, int CreatedBy, DateTime CreatedAt, int UpdatedBy, DateTime UpdatedAt);
    {
        public int? ID { get; set; }
        public string? PageVisiteUrl { get; set; }
        public string? SessionDuration { get; set; }
        public int? VisitCount { get; set; }
        public DateTime? FirstVisitAt { get; set; }
        public DateTime? LastVisitAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public int? Message { get; set; }
        public int? UserId { get; set; }
        public string? Name { get; set; }
        public string? FamilyName { get; set; }
        public string? GeneratedCode { get; set; }
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool? LockoutEnabled { get; set; }
        public int? AccessFailedCount { get; set; }
    }
}
