using Azure.Core;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Activities;
using CleanArc.Application.Models.ActivitySupervisor;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.URL;
using CleanArc.Domain.Entities.ActivitySupervisor;
using CleanArc.Domain.Entities.UserManagement;
using CleanArc.Infrastructure.Persistence.Helpers;
using CleanArc.Infrastructure.Sql;
using CleanArc.Infrastructure.Sql.SqlQueries;
using CleanArc.SharedKernel.Extensions;
using Dapper;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;using CleanArc.Application.Common;

namespace CleanArc.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for handling operations related to menus.
/// </summary>
/// <seealso cref="CleanArc.Application.Contracts.Persistence.IMenuRepository" />
public class ActivitySupervisorRepository:IActivitySupervisorRepository
{
    /// <summary>
    /// The configuration for accessing application settings.
    /// </summary>
    private readonly IConfiguration configuration;

    /// <summary>
    /// The mapper for mapping between different object types.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// The logger for logging repository-related information.
    /// </summary>
    private readonly ILogger<ActivitySupervisorRepository> _logger;

    /// <summary>
    /// The HTTP context accessor for accessing HTTP context information.
    /// </summary>
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuRepository"/> class.
    /// </summary>
    /// <param name="configuration">The configuration for accessing application settings.</param>
    /// <param name="mapper">The mapper for mapping between different object types.</param>
    /// <param name="logger">The logger for logging repository-related information.</param>
    /// <param name="httpContextAccessor">The HTTP context accessor for accessing HTTP context information.</param>
    public ActivitySupervisorRepository(IConfiguration configuration, IMapper mapper, ILogger<ActivitySupervisorRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
/// <inheritdoc/>
public async Task<ResponseEntity> AddAsync(ActivitySupervisor ActivitySupervisor)
{
    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, ActivitySupervisor))
    {
        using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
        {
         
                connection.Open();
                CreateActivitySupervisorDTO createActivitySupervisorDTO = _mapper.Map<CreateActivitySupervisorDTO>(ActivitySupervisor);
                var parameters = new DynamicParameters(createActivitySupervisorDTO);
                //parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
              //  parameters.Add("@ActivityID ", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ActivitySupervisorQueries.Create_ActivitySupervisor, parameters, commandType: CommandType.StoredProcedure);
             (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
            return result;
        }
    }
}

    public async Task<ResponseEntity> DeleteAsync(DeleteRequest deleteRequest, int? updatedBy)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, deleteRequest))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@Ids", deleteRequest.SelectedIds);
                parameters.Add("@CultureId", deleteRequest.CultureId);
                parameters.Add("@IsDeleted", deleteRequest.isDeleted);
                parameters.Add("@UpdatedBy", updatedBy);
                //parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ActivitySupervisorQueries.Delete_ActivitySupervisor, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                return result;
            }
        }
    }

    public async Task<ListResponseWrapper<ActivitySupervisor>> GetAllAsync(SearchRequest searchRequest)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
                parameters.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
                parameters.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
                //parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object); // Ensure proper type
                //parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object); // Ensure proper type
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                var result = await connection.QueryAsync<ActivitySupervisor>(ActivitySupervisorQueries.GetAll_Manager, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                var response = new ListResponseWrapper<ActivitySupervisor> { Data = result.ToList(), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") };return response;
            }
        }
    }
    public async Task<SingleResponseWrapper<ActivitySupervisor>> GetByIdAsync(SearchRequestById searchRequestById)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequestById))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                parameters.Add("@CultureId", searchRequestById.CultureId, DbType.Int32);
                parameters.Add("@ID", searchRequestById.Id, DbType.Int32);
                var result = await connection.QuerySingleOrDefaultAsync<ActivitySupervisor>(ActivitySupervisorQueries.GetByID_Manager, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                var response = new SingleResponseWrapper<ActivitySupervisor>
                {
                    Data = result,
                    Code = parameters.Get<int>("@Code"),
                    Message = parameters.Get<string>("@Message")
                };
                return response;
            }
        }
    }



    public async Task<ResponseEntity> UpdateAsync(ActivitySupervisor entity)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdateActivitySupervisorDTO updateActivitySupervisorDTO = _mapper.Map<UpdateActivitySupervisorDTO>(entity);
                var parameters = new DynamicParameters(updateActivitySupervisorDTO);
                //parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
               // parameters.Add("@ActivityID ", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ActivitySupervisorQueries.Update_ActivitySupervisor, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                return result;
            }
        }
    }
}

/// <inheritdoc/>


/// <inheritdoc/>

