using Azure.Core;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Activities;
using CleanArc.Application.Models.BusinessProfile;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.URL;
using CleanArc.Domain.Entities.Activity;
using CleanArc.Domain.Entities.BusinessProfile;
using CleanArc.Domain.Entities.SearchHotelRoomDetail;
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
using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using CleanArc.Application.Common;
using CleanArc.Application.Features.Activity.Queries.GetActivityCheckoutDetail;

namespace CleanArc.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for handling operations related to menus.
/// </summary>
/// <seealso cref="CleanArc.Application.Contracts.Persistence.IMenuRepository" />
public class ActivityRepository : IActivityRepository
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
	private readonly ILogger<ActivityRepository> _logger;

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
	public ActivityRepository(IConfiguration configuration, IMapper mapper, ILogger<ActivityRepository> logger, IHttpContextAccessor httpContextAccessor)
	{
		this.configuration = configuration;
		this._mapper = mapper;
		this._logger = logger;
		_httpContextAccessor = httpContextAccessor;
	}
	/// <inheritdoc/>
	public async Task<ResponseEntity> AddAsync(Activity Activity)
	{
		using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, Activity))
		{
			using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
			{
				connection.Open();
				CreateActivityDTO createActivityDTO = _mapper.Map<CreateActivityDTO>(Activity);
				var parameters = new DynamicParameters(createActivityDTO);
				//parameters.AddDynamicParams(createActivityDTO);
				
				// var result = await connection.ExecuteScalarAsync(ActivityQueries.Create_Activity, parameters, commandType: CommandType.StoredProcedure);
				//var result = await connection.QuerySingleOrDefaultAsync<int>(ActivityQueries.Create_Activity, parameters, commandType: CommandType.StoredProcedure);
				var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ActivityQueries.Create_Activity, parameters, commandType: CommandType.StoredProcedure);

				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); //if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
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
				
				var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ActivityQueries.Delete_Activity, parameters, commandType: CommandType.StoredProcedure);
				// var result = await connection.QuerySingleOrDefaultAsync<int>(ActivityQueries.Delete_Activity, parameters, commandType: CommandType.StoredProcedure);

				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
				//if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
				return result;
			}
		}
	}

	public async Task<SingleResponseWrapper<Activity>> GetActivityCheckoutDetailAsync(GetActivityCheckoutDetailQuery searchRequestById)
	{
		using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequestById))
		{
			using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
			{

				connection.Open();
				var parameters = new DynamicParameters();
				parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
				parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
				parameters.Add("@CultureId", searchRequestById.searchRequestById.CultureId, DbType.Int32);
				parameters.Add("@ID", searchRequestById.searchRequestById.Id, DbType.Int32);
				parameters.Add("@isCheckOut", true, DbType.Boolean);
				parameters.Add("@userID", searchRequestById.UserId, DbType.Int32);

				var result = await connection.QuerySingleOrDefaultAsync<Activity>(ActivityQueries.GetByID_Activity, parameters, commandType: CommandType.StoredProcedure);
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

				var response = new SingleResponseWrapper<Activity>
				{
					Data = result,
					Code = parameters.Get<int>("@Code"),
					Message = parameters.Get<string>("@Message")
				};
				return response;
			}
		}
	}

	public async Task<ListResponseWrapper<Activity>> GetAllAsync(SearchRequest searchRequest)
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


				var result = await connection.QueryAsync<Activity>(ActivityQueries.GetAll_Activity, parameters, commandType: CommandType.StoredProcedure);
				foreach (var item in result)
				{
					var imageParams = new DynamicParameters();
					imageParams.Add("@ID", item.ID, DbType.Int32);
					imageParams.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
					imageParams.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
					imageParams.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

					var imageList = await connection.QueryAsync<ActivityIDImageMapping>(ActivityIDImageMappingQueries.Mapping_GetByActivityID_Activity_Image, imageParams, commandType: CommandType.StoredProcedure);
					var AddressList = await connection.QueryAsync<ActivityAddressMapping>(ActivityAddressMappingQueries.GetByActivityID_ActivityAddress, imageParams, commandType: CommandType.StoredProcedure);

					item.ActivityImages = imageList.ToList();
					item.ActivityAddress = AddressList.ToList();
				}

					 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
				var response = new ListResponseWrapper<Activity> { Data = result.ToList(), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") }; return response;
			}
		}

	}
	public async Task<SingleResponseWrapper<Activity>> GetByIdAsync(SearchRequestById searchRequestById)
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

				var result = await connection.QuerySingleOrDefaultAsync<Activity>(ActivityQueries.GetByID_Activity, parameters, commandType: CommandType.StoredProcedure);
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

				var response = new SingleResponseWrapper<Activity>
				{
					Data = result,
					Code = parameters.Get<int>("@Code"),
					Message = parameters.Get<string>("@Message")
				};
				return response;
			}
		}
	}



	public async Task<ResponseEntity> UpdateAsync(Activity entity)
	{

		using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity))
		{
			using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
			{
				connection.Open();
				UpdateActivityDTO updateActivityDTO = _mapper.Map<UpdateActivityDTO>(entity);
				var parameters = new DynamicParameters(updateActivityDTO);
				//parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
				//parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

				var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ActivityQueries.Update_Activity, parameters, commandType: CommandType.StoredProcedure);
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
				if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
				return result;
			}
		}
	}
}

/// <inheritdoc/>


/// <inheritdoc/>

