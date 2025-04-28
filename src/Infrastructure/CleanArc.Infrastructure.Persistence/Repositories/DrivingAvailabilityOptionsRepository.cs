using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.DrivingAvailabilityOptions;
using CleanArc.Domain.Entities.RoomSizeUnit;
using CleanArc.Domain.Entities.DrivingAvailabilityOptions;
using CleanArc.Infrastructure.Persistence.Helpers;
using CleanArc.Infrastructure.Sql.SqlQueries;
using CleanArc.SharedKernel.Extensions;
using Dapper;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; 
using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Common;
using CleanArc.Domain.Entities.AdvertisementPage;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class DrivingAvailabilityOptionsRepository : IDrivingAvailabilityOptionsRepository
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
    private readonly ILogger<DrivingAvailabilityOptionsRepository> _logger;

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
    public DrivingAvailabilityOptionsRepository(IConfiguration configuration, IMapper mapper, ILogger<DrivingAvailabilityOptionsRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    /// <inheritdoc/>
    public async Task<ResponseEntity> AddAsync(DrivingAvailabilityOptions DrivingAvailabilityOptions)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, DrivingAvailabilityOptions))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                CreateDrivingAvailabilityOptionsDTO createDrivingAvailabilityOptionsDTO = _mapper.Map<CreateDrivingAvailabilityOptionsDTO>(DrivingAvailabilityOptions);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(DrivingAvailabilityOptionsQueries.Create_DrivingAvailabilityOptions, createDrivingAvailabilityOptionsDTO, commandType: CommandType.StoredProcedure);
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
                parameters.Add("@UpdatedBy", updatedBy);
				parameters.Add("@IsDeleted", deleteRequest.isDeleted);
                parameters.Add("@CultureId", deleteRequest.CultureId);
				var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(DrivingAvailabilityOptionsQueries.Delete_DrivingAvailabilityOptions, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }

    public async Task<ListResponseWrapper<DrivingAvailabilityOptions>> GetAllAsync(SearchRequest searchRequest)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
				var parameters = new DynamicParameters();

				parameters.Add("@PageNumber", searchRequest.PageNumber);
				parameters.Add("@PageSize", searchRequest.PageSize);
				parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), dbType: DbType.Object);
				parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), dbType: DbType.Object);
				parameters.Add("@CultureId", searchRequest.CultureId, DbType.Int32);
				parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
				parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
				var result = await connection.QueryAsync<DrivingAvailabilityOptions>(DrivingAvailabilityOptionsQueries.GetALL_DrivingAvailabilityOptions, parameters, commandType: CommandType.StoredProcedure);
                
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
				var response = new ListResponseWrapper<DrivingAvailabilityOptions>
                {
                    Data = result.ToList(),
                    Code = parameters.Get<int>("@Code"),
                    Message = parameters.Get<string>("@Message")
                }; 
                return response;

			}
		}
    }
    public async Task<SingleResponseWrapper<DrivingAvailabilityOptions>> GetByIdAsync(SearchRequestById searchRequestById)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequestById))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
				var parameters = new DynamicParameters();
				parameters.Add("@ID", searchRequestById.Id, DbType.Int32);
                parameters.Add("@CultureId", searchRequestById.CultureId, DbType.Int32);
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                var result = await connection.QuerySingleOrDefaultAsync<DrivingAvailabilityOptions>(DrivingAvailabilityOptionsQueries.GetByID_DrivingAvailabilityOptions, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                var response = new SingleResponseWrapper<DrivingAvailabilityOptions>
                {
                    Data = result,
                    Code = parameters.Get<int>("@Code"),
                    Message = parameters.Get<string>("@Message")
                };
                return response;
            }
        }
    }



    public async Task<ResponseEntity> UpdateAsync(DrivingAvailabilityOptions DrivingAvailabilityOptions)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, DrivingAvailabilityOptions))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdateDrivingAvailabilityOptionsDTO updateDrivingAvailabilityOptionsDTO = _mapper.Map<UpdateDrivingAvailabilityOptionsDTO>(DrivingAvailabilityOptions);

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(DrivingAvailabilityOptionsQueries.Update_DrivingAvailabilityOptions, updateDrivingAvailabilityOptionsDTO, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }
}
