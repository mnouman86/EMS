using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Domain.Entities.Authorization;
using CleanArc.Infrastructure.Sql.SqlQueries;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class TeacherScopeRepository : ITeacherScopeRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<TeacherScopeRepository> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TeacherScopeRepository(IConfiguration configuration, ILogger<TeacherScopeRepository> logger, IHttpContextAccessor httpContextAccessor)
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

    public async Task<ListResponseWrapper<TeacherClassScopeRow>> GetClassScopeAsync(int userId)
    {
        using var connection = OpenConnection();
        var p = new DynamicParameters();
        p.Add("@UserId", userId, DbType.Int32);
        p.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        var rows = (await connection.QueryAsync<TeacherClassScopeRow>(
            AuthorizationQueries.Get_TeacherClassScope, p, commandType: CommandType.StoredProcedure)).ToList();
        return new ListResponseWrapper<TeacherClassScopeRow>
        {
            Data = rows,
            TotalCount = rows.Count,
            Code = p.Get<int?>("@Code") ?? 0,
            Message = p.Get<string>("@Message") ?? string.Empty
        };
    }
}
