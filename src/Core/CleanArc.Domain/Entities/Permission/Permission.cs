using System;

namespace CleanArc.Domain.Entities.Permission
{
    /* ---- Query projections for the Access Control module ---- */

    public class AppFeatureRow
    {
        public int FeatureId { get; set; }
        public string? FeatureName { get; set; }
        public string? FeatureCode { get; set; }
        public int? ParentFeatureId { get; set; }
        public string? Route { get; set; }
        public string? Icon { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }

    public class AppPermissionRow
    {
        public int PermissionId { get; set; }
        public string? PermissionName { get; set; }
        public string? PermissionCode { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class UserEffectivePermissionRow
    {
        public string? FeatureCode { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanExport { get; set; }
        public bool CanApprove { get; set; }
        public bool CanPrint { get; set; }
    }

    public class UserFeaturePermissionGridRow
    {
        public int FeatureId { get; set; }
        public string? FeatureName { get; set; }
        public string? FeatureCode { get; set; }
        public int? ParentFeatureId { get; set; }
        public string? Route { get; set; }
        public int DisplayOrder { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanExport { get; set; }
        public bool CanApprove { get; set; }
        public bool CanPrint { get; set; }
    }

    public class RoleFeaturePermissionRow
    {
        public int FeatureId { get; set; }
        public string? FeatureCode { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanExport { get; set; }
        public bool CanApprove { get; set; }
        public bool CanPrint { get; set; }
    }

    public class RoleLookupRow
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
    }

    public class PermissionUserRow
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? RoleName { get; set; }
        public string? Department { get; set; }
        public string? Status { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
