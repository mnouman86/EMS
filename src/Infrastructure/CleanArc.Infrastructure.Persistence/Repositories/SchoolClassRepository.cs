using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.SchoolClass;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.SchoolClass;
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

public class SchoolClassRepository : ISchoolClassRepository
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<SchoolClassRepository> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SchoolClassRepository(
        IConfiguration configuration,
        IMapper mapper,
        ILogger<SchoolClassRepository> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _mapper = mapper;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResponseEntity> AddAsync(SchoolClass entity)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity);
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection1"));
        connection.Open();
        var dto = _mapper.Map<CreateSchoolClassDTO>(entity);
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            SchoolClassQueries.Create_SchoolClass, dto, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }

    public async Task<ResponseEntity> UpdateAsync(SchoolClass entity)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity);
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection1"));
        connection.Open();
        var dto = _mapper.Map<UpdateSchoolClassDTO>(entity);
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            SchoolClassQueries.Update_SchoolClass, dto, commandType: CommandType.StoredProcedure);
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
            SchoolClassQueries.Delete_SchoolClass, parameters, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }

    public async Task<ListResponseWrapper<SchoolClass>> GetAllAsync(SearchRequest searchRequest)
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

        var result = await connection.QueryAsync<SchoolClass>(
            SchoolClassQueries.GetAll_SchoolClass, parameters, commandType: CommandType.StoredProcedure);

        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

        return new ListResponseWrapper<SchoolClass>
        {
            Data = result.ToList(),
            TotalCount = parameters.Get<int?>("@TotalCount") ?? 0,
            Code = parameters.Get<int?>("@Code") ?? 0,
            Message = parameters.Get<string>("@Message") ?? string.Empty
        };
    }

    public async Task<SingleResponseWrapper<SchoolClass>> GetByIdAsync(SearchRequestById searchRequestById)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequestById);
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection1"));
        connection.Open();
        var parameters = new DynamicParameters();
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        parameters.Add("@CultureId", searchRequestById.CultureId, DbType.Int32);
        parameters.Add("@ID", searchRequestById.Id, DbType.Int32);

        var result = await connection.QuerySingleOrDefaultAsync<SchoolClass>(
            SchoolClassQueries.GetById_SchoolClass, parameters, commandType: CommandType.StoredProcedure);

        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

        return new SingleResponseWrapper<SchoolClass>
        {
            Data = result,
            Code = parameters.Get<int>("@Code"),
            Message = parameters.Get<string>("@Message")
        };
    }

    public async Task<ResponseEntity> AssignClassTeacherAsync(AssignClassTeacherDTO dto)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, dto);
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection1"));
        connection.Open();
        var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            SchoolClassQueries.AssignTeacher_SchoolClass, dto, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return result;
    }
}
