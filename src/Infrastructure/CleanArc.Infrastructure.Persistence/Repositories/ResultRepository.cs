using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.Result;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Result;
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

public class ResultRepository : IResultRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ResultRepository> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ResultRepository(
        IConfiguration configuration,
        ILogger<ResultRepository> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    private SqlConnection OpenConnection()
    {
        var connection = new SqlConnection(_configuration.GetConnectionString("DBConnection1"));
        connection.Open();
        return connection;
    }

    private async Task<ResponseEntity> ExecScalarSpAsync<T>(string sp, T parameters)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, parameters);
        using var connection = OpenConnection();
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            sp, parameters, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }

    /* ---------- RES-01: Sessions ---------- */

    public Task<ResponseEntity> CreateSessionAsync(CreateResultSessionDTO dto)
        => ExecScalarSpAsync(ResultQueries.Create_Session, dto);

    public async Task<ListResponseWrapper<ResultSession>> GetSessionsAsync(SearchRequest request)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@PageNumber", request.PageNumber, DbType.Int32);
        if (request.PageSize > 0) parameters.Add("@PageSize", request.PageSize, DbType.Int32);
        parameters.Add("@SortingArray", DataTableHelper.ToDataTable(request.SortingArray), DbType.Object);
        parameters.Add("@FilterArray", DataTableHelper.ToDataTable(request.FilterArray), DbType.Object);
        parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

        var result = await connection.QueryAsync<ResultSession>(
            ResultQueries.GetAll_Sessions, parameters, commandType: CommandType.StoredProcedure);

        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

        return new ListResponseWrapper<ResultSession>
        {
            Data = result.ToList(),
            TotalCount = parameters.Get<int?>("@TotalCount") ?? 0,
            Code = parameters.Get<int?>("@Code") ?? 0,
            Message = parameters.Get<string>("@Message") ?? string.Empty
        };
    }

    /* ---------- RES-04: Grade bands ---------- */

    public Task<ResponseEntity> ConfigureGradeBandsAsync(ConfigureGradeBandsDTO dto)
        => ExecScalarSpAsync(ResultQueries.Configure_GradeBands, dto);

    public async Task<ListResponseWrapper<GradeBand>> GetGradeBandsAsync()
    {
        using var connection = OpenConnection();
        var result = await connection.QueryAsync<GradeBand>(
            ResultQueries.GetAll_GradeBands, commandType: CommandType.StoredProcedure);
        var list = result.ToList();
        return new ListResponseWrapper<GradeBand>
        {
            Data = list,
            TotalCount = list.Count,
            Code = 200,
            Message = "Success"
        };
    }

    /* ---------- RES-02 + RES-03: Marks entry ---------- */

    public Task<ResponseEntity> EnterMarksAsync(EnterMarksDTO dto)
        => ExecScalarSpAsync(ResultQueries.Enter_Marks, dto);

    public async Task<ListResponseWrapper<StudentSubjectMark>> GetMarksEntryGridAsync(MarksEntryGridRequest request)
    {
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@ResultSessionId", request.ResultSessionId, DbType.Int32);
        parameters.Add("@SchoolClassId", request.SchoolClassId, DbType.Int32);
        parameters.Add("@SubjectId", request.SubjectId, DbType.Int32);
        parameters.Add("@TeacherEmployeeId", request.TeacherEmployeeId, DbType.Int32);
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

        var result = await connection.QueryAsync<StudentSubjectMark>(
            ResultQueries.Get_MarksEntryGrid, parameters, commandType: CommandType.StoredProcedure);
        var list = result.ToList();

        return new ListResponseWrapper<StudentSubjectMark>
        {
            Data = list,
            TotalCount = list.Count,
            Code = parameters.Get<int?>("@Code") ?? 0,
            Message = parameters.Get<string>("@Message") ?? string.Empty
        };
    }

    /* ---------- RES-05: Lock / Unlock / Pre-lock ---------- */

    public async Task<ListResponseWrapper<PreLockMissing>> GetPreLockReportAsync(LockResultsDTO request)
    {
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@ResultSessionId", request.ResultSessionId, DbType.Int32);
        parameters.Add("@SchoolClassId", request.SchoolClassId, DbType.Int32);
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

        var result = await connection.QueryAsync<PreLockMissing>(
            ResultQueries.PreLock_Report, parameters, commandType: CommandType.StoredProcedure);
        var list = result.ToList();

        return new ListResponseWrapper<PreLockMissing>
        {
            Data = list,
            TotalCount = list.Count,
            Code = parameters.Get<int?>("@Code") ?? 0,
            Message = parameters.Get<string>("@Message") ?? string.Empty
        };
    }

    public Task<ResponseEntity> LockClassResultsAsync(LockResultsDTO dto)
        => ExecScalarSpAsync(ResultQueries.Lock_ClassResults, dto);

    public Task<ResponseEntity> UnlockClassResultsAsync(LockResultsDTO dto)
        => ExecScalarSpAsync(ResultQueries.Unlock_ClassResults, dto);

    /* ---------- RES-06: Class sheet ---------- */

    public async Task<ListResponseWrapper<ClassSheetRow>> GetClassResultSheetAsync(ClassSheetRequest request)
    {
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@ResultSessionId", request.ResultSessionId, DbType.Int32);
        parameters.Add("@SchoolClassId", request.SchoolClassId, DbType.Int32);
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

        var result = await connection.QueryAsync<ClassSheetRow>(
            ResultQueries.Get_ClassSheet, parameters, commandType: CommandType.StoredProcedure);
        var list = result.ToList();

        return new ListResponseWrapper<ClassSheetRow>
        {
            Data = list,
            TotalCount = list.Count,
            Code = parameters.Get<int?>("@Code") ?? 0,
            Message = parameters.Get<string>("@Message") ?? string.Empty
        };
    }

    /* ---------- RES-07: Parent search ---------- */

    public async Task<SingleResponseWrapper<StudentResultCard>> ParentSearchAsync(ParentSearchDTO dto)
    {
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@StudentCode", dto.StudentCode);
        parameters.Add("@SecondFactor", dto.SecondFactor);
        parameters.Add("@ClientIp", dto.ClientIp);
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

        var result = await connection.QuerySingleOrDefaultAsync<StudentResultCard>(
            ResultQueries.Parent_SearchResult, parameters, commandType: CommandType.StoredProcedure);

        return new SingleResponseWrapper<StudentResultCard>
        {
            Data = result,
            Code = parameters.Get<int>("@Code"),
            Message = parameters.Get<string>("@Message")
        };
    }

    /* ---------- RES-08: Result card data ---------- */

    public async Task<SingleResponseWrapper<StudentResultCard>> GetStudentResultCardAsync(StudentCardRequest request)
    {
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@ResultSessionId", request.ResultSessionId, DbType.Int32);
        parameters.Add("@StudentId", request.StudentId, DbType.Int32);
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

        var result = await connection.QuerySingleOrDefaultAsync<StudentResultCard>(
            ResultQueries.Get_StudentResultCard, parameters, commandType: CommandType.StoredProcedure);

        return new SingleResponseWrapper<StudentResultCard>
        {
            Data = result,
            Code = parameters.Get<int>("@Code"),
            Message = parameters.Get<string>("@Message")
        };
    }

    public async Task<ListResponseWrapper<StudentSubjectMark>> GetStudentResultMarksAsync(StudentCardRequest request)
    {
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@ResultSessionId", request.ResultSessionId, DbType.Int32);
        parameters.Add("@StudentId", request.StudentId, DbType.Int32);
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

        var result = await connection.QueryAsync<StudentSubjectMark>(
            ResultQueries.Get_StudentResultMarks, parameters, commandType: CommandType.StoredProcedure);
        var list = result.ToList();

        return new ListResponseWrapper<StudentSubjectMark>
        {
            Data = list,
            TotalCount = list.Count,
            Code = parameters.Get<int?>("@Code") ?? 0,
            Message = parameters.Get<string>("@Message") ?? string.Empty
        };
    }
}
