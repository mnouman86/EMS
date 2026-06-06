using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.Student;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Student;
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

public class StudentRepository : IStudentRepository
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<StudentRepository> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public StudentRepository(
        IConfiguration configuration,
        IMapper mapper,
        ILogger<StudentRepository> logger,
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

    public async Task<ResponseEntity> AddAsync(Student entity)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity);
        using var connection = OpenConnection();
        var dto = _mapper.Map<SubmitAdmissionDTO>(entity);
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            StudentQueries.Create_Student, dto, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }

    public async Task<ResponseEntity> UpdateAsync(Student entity)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity);
        using var connection = OpenConnection();
        var dto = _mapper.Map<UpdateStudentDTO>(entity);
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            StudentQueries.Update_Student, dto, commandType: CommandType.StoredProcedure);
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
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            StudentQueries.Delete_Student, parameters, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }

    public async Task<ListResponseWrapper<Student>> GetAllAsync(SearchRequest searchRequest)
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

        var result = await connection.QueryAsync<Student>(
            StudentQueries.GetAll_Student, parameters, commandType: CommandType.StoredProcedure);

        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

        return new ListResponseWrapper<Student>
        {
            Data = result.ToList(),
            TotalCount = parameters.Get<int?>("@TotalCount") ?? 0,
            Code = parameters.Get<int?>("@Code") ?? 0,
            Message = parameters.Get<string>("@Message") ?? string.Empty
        };
    }

    public async Task<SingleResponseWrapper<Student>> GetByIdAsync(SearchRequestById searchRequestById)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequestById);
        using var connection = OpenConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        parameters.Add("@CultureId", searchRequestById.CultureId, DbType.Int32);
        parameters.Add("@ID", searchRequestById.Id, DbType.Int32);

        var result = await connection.QuerySingleOrDefaultAsync<Student>(
            StudentQueries.GetById_Student, parameters, commandType: CommandType.StoredProcedure);

        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

        return new SingleResponseWrapper<Student>
        {
            Data = result,
            Code = parameters.Get<int>("@Code"),
            Message = parameters.Get<string>("@Message")
        };
    }

    public async Task<ResponseEntity> ChangeStatusAsync(ChangeStudentStatusDTO dto)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, dto);
        using var connection = OpenConnection();
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            StudentQueries.ChangeStatus_Student, dto, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }

    public async Task<ResponseEntity> BulkImportAsync(BulkImportStudentsDTO dto)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, dto);
        using var connection = OpenConnection();
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            StudentQueries.BulkImport_Students, dto, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }

    public async Task<ResponseEntity> PromoteAsync(PromoteStudentsDTO dto)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, dto);
        using var connection = OpenConnection();
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            StudentQueries.Promote_Students, dto, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }
}
