using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.AcademicYear;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.AcademicYear;
using CleanArc.Infrastructure.Persistence.Helpers;
using CleanArc.Infrastructure.Sql.SqlQueries;
using CleanArc.SharedKernel.Extensions;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class AcademicYearRepository : IAcademicYearRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AcademicYearRepository> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AcademicYearRepository(
        IConfiguration configuration,
        ILogger<AcademicYearRepository> logger,
        IHttpContextAccessor httpContextAccessor)
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

    private async Task<ResponseEntity> ExecScalarAsync<T>(string sp, T parameters)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, parameters);
        using var connection = OpenConnection();
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            sp, parameters, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }

    public Task<ResponseEntity> CreateAsync(CreateAcademicYearDTO dto)
        => ExecScalarAsync(AcademicYearQueries.Create_AcademicYear, dto);

    public Task<ResponseEntity> UpdateAsync(UpdateAcademicYearDTO dto)
        => ExecScalarAsync(AcademicYearQueries.Update_AcademicYear, dto);

    public Task<ResponseEntity> SetCurrentAsync(SetCurrentAcademicYearDTO dto)
        => ExecScalarAsync(AcademicYearQueries.SetCurrent_AcademicYear, dto);

    public async Task<ResponseEntity> DeleteAsync(DeleteRequest deleteRequest, int? updatedBy)
    {
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@Ids", deleteRequest.SelectedIds);
        parameters.Add("@UpdatedBy", updatedBy);
        parameters.Add("@IsDeleted", deleteRequest.isDeleted);
        parameters.Add("@ForceHard", deleteRequest.ForceHard);
        return await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            AcademicYearQueries.Delete_AcademicYear, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<ListResponseWrapper<AcademicYear>> GetAllAsync(SearchRequest request)
    {
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@PageNumber", request.PageNumber, DbType.Int32);
        if (request.PageSize > 0) parameters.Add("@PageSize", request.PageSize, DbType.Int32);
        parameters.Add("@SortingArray", DataTableHelper.ToDataTable(request.SortingArray), DbType.Object);
        parameters.Add("@FilterArray", DataTableHelper.ToDataTable(request.FilterArray), DbType.Object);
        parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

        var result = await connection.QueryAsync<AcademicYear>(
            AcademicYearQueries.GetAll_AcademicYear, parameters, commandType: CommandType.StoredProcedure);

        return new ListResponseWrapper<AcademicYear>
        {
            Data = result.ToList(),
            TotalCount = parameters.Get<int?>("@TotalCount") ?? 0,
            Code = parameters.Get<int?>("@Code") ?? 0,
            Message = parameters.Get<string>("@Message") ?? string.Empty
        };
    }

    public async Task<SingleResponseWrapper<AcademicYear>> GetByIdAsync(SearchRequestById request)
    {
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@ID", request.Id, DbType.Int32);
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        var data = await connection.QuerySingleOrDefaultAsync<AcademicYear>(
            AcademicYearQueries.GetById_AcademicYear, parameters, commandType: CommandType.StoredProcedure);
        return new SingleResponseWrapper<AcademicYear>
        {
            Data = data,
            Code = parameters.Get<int>("@Code"),
            Message = parameters.Get<string>("@Message")
        };
    }

    public async Task<SingleResponseWrapper<AcademicYear>> GetCurrentAsync()
    {
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        var data = await connection.QuerySingleOrDefaultAsync<AcademicYear>(
            AcademicYearQueries.GetCurrent_AcademicYear, parameters, commandType: CommandType.StoredProcedure);
        return new SingleResponseWrapper<AcademicYear>
        {
            Data = data,
            Code = parameters.Get<int>("@Code"),
            Message = parameters.Get<string>("@Message")
        };
    }
}
