namespace CleanArc.Application.Models.Permission
{
    /* One row of the permission matrix sent from the UI. PascalCase property names
       are preserved by System.Text.Json default options so they match the
       OPENJSON paths ($.FeatureId, $.CanRead, ...) in usp_Save_UserFeaturePermissions. */
    public class FeaturePermissionInput
    {
        public int FeatureId { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanExport { get; set; }
        public bool CanApprove { get; set; }
        public bool CanPrint { get; set; }
    }
}
