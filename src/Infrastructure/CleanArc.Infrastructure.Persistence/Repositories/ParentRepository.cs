using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Parent;
using CleanArc.Infrastructure.Sql.SqlQueries;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class ParentRepository : IParentRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ParentRepository> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ParentRepository(IConfiguration configuration, ILogger<ParentRepository> logger, IHttpContextAccessor httpContextAccessor)
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

    public Task<ResponseEntity> LinkAsync(int userId, int studentId, string? relationship, int? createdBy)
    {
        var p = new DynamicParameters();
        p.Add("@UserId", userId, DbType.Int32);
        p.Add("@StudentId", studentId, DbType.Int32);
        p.Add("@Relationship", relationship);
        p.Add("@CreatedBy", createdBy, DbType.Int32);
        using var conn = OpenConnection();
        return conn.QueryFirstOrDefaultAsync<ResponseEntity>(ParentQueries.Link_StudentParent, p, commandType: CommandType.StoredProcedure);
    }

    public Task<ResponseEntity> UnlinkAsync(int userId, int studentId)
    {
        var p = new DynamicParameters();
        p.Add("@UserId", userId, DbType.Int32);
        p.Add("@StudentId", studentId, DbType.Int32);
        using var conn = OpenConnection();
        return conn.QueryFirstOrDefaultAsync<ResponseEntity>(ParentQueries.Unlink_StudentParent, p, commandType: CommandType.StoredProcedure);
    }

    public Task<ListResponseWrapper<ParentChildRow>> GetChildrenAsync(int userId)
    {
        var p = new DynamicParameters();
        p.Add("@UserId", userId, DbType.Int32);
        return ListWithOutputs<ParentChildRow>(ParentQueries.Get_ParentChildren, p);
    }

    public async Task<bool> IsLinkedAsync(int userId, int studentId)
    {
        var p = new DynamicParameters();
        p.Add("@UserId", userId, DbType.Int32);
        p.Add("@StudentId", studentId, DbType.Int32);
        var res = await ListWithOutputs<ParentLinkCheckRow>(ParentQueries.Is_ParentOfStudent, p);
        return res.Data.FirstOrDefault()?.IsLinked ?? false;
    }

    public Task<ListResponseWrapper<StudentPaymentRow>> GetStudentPaymentsAsync(int studentId)
    {
        var p = new DynamicParameters();
        p.Add("@StudentId", studentId, DbType.Int32);
        return ListWithOutputs<StudentPaymentRow>(ParentQueries.Get_StudentPayments, p);
    }
}
