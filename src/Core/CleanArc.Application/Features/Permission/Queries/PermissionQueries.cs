using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Permission.Queries
{
    /* ---------- Feature tree ---------- */
    public record GetFeaturesQuery() : IRequest<OperationResult<List<FeatureResult>>>;

    public class FeatureResult
    {
        public int FeatureId { get; set; }
        public string FeatureName { get; set; }
        public string FeatureCode { get; set; }
        public int? ParentFeatureId { get; set; }
        public string Route { get; set; }
        public string Icon { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }

    internal class GetFeaturesQueryHandler : IRequestHandler<GetFeaturesQuery, OperationResult<List<FeatureResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetFeaturesQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<FeatureResult>>> Handle(GetFeaturesQuery r, CancellationToken ct)
        {
            var res = await _u.PermissionRepository.GetFeaturesAsync();
            if (res.Code != 200) return OperationResult<List<FeatureResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<FeatureResult>>.SuccessResult(_m.Map<List<FeatureResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* ---------- One user's assignment grid ---------- */
    public record GetUserPermissionGridQuery(int UserId) : IRequest<OperationResult<List<UserPermissionGridResult>>>;

    public class UserPermissionGridResult
    {
        public int FeatureId { get; set; }
        public string FeatureName { get; set; }
        public string FeatureCode { get; set; }
        public int? ParentFeatureId { get; set; }
        public string Route { get; set; }
        public int DisplayOrder { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanExport { get; set; }
        public bool CanApprove { get; set; }
        public bool CanPrint { get; set; }
    }

    internal class GetUserPermissionGridQueryHandler : IRequestHandler<GetUserPermissionGridQuery, OperationResult<List<UserPermissionGridResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetUserPermissionGridQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<UserPermissionGridResult>>> Handle(GetUserPermissionGridQuery r, CancellationToken ct)
        {
            var res = await _u.PermissionRepository.GetUserFeaturePermissionGridAsync(r.UserId);
            if (res.Code != 200) return OperationResult<List<UserPermissionGridResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<UserPermissionGridResult>>.SuccessResult(_m.Map<List<UserPermissionGridResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* ---------- A role template (copy source) ---------- */
    public record GetRoleTemplateQuery(int RoleId) : IRequest<OperationResult<List<RolePermissionResult>>>;

    public class RolePermissionResult
    {
        public int FeatureId { get; set; }
        public string FeatureCode { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanExport { get; set; }
        public bool CanApprove { get; set; }
        public bool CanPrint { get; set; }
    }

    internal class GetRoleTemplateQueryHandler : IRequestHandler<GetRoleTemplateQuery, OperationResult<List<RolePermissionResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetRoleTemplateQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<RolePermissionResult>>> Handle(GetRoleTemplateQuery r, CancellationToken ct)
        {
            var res = await _u.PermissionRepository.GetRoleFeaturePermissionsAsync(r.RoleId);
            if (res.Code != 200) return OperationResult<List<RolePermissionResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<RolePermissionResult>>.SuccessResult(_m.Map<List<RolePermissionResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* ---------- User listing grid (paged) ---------- */
    public record GetPermissionUsersQuery(string SearchTerm, int? RoleId, bool? IsActive, int PageNumber, int PageSize)
        : IRequest<OperationResult<List<PermissionUserResult>>>;

    public class PermissionUserResult
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public DateTime? LastLogin { get; set; }
    }

    internal class GetPermissionUsersQueryHandler : IRequestHandler<GetPermissionUsersQuery, OperationResult<List<PermissionUserResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetPermissionUsersQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<PermissionUserResult>>> Handle(GetPermissionUsersQuery r, CancellationToken ct)
        {
            var res = await _u.PermissionRepository.GetPermissionUsersAsync(r.SearchTerm, r.RoleId, r.IsActive,
                r.PageNumber <= 0 ? 1 : r.PageNumber, r.PageSize <= 0 ? 20 : r.PageSize);
            if (res.Code != 200) return OperationResult<List<PermissionUserResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<PermissionUserResult>>.SuccessResult(_m.Map<List<PermissionUserResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* ---------- Roles lookup (filter + copy-from-role) ---------- */
    public record GetRolesQuery() : IRequest<OperationResult<List<RoleLookupResult>>>;

    public class RoleLookupResult
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    internal class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, OperationResult<List<RoleLookupResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetRolesQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<RoleLookupResult>>> Handle(GetRolesQuery r, CancellationToken ct)
        {
            var res = await _u.PermissionRepository.GetRolesAsync();
            if (res.Code != 200) return OperationResult<List<RoleLookupResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<RoleLookupResult>>.SuccessResult(_m.Map<List<RoleLookupResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* ---------- Current user's effective permissions (for the SPA) ---------- */
    public record GetMyPermissionsQuery(int UserId, bool IsSuperAdmin) : IRequest<OperationResult<MyPermissionsResult>>;

    public class MyPermissionsResult
    {
        public bool IsSuperAdmin { get; set; }
        public List<EffectivePermissionResult> Permissions { get; set; } = new();
    }

    public class EffectivePermissionResult
    {
        public string FeatureCode { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanExport { get; set; }
        public bool CanApprove { get; set; }
        public bool CanPrint { get; set; }
    }

    internal class GetMyPermissionsQueryHandler : IRequestHandler<GetMyPermissionsQuery, OperationResult<MyPermissionsResult>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetMyPermissionsQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<MyPermissionsResult>> Handle(GetMyPermissionsQuery r, CancellationToken ct)
        {
            var result = new MyPermissionsResult { IsSuperAdmin = r.IsSuperAdmin };
            if (!r.IsSuperAdmin)
            {
                var res = await _u.PermissionRepository.GetUserEffectivePermissionsAsync(r.UserId);
                if (res.Code == 200 && res.Data != null)
                    result.Permissions = _m.Map<List<EffectivePermissionResult>>(res.Data);
            }
            return OperationResult<MyPermissionsResult>.SuccessResult(result);
        }
    }
}
