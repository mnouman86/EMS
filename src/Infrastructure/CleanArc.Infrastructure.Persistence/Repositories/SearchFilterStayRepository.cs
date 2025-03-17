using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
//using CleanArc.Domain.Entities.SearchFilterStay;
using CleanArc.Domain.Entities.SearchHotelDetail;
using CleanArc.Domain.Entities.SearchHotelImage;
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
using System.Reflection.Metadata;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class SearchFilterStayRepository : ISearchFilterStayRepository
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
	private readonly ILogger<SearchFilterStayRepository> _logger;

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
	public SearchFilterStayRepository(IConfiguration configuration, IMapper mapper, ILogger<SearchFilterStayRepository> logger, IHttpContextAccessor httpContextAccessor)
	{
		this.configuration = configuration;
		this._mapper = mapper;
		this._logger = logger;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<ListResponseWrapper<SearchHotelDetail>> GetAllWithParamAsync(SearchRequestStays searchRequest)
	{
		using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
		{
			using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
			{
				connection.Open();
                var parameters = new DynamicParameters();

                // Add input parameters
                parameters.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
                parameters.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
                parameters.Add("@Name", searchRequest.Name, DbType.String);
                parameters.Add("@MaxPrice", searchRequest.MaxPrice, DbType.Decimal);
                parameters.Add("@MinPrice", searchRequest.MinPrice, DbType.Decimal);
                parameters.Add("@HotelAmenities", searchRequest.HotelAmenities, DbType.String);
                parameters.Add("@RoomAmenities", searchRequest.RoomAmenities, DbType.String);
                parameters.Add("@BathroomAmenities", searchRequest.BathroomAmenities, DbType.String);
                parameters.Add("@RoomFeature", searchRequest.RoomFeature, DbType.String);
                parameters.Add("@RoomView", searchRequest.RoomView, DbType.String);
                parameters.Add("@CultureId", searchRequest.CultureId, DbType.Int32);

                // Convert List to DataTable and Add as Table-Valued Parameter (TVP)
                parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object);
                parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object);

                // Add output parameters
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = await connection.QueryAsync<SearchHotelDetail>(SearchFilterStayQueries.GetAll_SearchDetail, parameters, commandType: CommandType.StoredProcedure);
				foreach (var item in result)
				{
					List<FilterParameter> ImagesFilterArray = new List<FilterParameter>();
					List<SortingParameter> ImagesSortingArray = new List<SortingParameter>();

					ImagesFilterArray.Add(new FilterParameter { ParameterName = "genericTitleID", ParameterValue = item.HotelID.ToString() });
					var parameter = new DynamicParameters();
					parameter.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
					parameter.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
					parameter.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
					parameter.Add("@SortingArray", DataTableHelper.ToDataTable(ImagesSortingArray), DbType.Object); // Ensure proper type
					parameter.Add("@FilterArray", DataTableHelper.ToDataTable(ImagesFilterArray), DbType.Object); // Ensure proper type
					parameter.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
					parameter.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
					
					var imageList = await connection.QueryAsync<HotelImage>(SearchHotelImageQueries.GetByHotelID_HotelImage, parameter, commandType: CommandType.StoredProcedure);
					item.HotelImages = new List<HotelImage>();
					item.HotelImages.AddRange(imageList);
				}
				 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
				var response = new ListResponseWrapper<SearchHotelDetail> { Data = result.ToList() };return response;
			}
		}
	}

}
