using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.RoomDiscountByUserType;
using CleanArc.Domain.Entities.RoomDiscountByUserType;
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
using CleanArc.Domain.Entities.ServiceCategory;
using CleanArc.Application.Models.CoreArea;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class RoomDiscountByUserTypeRepository : IRoomDiscountByUserTypeRepository
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
    private readonly ILogger<RoomDiscountByUserTypeRepository> _logger;

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
    public RoomDiscountByUserTypeRepository(IConfiguration configuration, IMapper mapper, ILogger<RoomDiscountByUserTypeRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<ResponseEntity> AddAsync(RoomDiscountByUserType roomDiscountByUserType)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, roomDiscountByUserType))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                CreateRoomDiscountByUserTypeDTO createRoomDiscountByUserTypeDTO = _mapper.Map<CreateRoomDiscountByUserTypeDTO>(roomDiscountByUserType);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(RoomDiscountByUserTypeQueries.Create_RoomDiscountByUserType, createRoomDiscountByUserTypeDTO, commandType: CommandType.StoredProcedure);
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
				
				var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(RoomDiscountByUserTypeQueries.Delete_RoomDiscountByUserType, parameters, commandType: CommandType.StoredProcedure);
               

				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                //
				return result;
			}
        }
    }

    public async Task<ListResponseWrapper<RoomDiscountByUserType>> GetAllAsync(SearchRequest searchRequest)
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
				var result = await connection.QueryAsync<RoomDiscountByUserType>(RoomDiscountByUserTypeQueries.GetALL_RoomDiscountByUserType, parameters, commandType: CommandType.StoredProcedure);

				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
				var response = new ListResponseWrapper<RoomDiscountByUserType> { Data = result.ToList(), TotalCount = parameters.Get<int>("@TotalCount"), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") }; return response;
			}
        }
    }
    public async Task<SingleResponseWrapper<RoomDiscountByUserType>> GetByIdAsync(SearchRequestById searchRequestById)
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
				var result = await connection.QuerySingleOrDefaultAsync<RoomDiscountByUserType>(RoomDiscountByUserTypeQueries.GetByID_RoomDiscountByUserType, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                var response = new SingleResponseWrapper<RoomDiscountByUserType>
                {
                    Data = result,
                    Code = parameters.Get<int>("@Code"),
                    Message = parameters.Get<string>("@Message")
                };
                return response;
            }
        }
    }



    public async Task<ResponseEntity> UpdateAsync(RoomDiscountByUserType roomDiscountByUserType)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, roomDiscountByUserType))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdateRoomDiscountByUserTypeDTO updateRoomDiscountByUserTypeDTO = _mapper.Map<UpdateRoomDiscountByUserTypeDTO>(roomDiscountByUserType);
				var parameters = new DynamicParameters(updateRoomDiscountByUserTypeDTO);
				var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(RoomDiscountByUserTypeQueries.Update_RoomDiscountByUserType, parameters, commandType: CommandType.StoredProcedure);

				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                //
				return result;
			}
        }
    }
}


