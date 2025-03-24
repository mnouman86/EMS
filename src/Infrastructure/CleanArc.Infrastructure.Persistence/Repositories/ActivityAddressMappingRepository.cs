using Azure.Core;
using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.ActivityAddressMapping;
using CleanArc.Application.Models.BusinessProfile;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.URL;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.ActivityAddressMapping;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;using CleanArc.Application.Common;

namespace CleanArc.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for handling operations related to menus.
/// </summary>
/// <seealso cref="CleanArc.Application.Contracts.Persistence.IMenuRepository" />
public class ActivityAddressMappingRepository:IActivityAddressMappingRepository
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
    private readonly ILogger<ActivityAddressMappingRepository> _logger;

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
    public ActivityAddressMappingRepository(IConfiguration configuration, IMapper mapper, ILogger<ActivityAddressMappingRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
/// <inheritdoc/>
public async Task<ResponseEntity> AddAsync(ActivityAddressMapping ActivityAddressMapping)
{
    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, ActivityAddressMapping))
    {
        using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
        {
            connection.Open();
                CreateActivityAddressMappingDTO createActivityAddressMappingDTO = _mapper.Map<CreateActivityAddressMappingDTO>(ActivityAddressMapping);
                var parameters = new DynamicParameters(createActivityAddressMappingDTO);
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ActivityAddressMappingQueries.Mapping_Create_Activity_Image, parameters, commandType: CommandType.StoredProcedure);
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
                parameters.Add("@UpdatedBy", updatedBy);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ActivityAddressMappingQueries.Mapping_Delete_Activity_Image, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                return result;
            }
        }
    }

    public async Task<ListResponseWrapper<ActivityAddressMapping>> GetAllAsync(SearchRequest searchRequest)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                parameters.Add("@CultureId", searchRequest.CultureId, DbType.Int32);
                var result = await connection.QueryAsync<ActivityAddressMapping>(ActivityAddressMappingQueries.GetByActivityID_ActivityAddress, parameters, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                var response = new ListResponseWrapper<ActivityAddressMapping> { Data = result.ToList(), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") };return response;
            }
        }
    }
    public Task<SingleResponseWrapper<ActivityAddressMapping>> GetByIdAsync(SearchRequestById searchRequestById)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseEntity> UpdateAsync(ActivityAddressMapping entity)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdateActivityAddressMappingDTO updateActivityAddressMappingDTO = _mapper.Map<UpdateActivityAddressMappingDTO>(entity);
                var parameters = new DynamicParameters(updateActivityAddressMappingDTO);
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ActivityAddressMappingQueries.Mapping_Update_ActivityAddress, parameters, commandType: CommandType.StoredProcedure);
                  (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);  
                return result;
            }
        }
    }
}

/// <inheritdoc/>


/// <inheritdoc/>

