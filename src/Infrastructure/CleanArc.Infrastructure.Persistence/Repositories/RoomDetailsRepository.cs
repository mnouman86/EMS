using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.RoomDetails;
using CleanArc.Domain.Entities.RoomDetails;
using CleanArc.Infrastructure.Persistence.Helpers;
using CleanArc.Infrastructure.Sql.SqlQueries;
using CleanArc.SharedKernel.Extensions;
using Dapper;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Common;
using CleanArc.Domain.Entities.CoreArea;
using CleanArc.Domain.Entities.ActivitySchedule;
using CleanArc.Application.Models.RoomType;
using CleanArc.Application.Models.CoreArea;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class RoomDetailsRepository : IRoomDetailsRepository
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
	private readonly ILogger<RoomDetailsRepository> _logger;

	/// <summary>
	/// The HTTP context accessor for accessing HTTP context information.
	/// </summary>
	private readonly IHttpContextAccessor _httpContextAccessor;
	public RoomDetailsRepository(IConfiguration configuration, IMapper mapper, ILogger<RoomDetailsRepository> logger, IHttpContextAccessor httpContextAccessor)
	{
		this.configuration = configuration;
		this._mapper = mapper;
		this._logger = logger;
		_httpContextAccessor = httpContextAccessor;
	}
	public async Task<ResponseEntity> AddAsync(RoomDetails roomDetails)
	{
		using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, roomDetails))
		{
			using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
			{
				connection.Open();
				CreateRoomDetailsDTO createRoomDetailsDTO = _mapper.Map<CreateRoomDetailsDTO>(roomDetails);
				var parameters = new DynamicParameters(createRoomDetailsDTO);

				parameters.Add("@cultureId", 1);
				parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
				parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
			

				var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(RoomDetailQueries.Create_RoomDetail, parameters, commandType: CommandType.StoredProcedure);
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
				return result;
			}
		}
	}

	public async Task<ResponseEntity> DeleteAsync(string selectedIds, int updatedBy, int? CultureId)
	{
		using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, new { selectedIds, updatedBy }))
		{
			using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
			{
				connection.Open();
				var parameters = new DynamicParameters();
				parameters.Add("@ID", selectedIds);
				parameters.Add("@CultureId", CultureId);
				parameters.Add("@UpdatedBy", updatedBy);
				parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
				parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

				var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(RoomDetailQueries.Delete_RoomDetail, parameters, commandType: CommandType.StoredProcedure);
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
				return result;
			}
		}
	}

	public async Task<ListResponseWrapper<RoomDetails>> GetAllAsync(SearchRequest searchRequest)
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
				parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object); // Ensure proper type
				parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object); // Ensure proper type
				parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
				parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
				var result = await connection.QueryAsync<RoomDetails>(RoomDetailQueries.usp_GetALL_RoomDetail, parameters, commandType: CommandType.StoredProcedure);


				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
				var response = new ListResponseWrapper<RoomDetails> { Data = result.ToList(), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") }; return response;

			}
		}
	}
	public async Task<SingleResponseWrapper<RoomDetails>> GetByIdAsync(long id)
	{
		using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, id))
		{
			using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
			{
				connection.Open();
				var parameters = new DynamicParameters();
				parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
				parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
				parameters.Add("@CultureId", 1, DbType.Int32);
				parameters.Add("@ID", id, DbType.Int32);



				var result = await connection.QuerySingleOrDefaultAsync<RoomDetails>(RoomDetailQueries.usp_GetByID_RoomDetail, parameters, commandType: CommandType.StoredProcedure);
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
				//if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
				var response = new SingleResponseWrapper<RoomDetails>
				{
					Data = result,
					Code = parameters.Get<int>("@Code"),
					Message = parameters.Get<string>("@Message")
				};
				return response;
			}
		}
	}



	public async Task<ResponseEntity> UpdateAsync(RoomDetails roomDetails)
	{
		using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, roomDetails))
		{
			using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
			{
				connection.Open();
				UpdateRoomDetailsDTO updateRoomDetailsDTO = _mapper.Map<UpdateRoomDetailsDTO>(roomDetails);
				var parameters = new DynamicParameters(updateRoomDetailsDTO);
				parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
				parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
				parameters.Add("@CultureId", 1, DbType.Int32);

				var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(RoomDetailQueries.Update_RoomDetail, parameters, commandType: CommandType.StoredProcedure);
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
				return result;
			}
		}
	}
}



