using Azure.Core;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.ActivityIncludedOption;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.URL;
using CleanArc.Domain.Entities.ActivityIncludedOption;
using CleanArc.Domain.Entities.UserManagement;
using CleanArc.Infrastructure.Persistence.Helpers;
using CleanArc.Infrastructure.Sql;
using CleanArc.Infrastructure.Sql.SqlQueries;
using CleanArc.SharedKernel.Extensions;
using Dapper;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CleanArc.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for handling operations related to menus.
/// </summary>
/// <seealso cref="CleanArc.Application.Contracts.Persistence.IMenuRepository" />
public class ActivityIncludedOptionRepository:IActivityIncludedOptionRepository
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
    private readonly ILogger<ActivityIncludedOptionRepository> _logger;

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
    public ActivityIncludedOptionRepository(IConfiguration configuration, IMapper mapper, ILogger<ActivityIncludedOptionRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
/// <inheritdoc/>
public async Task<string> AddAsync(ActivityIncludedOption ActivityIncludedOption)
{
    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, ActivityIncludedOption))
    {
        using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
        {
            connection.Open();
                CreateActivityIncludedOptionDTO createActivityIncludedOptionDTO = _mapper.Map<CreateActivityIncludedOptionDTO>(ActivityIncludedOption);
            var result = await connection.ExecuteAsync(ActivityIncludedOptionQueries.Create_ActivityIncludedOption, createActivityIncludedOptionDTO, commandType: CommandType.StoredProcedure);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
            return result.ToString();
        }
    }
}

    public async Task<string> DeleteAsync(string selectedIds, int updatedBy, int? CultureId)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, new { selectedIds, updatedBy }))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@ID", selectedIds);
                parameters.Add("@UpdatedBy", updatedBy);
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = await connection.ExecuteAsync(ActivityIncludedOptionQueries.Delete_ActivityIncludedOption, parameters, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result.ToString();
            }
        }
    }

    public async Task<IReadOnlyList<ActivityIncludedOption>> GetAllAsync(SearchRequest searchRequest)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                var parameters = new
                {
                    PageNumber = searchRequest.PageNumber,
                    PageSize = searchRequest.PageSize,
                    //SortingColumnName = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnName,
                    //SortingColumnDirection = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnDirection,
                    //FilterParameterName = searchRequest.FilterArray?.FirstOrDefault()?.ParameterName,
                    //FilterParameterValue = searchRequest.FilterArray?.FirstOrDefault()?.ParameterValue
                    SortingArray = DataTableHelper.ToDataTable(searchRequest.SortingArray), // Convert list to DataTable
                    FilterArray = DataTableHelper.ToDataTable(searchRequest.FilterArray) // Convert list to DataTable
                };
                var result = await connection.QueryAsync<ActivityIncludedOption>(ActivityIncludedOptionQueries.usp_GetAll_ActivityIncludedOption, parameters, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result.ToList();
            }
        }
    }
    public async Task<ActivityIncludedOption> GetByIdAsync(long id)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, id))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<ActivityIncludedOption>(ActivityIncludedOptionQueries.usp_GetByID_ActivityIncludedOption, new { ID = id }, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }



    public async Task<string> UpdateAsync(ActivityIncludedOption entity)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdateActivityIncludedOptionDTO updateActivityIncludedOptionDTO = _mapper.Map<UpdateActivityIncludedOptionDTO>(entity);

                var result = await connection.ExecuteAsync(ActivityIncludedOptionQueries.update_ActivityIncludedOption, updateActivityIncludedOptionDTO, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result.ToString();
            }
        }
    }
}

/// <inheritdoc/>


/// <inheritdoc/>

