namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class PermissionQueries
    {
        public static string Get_Features                  => "usp_Get_AppFeatures";
        public static string Get_Permissions               => "usp_Get_AppPermissions";
        public static string Get_UserEffectivePermissions  => "usp_Get_UserEffectivePermissions";
        public static string Get_UserFeaturePermissionGrid => "usp_Get_UserFeaturePermissionGrid";
        public static string Get_RoleFeaturePermissions     => "usp_Get_RoleFeaturePermissions";
        public static string Get_PermissionUsers            => "usp_Get_PermissionUsers";
        public static string Get_Roles                      => "usp_Get_Roles";
        public static string Save_UserFeaturePermissions    => "usp_Save_UserFeaturePermissions";
        public static string Save_RoleFeaturePermissions    => "usp_Save_RoleFeaturePermissions";
    }
}
