using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
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
using CleanArc.Domain.Entities.PopularItemsVisit;
using CleanArc.Domain.Enums;
using CleanArc.Application.Services.Aggregators;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class SearchHotelDetailRepository : ISearchHotelRepository
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
    private readonly ILogger<SearchHotelDetailRepository> _logger;

    /// <summary>
    /// The HTTP context accessor for accessing HTTP context information.
    /// </summary>
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HotelProviderAggregator _hotelProviderAggregator;


    /// <summary>
    /// Initializes a new instance of the <see cref="MenuRepository"/> class.
    /// </summary>
    /// <param name="configuration">The configuration for accessing application settings.</param>
    /// <param name="mapper">The mapper for mapping between different object types.</param>
    /// <param name="logger">The logger for logging repository-related information.</param>
    /// <param name="httpContextAccessor">The HTTP context accessor for accessing HTTP context information.</param>
    /// 
    public SearchHotelDetailRepository(IConfiguration configuration, IMapper mapper, ILogger<SearchHotelDetailRepository> logger,
        IHttpContextAccessor httpContextAccessor, HotelProviderAggregator hotelProviderAggregator)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
        this._hotelProviderAggregator = hotelProviderAggregator;
    }
   
    public async Task<SingleResponseWrapper<SearchHotelDetail>> GetAllAsync(CustomizedSearchRequest searchRequest)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
				IEnumerable<SearchDetail> result=new List<SearchDetail>();
				var parameters = new DynamicParameters();
				parameters.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
				if (searchRequest.PageSize > 0) parameters.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
                if (searchRequest.UserId > 0) parameters.Add("@UserId", searchRequest.UserId, DbType.Int32);
                parameters.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
				parameters.Add("@NoOfRooms", searchRequest.NoOfRooms, DbType.Int32);
                parameters.Add("@NoOfPersons", searchRequest.NoOfDays, DbType.Int32);
                //parameters.Add("@NoOfDays", searchRequest.NoOfDays, DbType.Int32);
                parameters.Add("@Rating", searchRequest.Rating, DbType.Int32);
				parameters.Add("@MinPrice", searchRequest.MinPrice, DbType.Int32);
				parameters.Add("@MaxPrice", searchRequest.MaxPrice, DbType.Int32);
                parameters.Add("@FromDate", searchRequest.StartDate, DbType.Date);
                parameters.Add("@ToDate", searchRequest.EndDate, DbType.Date);
                parameters.Add("@Amenities", searchRequest.Amenities, DbType.String);
				parameters.Add("@Name", searchRequest.Name, DbType.String);
				parameters.Add("@Type", searchRequest.Type, DbType.String);
				parameters.Add("@PropertyType", searchRequest.PropertyType, DbType.String);
				parameters.Add("@RoomView", searchRequest.RoomView, DbType.String);
				parameters.Add("@OutDoor", searchRequest.OutDoor, DbType.String);
                parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object); // Ensure proper type
				parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object); // Ensure proper type
                parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
				parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                string query = SearchHotelDetailQueries.GetAll_SearchHotelDetail;
                if (searchRequest.FilterByWishList)
                    query = SearchHotelDetailQueries.GetAll_StaysWishList;
                if (!searchRequest.IsThirdParty)
                {
                    result = await connection.QueryAsync<SearchDetail>(query, parameters, commandType: CommandType.StoredProcedure);
                    foreach (var item in result)
                    {
                        var Params = new DynamicParameters();
                        Params.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
                        Params.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
                        Params.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
                        List<FilterParameter> sortingFilter = new List<FilterParameter>();
                        Params.Add("@SortingArray", DataTableHelper.ToDataTable(sortingFilter), DbType.Object); // Ensure proper type

                        Params.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        Params.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                        var ParamsImage = Params;
                        List<FilterParameter> list = new List<FilterParameter>();
                        list.Add(new FilterParameter { ParameterName = "GenericTitleId", ParameterValue = item.Id.ToString() });
                        list.Add(new FilterParameter { ParameterName = "ServiceTypeEnumId", ParameterValue = ((int)ServiceType.Room).ToString() });

                        ParamsImage.Add("@FilterArray", DataTableHelper.ToDataTable(list), DbType.Object); // Ensure proper type
                        ParamsImage.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        var imageList = await connection.QueryAsync<Domain.Entities.GenericMedia.GenericMedia>(GenericMediaQueries.GetAll_HotelImage, ParamsImage, commandType: CommandType.StoredProcedure);
                        item.HotelImages = imageList.ToList();

                        var ParamsAmenity = Params;
                        List<FilterParameter> Amenity = new List<FilterParameter>();
                        Amenity.Add(new FilterParameter { ParameterName = "GenericTitleId", ParameterValue = item.Id.ToString() });
                        Amenity.Add(new FilterParameter { ParameterName = "ServiceTypeEnumId", ParameterValue = ((int)ServiceType.Room).ToString() });
                        ParamsAmenity.Add("@FilterArray", DataTableHelper.ToDataTable(Amenity), DbType.Object); // Ensure proper type
                        ParamsAmenity.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        var amenities = await connection.QueryAsync<Domain.Entities.AmenityMapping.AmenityMapping>(AmenityMappingQueries.GetByAmenityTypeEnumID_AmenityMapping, ParamsAmenity, commandType: CommandType.StoredProcedure);
                        item.Amenities = amenities.Where(x => x.Selected == true).Take(3).ToList();
                    }
                }
                // Get third-party data using aggregator

                List<SearchDetail> thirdPartyHotels = new List<SearchDetail>();
                if (searchRequest.StartDate!=null && searchRequest.EndDate!=null)
                {
                    thirdPartyHotels = await _hotelProviderAggregator.SearchHotelsAsync(searchRequest);
                }
                    
                

                var combinedHotels = thirdPartyHotels;
                if (result!=null)
                {
                    combinedHotels.AddRange(result.Where(x=>x.GenericTitleId>0).ToList()); // ← Merged list

                }
                string? sortingDirection = searchRequest?.SortingArray?.FirstOrDefault()?.SortingColumnDirection;
                if (sortingDirection != null && sortingDirection == "ASC")
                {
                    combinedHotels = combinedHotels.OrderBy(h => h.DiscountedPrice).ToList();
                }
                else
                {
                    combinedHotels = combinedHotels.OrderByDescending(h => h.DiscountedPrice).ToList();
                }

                    (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(combinedHotels);
                SearchHotelDetail searchHotelDetail = new SearchHotelDetail();
                //searchHotelDetail.HotelDetail = result.ToList();
                searchHotelDetail.HotelDetail = combinedHotels;
                searchHotelDetail.RoomPriceMinimum= combinedHotels.Count()>0? (int)Math.Round(combinedHotels.Where(x => x.RoomDetailPrice.HasValue).Min(x => x.RoomDetailPrice.Value)):0;
                searchHotelDetail.RoomPriceMaximum= combinedHotels.Count() > 0 ? (int)Math.Round(combinedHotels.Where(x => x.RoomDetailPrice.HasValue).Max(x => x.RoomDetailPrice.Value)) : 0;
                var response = new SingleResponseWrapper<SearchHotelDetail>
                {
                    Data = searchHotelDetail,
                    Code = !searchRequest.IsThirdParty ? parameters.Get<int>("@Code") : 200,
                    Message = !searchRequest.IsThirdParty ? parameters.Get<string>("@Message") : "Data retrieved successfully."                    
                };
                if ((result?.Count()==0 ||searchHotelDetail?.HotelDetail.Count>0) && combinedHotels?.Count()>0) { response.Code = 200;response.Message = "Data retrieved successfully."; }
                return response;

			}
		}
    }

    
}
