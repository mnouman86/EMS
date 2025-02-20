using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.RoomImages;
using CleanArc.Domain.Entities.SearchHotelDetail;
using CleanArc.Domain.Entities.SearchHotelRoomDetail;
using CleanArc.Domain.Entities.SearchRoomAmenities;
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
using CleanArc.Domain.Entities.Country;

namespace CleanArc.Infrastructure.Persistence.Repositories
{

	public class SearchHotelRoomDetailRepository : ISearchHotelRoomDetailRepository
	{/// <summary>
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
		private readonly ILogger<SearchHotelRoomDetailRepository> _logger;

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
		/// 
		public SearchHotelRoomDetailRepository(IConfiguration configuration, IMapper mapper, ILogger<SearchHotelRoomDetailRepository> logger, IHttpContextAccessor httpContextAccessor)
		{
			this.configuration = configuration;
			this._mapper = mapper;
			this._logger = logger;
			_httpContextAccessor = httpContextAccessor;
		}
		public Task<ResponseEntity> AddAsync(SearchHotelRoomDetail entity)
		{
			throw new NotImplementedException();
		}

		public Task<ResponseEntity> DeleteAsync(string selectedIds, int updatedBy, int? CultureId)
		{
			throw new NotImplementedException();
		}

		public async Task<ListResponseWrapper<SearchHotelRoomDetail>> GetAllAsync(SearchRequest searchRequest)
		{
			using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
			{
				using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
				{
					connection.Open();
					// var parameters = new
					//{
					//    PageNumber = searchRequest.PageNumber,
					//    PageSize = searchRequest.PageSize,
					//    //SortingColumnName = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnName,
					//    //SortingColumnDirection = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnDirection,
					//    //FilterParameterName = searchRequest.FilterArray?.FirstOrDefault()?.ParameterName,
					//    //FilterParameterValue = searchRequest.FilterArray?.FirstOrDefault()?.ParameterValue
					//    SortingArray = DataTableHelper.ToDataTable(searchRequest.SortingArray), // Convert list to DataTable
					//    FilterArray = DataTableHelper.ToDataTable(searchRequest.FilterArray) // Convert list to DataTable
					//};

					var parameters = new DynamicParameters();
					parameters.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
					parameters.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
					parameters.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
					parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object); // Ensure proper type
					parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object); // Ensure proper type
					parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
					parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
					var result = await connection.QueryAsync<SearchHotelRoomDetail>(SearchHotelRoomDetailQueries.usp_GetALLByHotelID_Rooms, parameters, commandType: CommandType.StoredProcedure);
					foreach (var item in result)
					{
						List<FilterParameter> FilterArray = new List<FilterParameter>();
						List<SortingParameter> SortingArray = new List<SortingParameter>();
						FilterArray.Add(new FilterParameter { ParameterName = "RoomID", ParameterValue = item.ID.ToString() });
						var parameter = new DynamicParameters();
						parameter.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
						parameter.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
						parameter.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
						parameter.Add("@SortingArray", DataTableHelper.ToDataTable(SortingArray), DbType.Object); // Ensure proper type
						parameter.Add("@FilterArray", DataTableHelper.ToDataTable(FilterArray), DbType.Object); // Ensure proper type
						parameter.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
						parameter.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
						var imageList = await connection.QueryAsync<RoomImage>(RoomImagesQueries.usp_GetByHotelID_RoomImages, parameter, commandType: CommandType.StoredProcedure);
						var amenitiesList = await connection.QueryAsync<RoomAmenities>(SearchRoomAmenitiesQueries.usp_GetByHotelID_RoomAmenities, parameter, commandType: CommandType.StoredProcedure);
						item.RoomImages = new List<RoomImage>();
						item.RoomImages.AddRange(imageList);
						item.RoomAmenities = new List<RoomAmenities>();
						item.RoomAmenities.AddRange(amenitiesList);
					}
						 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
					//var response = new ListResponseWrapper<SearchHotelRoomDetail> { Data = result.ToList() };return response;

					var response = new ListResponseWrapper<SearchHotelRoomDetail> { Data = result.ToList(), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") }; return response;

				}
			}
		}

		public Task<SingleResponseWrapper<SearchHotelRoomDetail>> GetByIdAsync(long id)
		{
			throw new NotImplementedException();
		}

		public Task<ResponseEntity> UpdateAsync(SearchHotelRoomDetail entity)
		{
			throw new NotImplementedException();
		}
	}
}

