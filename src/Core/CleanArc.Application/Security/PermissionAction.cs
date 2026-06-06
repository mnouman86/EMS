namespace CleanArc.Application.Security;

/// <summary>The action verbs a feature can be guarded by (mirrors UserFeaturePermission columns).</summary>
public enum PermissionAction
{
    Read,
    Create,
    Update,
    Delete,
    Export,
    Approve,
    Print
}
