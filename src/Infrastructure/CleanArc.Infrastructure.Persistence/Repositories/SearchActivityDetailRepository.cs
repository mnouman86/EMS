using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.RoomImages;
using CleanArc.Domain.Entities.SearchHotelDetail;
using CleanArc.Domain.Entities.SearchBusinessCarDetail;
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
using CleanArc.Domain.Enums;
using CleanArc.Domain.Entities.AmenityMapping;
using CleanArc.Domain.Entities.CarDetail;
using CleanArc.Domain.Entities.CustomerReview;
using CleanArc.Domain.Entities.FAQs;
using CleanArc.Domain.Entities.GenericMedia;
using CleanArc.Domain.Entities.Language;
using CleanArc.Domain.Entities.Amenity;
using CleanArc.Domain.Entities.Activity;
using CleanArc.Domain.Entities.GenericAddress;

namespace CleanArc.Infrastructure.Persistence.Repositories
{

    public class SearchActivityDetailRepository : ISearchActivityDetailRepository
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
        private readonly ILogger<SearchActivityDetailRepository> _logger;

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
        public SearchActivityDetailRepository(IConfiguration configuration, IMapper mapper, ILogger<SearchActivityDetailRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this._mapper = mapper;
            this._logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<SingleResponseWrapper<SearchActivityDetail>> GetAllAsync(ActivitySearchRequest searchRequest)
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
                    parameters.Add("@StartDate", searchRequest.StartDate, DbType.DateTime);
                    parameters.Add("@EndDate", searchRequest.EndDate, DbType.DateTime);
                    parameters.Add("@Rating", searchRequest.Rating, DbType.Int32);
                    parameters.Add("@MinPrice", searchRequest.MinPrice, DbType.Int32);
                    parameters.Add("@MaxPrice", searchRequest.MaxPrice, DbType.Int32);
                    parameters.Add("@Amenities", searchRequest.Amenities, DbType.String);
                    parameters.Add("@Name", searchRequest.Name, DbType.String);
                    parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object); // Ensure proper type
                    parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object); // Ensure proper type
                    parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                    var result = await connection.QueryAsync<Activity>(ActivityQueries.GetAll_Activity, parameters, commandType: CommandType.StoredProcedure);
                    foreach (var item in result)
                    {
                        //var imageParams = new DynamicParameters();
                        //imageParams.Add("@Id", item.Id, DbType.Int32);
                        //imageParams.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
                        //imageParams.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        //imageParams.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                        var Params = new DynamicParameters();
                        Params.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
                        Params.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
                        Params.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
                        Params.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object); // Ensure proper type
                        Params.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        List<FilterParameter> list = new List<FilterParameter>();
                        list.Add(new FilterParameter { ParameterName = "GenericTitleId", ParameterValue = item.Id.ToString() });
                        Params.Add("@FilterArray", DataTableHelper.ToDataTable(list), DbType.Object); // Ensure proper type
                        Params.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        Params.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                        var imageList = await connection.QueryAsync<Domain.Entities.GenericMedia.GenericMedia>(GenericMediaQueries.GetAll_HotelImage, Params, commandType: CommandType.StoredProcedure);

                        var AddressList = await connection.QueryAsync<GenericAddress>(GenericAddressQueries.GetAll_GenericAddress, Params, commandType: CommandType.StoredProcedure);

                        item.ActivityImages = imageList.ToList();
                        item.ActivityAddress = AddressList.ToList();
                    }
                    SearchActivityDetail searchActivityDetail = new SearchActivityDetail();
                    searchActivityDetail.ActivityDetail = result.ToList();
                    searchActivityDetail.ActivityPriceMinimum = result.Count()>0? result.Min(x => x.ActivityPrice) :0;
                    searchActivityDetail.ActivityPriceMaximum = result.Count() > 0 ? result.Max(x => x.ActivityPrice):0;
                    var response = new SingleResponseWrapper<SearchActivityDetail>
                    {
                        Data = searchActivityDetail,
                        Code = parameters.Get<int>("@Code"),
                        Message = parameters.Get<string>("@Message")
                    };
                    (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
					//var response = new ListResponseWrapper<SearchBusinessCarDetail>
     //               {
     //                   Data = result.ToList(),
     //                   Code = parameters.Get<int>("@Code"),
     //                   Message = parameters.Get<string>("@Message")
     //               }; 
                    return response;

				}
			}
        }

        public async Task<SingleResponseWrapper<CarDetail>> GetCarDetailByBusinessAsync(CarDetailSearchRequest searchRequest)
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
                    parameters.Add("@ID", searchRequest.Id, DbType.Int32);
                    parameters.Add("@NoOfDays", searchRequest.NoOfDays, DbType.Int32);

                    //var result = await connection.QuerySingleOrDefaultAsync<CarDetail>(CarDetailQueries.GetByID_CarDetail, parameters, commandType: CommandType.StoredProcedure);
                    var result = await connection.QueryMultipleAsync(CarDetailQueries.GetByID_CarDetailByBusiness, parameters, commandType: CommandType.StoredProcedure);
                    // var kbDetailAll = resultKBDetail.ReadFirst<KBDetail>();
                    var carDetail = result.Read<CarDetail>().FirstOrDefault();
                    if (carDetail != null)
                    {
                        var cars = result.Read<Car>().ToList();
                        carDetail.Cars = cars;
                        


                        var media = result.Read<GenericMedia>().ToList();
                        carDetail.Medias = media;
                        var amenities = result.Read<AmenityLookUp>().ToList();
                        carDetail.Amenities = amenities;
                        var faqs = result.Read<FAQs>().ToList();
                        carDetail.FAQs = faqs;
                        var languages = result.Read<LanguageLookUp>().ToList();
                        carDetail.Languages = languages;
                        var reviews = result.Read<CustomerReview>().ToList();
                        carDetail.Reviews = reviews;

                        if (!result.IsConsumed)
                        {
                            result.Dispose();
                        }
                        foreach (var car in cars)
                        {
                            var Params = new DynamicParameters();
                            Params.Add("@PageNumber", 1, DbType.Int32);
                            Params.Add("@PageSize", 1, DbType.Int32);
                            Params.Add("@cultureId", 1, DbType.Int32);
                            List<FilterParameter> sortingFilter = new List<FilterParameter>();
                            Params.Add("@SortingArray", DataTableHelper.ToDataTable(sortingFilter), DbType.Object); // Ensure proper type
                            Params.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                            Params.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                            Params.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                            List<FilterParameter> list = new List<FilterParameter>();
                            list.Add(new FilterParameter { ParameterName = "GenericTitleId", ParameterValue = car.CarId.ToString() });
                            list.Add(new FilterParameter { ParameterName = "ServiceTypeEnumId", ParameterValue = ((int)ServiceType.Car).ToString() });

                            Params.Add("@FilterArray", DataTableHelper.ToDataTable(list), DbType.Object); // Ensure proper type

                            var imageList = await connection.QueryAsync<Domain.Entities.GenericMedia.GenericMedia>(GenericMediaQueries.GetAll_HotelImage, Params, commandType: CommandType.StoredProcedure);
                            car.Medias = imageList.ToList();
                        }
                    }
                    //var result = await connection.QuerySingleOrDefaultAsync<RoomDetails>(RoomDetailQueries.GetByID_RoomDetail, parameters, commandType: CommandType.StoredProcedure);
                    (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    //

                    if (!result.IsConsumed)
                    {
                        result.Dispose();
                    }
                    //await connection.ExecuteAsync(
                    //        ActivityQueries.GetByID_Activity,
                    //        parameters,
                    //        commandType: CommandType.StoredProcedure);
                    int Code = parameters.Get<int>("@Code");
                    string Message = parameters.Get<string>("@Message");


                    var response = new SingleResponseWrapper<CarDetail>
                    {
                        Data = carDetail,
                        Code = Code,
                        Message = Message
                    };
                    return response;
                }
            }
        }
    }
}

