using CleanArc.Application.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Permission;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IPermissionRepository
    {
        Task<ListResponseWrapper<AppFeatureRow>> GetFeaturesAsync();
        Task<ListResponseWrapper<AppPermissionRow>> GetPermissionsAsync();
        Task<ListResponseWrapper<UserEffectivePermissionRow>> GetUserEffectivePermissionsAsync(int userId);
        Task<ListResponseWrapper<UserFeaturePermissionGridRow>> GetUserFeaturePermissionGridAsync(int userId);
        Task<ListResponseWrapper<RoleFeaturePermissionRow>> GetRoleFeaturePermissionsAsync(int roleId);
        Task<ListResponseWrapper<PermissionUserRow>> GetPermissionUsersAsync(string? search, int? roleId, bool? isActive, int pageNumber, int pageSize);
        Task<ListResponseWrapper<RoleLookupRow>> GetRolesAsync();
        Task<ResponseEntity> SaveUserFeaturePermissionsAsync(int userId, int changedBy, string permsJson);
        Task<ResponseEntity> SaveRoleFeaturePermissionsAsync(int roleId, int changedBy, string permsJson);
    }
}
