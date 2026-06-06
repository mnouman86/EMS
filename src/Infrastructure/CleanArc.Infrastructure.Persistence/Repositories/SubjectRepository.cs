using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.Subject;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Subject;
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

public class SubjectRepository : ISubjectRepository
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<SubjectRepository> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SubjectRepository(
        IConfiguration configuration,
        IMapper mapper,
        ILogger<SubjectRepository> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _mapper = mapper;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResponseEntity> AddAsync(Subject entity)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity);
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection1"));
        connection.Open();
        var dto = _mapper.Map<CreateSubjectDTO>(entity);
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            SubjectQueries.Create_Subject, dto, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }

    public async Task<ResponseEntity> UpdateAsync(Subject entity)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity);
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection1"));
        connection.Open();
        var dto = _mapper.Map<UpdateSubjectDTO>(entity);
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            SubjectQueries.Update_Subject, dto, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }

    public async Task<ResponseEntity> DeleteAsync(DeleteRequest deleteRequest, int? updatedBy)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, deleteRequest);
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection1"));
        connection.Open();
        var parameters = new DynamicParameters();
        parameters.Add("@Ids", deleteRequest.SelectedIds);
        parameters.Add("@UpdatedBy", updatedBy);
        parameters.Add("@IsDeleted", deleteRequest.isDeleted);
        parameters.Add("@CultureId", deleteRequest.CultureId);
        parameters.Add("@ForceHard", deleteRequest.ForceHard);
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            SubjectQueries.Delete_Subject, parameters, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }

    public async Task<ListResponseWrapper<Subject>> GetAllAsync(SearchRequest searchRequest)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest);
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection1"));
        connection.Open();
        var parameters = new DynamicParameters();
        parameters.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
        if (searchRequest.PageSize > 0) parameters.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
        parameters.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
        parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object);
        parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object);
        parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

        var result = await connection.QueryAsync<Subject>(
            SubjectQueries.GetAll_Subject, parameters, commandType: CommandType.StoredProcedure);

        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

        return new ListResponseWrapper<Subject>
        {
            Data = result.ToList(),
            TotalCount = parameters.Get<int?>("@TotalCount") ?? 0,
            Code = parameters.Get<int?>("@Code") ?? 0,
            Message = parameters.Get<string>("@Message") ?? string.Empty
        };
    }

    public async Task<SingleResponseWrapper<Subject>> GetByIdAsync(SearchRequestById searchRequestById)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequestById);
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection1"));
        connection.Open();
        var parameters = new DynamicParameters();
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        parameters.Add("@CultureId", searchRequestById.CultureId, DbType.Int32);
        parameters.Add("@ID", searchRequestById.Id, DbType.Int32);

        var result = await connection.QuerySingleOrDefaultAsync<Subject>(
            SubjectQueries.GetById_Subject, parameters, commandType: CommandType.StoredProcedure);

        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

        return new SingleResponseWrapper<Subject>
        {
            Data = result,
            Code = parameters.Get<int>("@Code"),
            Message = parameters.Get<string>("@Message")
        };
    }

    public async Task<ResponseEntity> MapSubjectsToClassAsync(MapSubjectsToClassDTO dto)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, dto);
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection1"));
        connection.Open();
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            SubjectQueries.MapSubjects_ToClass, dto, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }

    public async Task<ListResponseWrapper<SchoolClassSubject>> GetSubjectsByClassAsync(SearchRequestById request)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection1"));
        connection.Open();
        var parameters = new DynamicParameters();
        parameters.Add("@SchoolClassId", request.Id, DbType.Int32);
        parameters.Add("@CultureId", request.CultureId, DbType.Int32);
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

        var result = await connection.QueryAsync<SchoolClassSubject>(
            SubjectQueries.GetSubjects_ByClass, parameters, commandType: CommandType.StoredProcedure);

        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

        return new ListResponseWrapper<SchoolClassSubject>
        {
            Data = result.ToList(),
            TotalCount = result.Count(),
            Code = parameters.Get<int?>("@Code") ?? 0,
            Message = parameters.Get<string>("@Message") ?? string.Empty
        };
    }
}
