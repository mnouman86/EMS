using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.AgeType;
using CleanArc.Application.Models.RatePlanType;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.AgeType;
using CleanArc.Domain.Entities.RatePlanType;
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
using CleanArc.Domain.Entities.City;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class RatePlanTypeRepository : IRatePlanTypeRepository 
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
    private readonly ILogger<RatePlanTypeRepository> _logger;

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
    public RatePlanTypeRepository(IConfiguration configuration, IMapper mapper, ILogger<RatePlanTypeRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResponseEntity> AddAsync(RatePlanType RatePlanType)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, RatePlanType))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                CreateRatePlanTypeDTO createRatePlanTypeDTO = _mapper.Map<CreateRatePlanTypeDTO>(RatePlanType);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(RatePlanTypeQueries.Create_RatePlanType, createRatePlanTypeDTO, commandType: CommandType.StoredProcedure);
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
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(RatePlanTypeQueries.Delete_RatePlanType, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                //
                return result;
            }
        }
    }

    public async Task<ListResponseWrapper<RatePlanType>> GetAllAsync(SearchRequest searchRequest)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
				var parameters = new DynamicParameters();
				parameters.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
				if (searchRequest.PageSize > 0) parameters.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
				parameters.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
				parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object); // Ensure proper type
				parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object); // Ensure proper type
                parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
				parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
				var result = await connection.QueryAsync<RatePlanType>(RatePlanTypeQueries.GetALL_RatePlanType, parameters, commandType: CommandType.StoredProcedure);
      

				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
				var response = new ListResponseWrapper<RatePlanType> { Data = result.ToList(), TotalCount = parameters.Get<int>("@TotalCount"), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") }; return response;

			}
		}
    }
    public async Task<SingleResponseWrapper<RatePlanType>> GetByIdAsync(SearchRequestById searchRequestById)
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
				var result = await connection.QuerySingleOrDefaultAsync<RatePlanType>(RatePlanTypeQueries.GetByID_RatePlanType, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                var response = new SingleResponseWrapper<RatePlanType>
                {
                    Data = result,
                    Code = parameters.Get<int>("@Code"),
                    Message = parameters.Get<string>("@Message")
                };
                return response;
            }
        }
    }



    public async Task<ResponseEntity> UpdateAsync(RatePlanType RatePlanType)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, RatePlanType))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdateRatePlanTypeDTO updateRatePlanTypeDTO = _mapper.Map<UpdateRatePlanTypeDTO>(RatePlanType);

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(RatePlanTypeQueries.Update_RatePlanType, updateRatePlanTypeDTO, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }
}

