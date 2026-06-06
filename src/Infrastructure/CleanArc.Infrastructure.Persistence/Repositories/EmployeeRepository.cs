using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Employee;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Employee;
using CleanArc.Infrastructure.Persistence.Helpers;
using CleanArc.Infrastructure.Sql.SqlQueries;
using CleanArc.SharedKernel.Extensions;
using Dapper;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<EmployeeRepository> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EmployeeRepository(
        IConfiguration configuration,
        IMapper mapper,
        ILogger<EmployeeRepository> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _mapper = mapper;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    private SqlConnection OpenConnection()
    {
        var connection = new SqlConnection(_configuration.GetConnectionString("DBConnection1"));
        connection.Open();
        return connection;
    }

    public async Task<ResponseEntity> AddAsync(Employee entity)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity);
        using var connection = OpenConnection();
        var dto = _mapper.Map<CreateEmployeeDTO>(entity);
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            EmployeeQueries.Create_Employee, dto, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }

    public async Task<ResponseEntity> UpdateAsync(Employee entity)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity);
        using var connection = OpenConnection();
        var dto = _mapper.Map<UpdateEmployeeDTO>(entity);
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            EmployeeQueries.Update_Employee, dto, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }

    public async Task<ResponseEntity> DeleteAsync(DeleteRequest deleteRequest, int? updatedBy)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, deleteRequest);
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@Ids", deleteRequest.SelectedIds);
        parameters.Add("@UpdatedBy", updatedBy);
        parameters.Add("@IsDeleted", deleteRequest.isDeleted);
        parameters.Add("@CultureId", deleteRequest.CultureId);
        parameters.Add("@ForceHard", deleteRequest.ForceHard);
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            EmployeeQueries.Delete_Employee, parameters, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }

    public async Task<ListResponseWrapper<Employee>> GetAllAsync(SearchRequest searchRequest)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest);
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
        if (searchRequest.PageSize > 0) parameters.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
        parameters.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
        parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object);
        parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object);
        parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

        var result = await connection.QueryAsync<Employee>(
            EmployeeQueries.GetAll_Employee, parameters, commandType: CommandType.StoredProcedure);

        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

        return new ListResponseWrapper<Employee>
        {
            Data = result.ToList(),
            TotalCount = parameters.Get<int?>("@TotalCount") ?? 0,
            Code = parameters.Get<int?>("@Code") ?? 0,
            Message = parameters.Get<string>("@Message") ?? string.Empty
        };
    }

    public async Task<SingleResponseWrapper<Employee>> GetByIdAsync(SearchRequestById searchRequestById)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequestById);
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        parameters.Add("@CultureId", searchRequestById.CultureId, DbType.Int32);
        parameters.Add("@ID", searchRequestById.Id, DbType.Int32);

        var result = await connection.QuerySingleOrDefaultAsync<Employee>(
            EmployeeQueries.GetById_Employee, parameters, commandType: CommandType.StoredProcedure);

        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

        return new SingleResponseWrapper<Employee>
        {
            Data = result,
            Code = parameters.Get<int>("@Code"),
            Message = parameters.Get<string>("@Message")
        };
    }

    public async Task<ResponseEntity> MarkAsLeftAsync(MarkEmployeeLeftDTO dto)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, dto);
        using var connection = OpenConnection();
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            EmployeeQueries.MarkLeft_Employee, dto, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }

    public async Task<ResponseEntity> AssignTeacherSubjectsAsync(AssignTeacherSubjectsDTO dto)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, dto);
        using var connection = OpenConnection();
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            EmployeeQueries.AssignTeacherSubjects, dto, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }

    public async Task<ListResponseWrapper<TeacherAssignment>> GetTeacherAssignmentsAsync(SearchRequestById request)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", request.Id, DbType.Int32);
        parameters.Add("@CultureId", request.CultureId, DbType.Int32);
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

        var result = await connection.QueryAsync<TeacherAssignment>(
            EmployeeQueries.GetTeacherAssignments, parameters, commandType: CommandType.StoredProcedure);

        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

        return new ListResponseWrapper<TeacherAssignment>
        {
            Data = result.ToList(),
            TotalCount = result.Count(),
            Code = parameters.Get<int?>("@Code") ?? 0,
            Message = parameters.Get<string>("@Message") ?? string.Empty
        };
    }

    public async Task<ResponseEntity> AddDocumentAsync(UploadEmployeeDocumentDTO dto)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, dto);
        using var connection = OpenConnection();
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            EmployeeQueries.AddEmployeeDocument, dto, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }

    public async Task<ListResponseWrapper<EmployeeDocument>> GetDocumentsAsync(SearchRequestById request)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", request.Id, DbType.Int32);
        parameters.Add("@CultureId", request.CultureId, DbType.Int32);
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

        var result = await connection.QueryAsync<EmployeeDocument>(
            EmployeeQueries.GetEmployeeDocuments, parameters, commandType: CommandType.StoredProcedure);

        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

        return new ListResponseWrapper<EmployeeDocument>
        {
            Data = result.ToList(),
            TotalCount = result.Count(),
            Code = parameters.Get<int?>("@Code") ?? 0,
            Message = parameters.Get<string>("@Message") ?? string.Empty
        };
    }

    /* ---------- Foundational extensions: salary + advance ---------- */

    public async Task<ResponseEntity> UpsertSalaryAsync(UpsertEmployeeSalaryDTO dto)
    {
        using var connection = OpenConnection();
        return await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            EmployeeQueries.Upsert_EmployeeSalary, dto, commandType: CommandType.StoredProcedure);
    }

    public async Task<SingleResponseWrapper<EmployeeSalary>> GetCurrentSalaryAsync(SearchRequestById request)
    {
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", request.Id, DbType.Int32);
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        var data = await connection.QuerySingleOrDefaultAsync<EmployeeSalary>(
            EmployeeQueries.Get_CurrentEmployeeSalary, parameters, commandType: CommandType.StoredProcedure);
        return new SingleResponseWrapper<EmployeeSalary>
        {
            Data = data,
            Code = parameters.Get<int>("@Code"),
            Message = parameters.Get<string>("@Message")
        };
    }

    public async Task<ListResponseWrapper<EmployeeSalary>> GetSalaryHistoryAsync(SearchRequestById request)
    {
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", request.Id, DbType.Int32);
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        var rows = await connection.QueryAsync<EmployeeSalary>(
            EmployeeQueries.Get_EmployeeSalaryHistory, parameters, commandType: CommandType.StoredProcedure);
        var list = rows.ToList();
        return new ListResponseWrapper<EmployeeSalary>
        {
            Data = list,
            TotalCount = list.Count,
            Code = parameters.Get<int?>("@Code") ?? 0,
            Message = parameters.Get<string>("@Message") ?? string.Empty
        };
    }

    public async Task<ResponseEntity> IssueAdvanceAsync(IssueEmployeeAdvanceDTO dto)
    {
        using var connection = OpenConnection();
        return await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            EmployeeQueries.Issue_EmployeeAdvance, dto, commandType: CommandType.StoredProcedure);
    }

    public async Task<ResponseEntity> AdjustAdvanceAsync(AdjustEmployeeAdvanceDTO dto)
    {
        using var connection = OpenConnection();
        return await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            EmployeeQueries.Adjust_EmployeeAdvance, dto, commandType: CommandType.StoredProcedure);
    }

    public async Task<ListResponseWrapper<EmployeeAdvance>> GetAdvancesAsync(SearchRequestById request)
    {
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", request.Id, DbType.Int32);
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        var rows = await connection.QueryAsync<EmployeeAdvance>(
            EmployeeQueries.Get_EmployeeAdvances, parameters, commandType: CommandType.StoredProcedure);
        var list = rows.ToList();
        return new ListResponseWrapper<EmployeeAdvance>
        {
            Data = list,
            TotalCount = list.Count,
            Code = parameters.Get<int?>("@Code") ?? 0,
            Message = parameters.Get<string>("@Message") ?? string.Empty
        };
    }
}
