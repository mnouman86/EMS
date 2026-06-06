using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Permission;
using CleanArc.Infrastructure.Sql.SqlQueries;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<PermissionRepository> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PermissionRepository(IConfiguration configuration, ILogger<PermissionRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    private SqlConnection OpenConnection()
    {
        var conn = new SqlConnection(_configuration.GetConnectionString("DBConnection1"));
        conn.Open();
        return conn;
    }

    private async Task<ListResponseWrapper<T>> ListWithOutputs<T>(string sp, DynamicParameters parameters)
    {
        using var connection = OpenConnection();
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        var rows = (await connection.QueryAsync<T>(sp, parameters, commandType: CommandType.StoredProcedure)).ToList();
        return new ListResponseWrapper<T>
        {
            Data = rows,
            TotalCount = rows.Count,
            Code = parameters.Get<int?>("@Code") ?? 0,
            Message = parameters.Get<string>("@Message") ?? string.Empty
        };
    }

    private async Task<ResponseEntity> Scalar(string sp, DynamicParameters parameters)
    {
        using var connection = OpenConnection();
        return await connection.QueryFirstOrDefaultAsync<ResponseEntity>(sp, parameters, commandType: CommandType.StoredProcedure);
    }

    public Task<ListResponseWrapper<AppFeatureRow>> GetFeaturesAsync()
        => ListWithOutputs<AppFeatureRow>(PermissionQueries.Get_Features, new DynamicParameters());

    public Task<ListResponseWrapper<AppPermissionRow>> GetPermissionsAsync()
        => ListWithOutputs<AppPermissionRow>(PermissionQueries.Get_Permissions, new DynamicParameters());

    public Task<ListResponseWrapper<UserEffectivePermissionRow>> GetUserEffectivePermissionsAsync(int userId)
    {
        var p = new DynamicParameters();
        p.Add("@UserId", userId, DbType.Int32);
        return ListWithOutputs<UserEffectivePermissionRow>(PermissionQueries.Get_UserEffectivePermissions, p);
    }

    public Task<ListResponseWrapper<UserFeaturePermissionGridRow>> GetUserFeaturePermissionGridAsync(int userId)
    {
        var p = new DynamicParameters();
        p.Add("@UserId", userId, DbType.Int32);
        return ListWithOutputs<UserFeaturePermissionGridRow>(PermissionQueries.Get_UserFeaturePermissionGrid, p);
    }

    public Task<ListResponseWrapper<RoleFeaturePermissionRow>> GetRoleFeaturePermissionsAsync(int roleId)
    {
        var p = new DynamicParameters();
        p.Add("@RoleId", roleId, DbType.Int32);
        return ListWithOutputs<RoleFeaturePermissionRow>(PermissionQueries.Get_RoleFeaturePermissions, p);
    }

    public async Task<ListResponseWrapper<PermissionUserRow>> GetPermissionUsersAsync(string? search, int? roleId, bool? isActive, int pageNumber, int pageSize)
    {
        var p = new DynamicParameters();
        p.Add("@Search", string.IsNullOrWhiteSpace(search) ? null : search.Trim());
        p.Add("@RoleId", roleId, DbType.Int32);
        p.Add("@IsActive", isActive, DbType.Boolean);
        p.Add("@PageNumber", pageNumber, DbType.Int32);
        p.Add("@PageSize", pageSize, DbType.Int32);
        p.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        using var conn = OpenConnection();
        var rows = (await conn.QueryAsync<PermissionUserRow>(PermissionQueries.Get_PermissionUsers, p, commandType: CommandType.StoredProcedure)).ToList();
        return new ListResponseWrapper<PermissionUserRow>
        {
            Data = rows,
            TotalCount = p.Get<int?>("@TotalCount") ?? rows.Count,
            Code = p.Get<int?>("@Code") ?? 0,
            Message = p.Get<string>("@Message") ?? string.Empty
        };
    }

    public Task<ListResponseWrapper<RoleLookupRow>> GetRolesAsync()
        => ListWithOutputs<RoleLookupRow>(PermissionQueries.Get_Roles, new DynamicParameters());

    public Task<ResponseEntity> SaveUserFeaturePermissionsAsync(int userId, int changedBy, string permsJson)
    {
        var p = new DynamicParameters();
        p.Add("@UserId", userId, DbType.Int32);
        p.Add("@ChangedBy", changedBy, DbType.Int32);
        p.Add("@PermsJson", permsJson);
        return Scalar(PermissionQueries.Save_UserFeaturePermissions, p);
    }

    public Task<ResponseEntity> SaveRoleFeaturePermissionsAsync(int roleId, int changedBy, string permsJson)
    {
        var p = new DynamicParameters();
        p.Add("@RoleId", roleId, DbType.Int32);
        p.Add("@ChangedBy", changedBy, DbType.Int32);
        p.Add("@PermsJson", permsJson);
        return Scalar(PermissionQueries.Save_RoleFeaturePermissions, p);
    }
}
