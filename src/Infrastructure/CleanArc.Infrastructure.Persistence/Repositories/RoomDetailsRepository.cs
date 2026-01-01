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
using CleanArc.Domain.Entities.AmenityMapping;
using CleanArc.Domain.Entities.Hotel;
using CleanArc.Domain.Entities.Language;
using CleanArc.Domain.Entities.GenericMedia;
using CleanArc.Domain.Entities.FAQs;
using CleanArc.Domain.Enums;
using CleanArc.Domain.Entities.CustomerReview;
using CleanArc.Domain.Entities.Amenity;
using CleanArc.Domain.Entities.RoomView;
using CleanArc.Domain.Entities.OutDoor;
using CleanArc.Domain.Entities.NearByLocation;
using CleanArc.Application.Services.Aggregators;
using CleanArc.Infrastructure.Persistence.Providers.BookingWhizz;
using CleanArc.Domain.Entities.RatePlanType;
using CleanArc.Domain.Entities.RatePlan;

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
    private readonly HotelProviderAggregator _hotelProviderAggregator;

    public RoomDetailsRepository(IConfiguration configuration, IMapper mapper, ILogger<RoomDetailsRepository> logger,
        IHttpContextAccessor httpContextAccessor, HotelProviderAggregator hotelProviderAggregator)
	{
		this.configuration = configuration;
		this._mapper = mapper;
		this._logger = logger;
		_httpContextAccessor = httpContextAccessor;
        this._hotelProviderAggregator = hotelProviderAggregator;
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
                var roomViewTable = new DataTable();
                roomViewTable.Columns.Add("RoomViewLookUpId", typeof(int));

                foreach (var roomView in roomDetails.RoomViewLookUpId)
                    roomViewTable.Rows.Add(roomView);
                parameters.Add("@RoomView", roomViewTable.AsTableValuedParameter("RoomViewTableType"));

                var outDoorTable = new DataTable();
                outDoorTable.Columns.Add("OutDoorLookUpId", typeof(int));

                foreach (var outDoor in roomDetails.OutDoorLookUpId)
                    outDoorTable.Rows.Add(outDoor);
                parameters.Add("@OutDoor", roomViewTable.AsTableValuedParameter("OutDoorTableType"));
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(RoomDetailQueries.Create_RoomDetail, parameters, commandType: CommandType.StoredProcedure);
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
				//
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
				
				var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(RoomDetailQueries.Delete_RoomDetail, parameters, commandType: CommandType.StoredProcedure);
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
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
				if (searchRequest.PageSize > 0) parameters.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
				parameters.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
				parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object); // Ensure proper type
				parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object); // Ensure proper type
                parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
				parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
				var result = await connection.QueryAsync<RoomDetails>(RoomDetailQueries.GetALL_RoomDetail, parameters, commandType: CommandType.StoredProcedure);
                foreach (var row in result)
                {
                    var parametersRoomType = new DynamicParameters();
                    parametersRoomType.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parametersRoomType.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                    parametersRoomType.Add("@CultureId", searchRequest.CultureId, DbType.Int32);
                    parametersRoomType.Add("@RoomDetailId", row.Id, DbType.Int32);
                    var resultRoomType = await connection.QueryAsync<RatePlanType>(RatePlanTypeQueries.GetByID_RatePlanType, parametersRoomType, commandType: CommandType.StoredProcedure);
                row.RatePlanTypes = resultRoomType;
                }

				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
				var response = new ListResponseWrapper<RoomDetails> { Data = result.ToList(), TotalCount = parameters.Get<int>("@TotalCount"), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") }; return response;

			}
		}
	}
	public async Task<SingleResponseWrapper<RoomDetails>> GetByIdAsync(SearchRequestById searchRequestById)
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



				//var result = await connection.QuerySingleOrDefaultAsync<RoomDetails>(RoomDetailQueries.GetByID_RoomDetail, parameters, commandType: CommandType.StoredProcedure);
                var result = await connection.QueryMultipleAsync(RoomDetailQueries.GetByID_RoomDetail, parameters, commandType: CommandType.StoredProcedure);
                var roomDetail = result.Read<RoomDetails>().FirstOrDefault();
                if (roomDetail != null)
                {
                    var roomView = result.Read<RoomViewLookUp>().ToList();
                    roomDetail.RoomView = roomView;

                    var outDoor = result.Read<OutDoorLookUp>().ToList();
                    roomDetail.OutDoor = outDoor;
                }
                if (!result.IsConsumed)
                {
                    result.Dispose();
                }
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
				//
				var response = new SingleResponseWrapper<RoomDetails>
				{
					Data = roomDetail,
					Code = parameters.Get<int>("@Code"),
					Message = parameters.Get<string>("@Message")
				};
				return response;
			}
		}
	}

    public async Task<SingleResponseWrapper<HotelDetail>> GetHotelDetailForRoomAsync(HotelDetailSearchRequest searchRequest)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
        {
            // ✅ Fetch & Merge BookingWhizz via Aggregator
            if (searchRequest.Provider == APIProvider.BookingWhizz)
            {
                
                    //var customizedRequest = new CustomizedSearchRequest
                    //{
                    //    StartDate = DateTime.Today.AddDays(1),
                    //    EndDate = DateTime.Today.AddDays(2),
                    //    FilterArray = new List<FilterParameter>
                    //    {
                    //        new FilterParameter { ParameterName = "AccommodationId", ParameterValue = searchRequest.Id.ToString() }
                    //    }
                    //};

                    var thirdPartyHotel = await _hotelProviderAggregator.GetHotelDetailAsync(searchRequest, searchRequest.Provider);

                    return new SingleResponseWrapper<HotelDetail>
                    {
                        Data = thirdPartyHotel,
                        Code = 200,
                        Message = "Data Retrieved Successfully"
                    };
            }
            else
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                    parameters.Add("@CultureId", searchRequest.CultureId, DbType.Int32);
                    parameters.Add("@ID", searchRequest.Id, DbType.Int32);
                    parameters.Add("@NoOfRooms", searchRequest.NoOfRooms, DbType.Int32);
                    parameters.Add("@StartDate", searchRequest.StartDate, DbType.Date);
                    parameters.Add("@EndDate", searchRequest.EndDate, DbType.Date);
                    parameters.Add("@NoOfDays", searchRequest.NoOfDays, DbType.Int32);

                    var result = await connection.QueryMultipleAsync(RoomDetailQueries.GetHotelDetail_ByRoom, parameters, commandType: CommandType.StoredProcedure);
                    // var kbDetailAll = resultKBDetail.ReadFirst<KBDetail>();
                    var hotelDetail = result.Read<HotelDetail>().FirstOrDefault();
                    if (hotelDetail != null)
                    {
                        var rooms = result.Read<RoomDetails>().ToList();
                        hotelDetail.Rooms = rooms;
                        var media = result.Read<GenericMedia>().ToList();
                        hotelDetail.Medias = media;
                        var hotelAmenities = result.Read<AmenityMapping>().ToList();
                        hotelDetail.Amenities = hotelAmenities;
                        var faqs = result.Read<FAQs>().ToList();
                        hotelDetail.FAQs = faqs;
                        var languages = result.Read<LanguageLookUp>().ToList();
                        hotelDetail.Languages = languages;
                        var reviews = result.Read<CustomerReview>().ToList();
                        hotelDetail.Reviews = reviews;

                        var roomView = result.Read<RoomView>().ToList();
                        hotelDetail.RoomView = roomView;

                        var outDoor = result.Read<OutDoor>().ToList();
                        hotelDetail.OutDoor = outDoor;

                        var nearByLocations = result.Read<NearByLocation>().ToList();
                        hotelDetail.NearByLocations = nearByLocations;

                        var ratePlans = result.Read<RatePlan>().ToList();
                        RoomDetails rd = hotelDetail.Rooms.Where(x => x.Id == searchRequest.Id).FirstOrDefault();
                        rd.RatePlans = ratePlans;
                        if (ratePlans!=null && hotelDetail.Rooms.Count>0)
                        {
                            hotelDetail.Rooms.Where(x => x.Id == searchRequest.Id).FirstOrDefault().RatePlans = ratePlans;
                        }
                        
                        
                        if (!result.IsConsumed)
                        {
                            result.Dispose();
                        }
                        foreach (var item in hotelDetail.Rooms)
                        {
                            var Params = new DynamicParameters();
                            Params.Add("@PageNumber", 1, DbType.Int32);
                            Params.Add("@PageSize", 3, DbType.Int32);
                            Params.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
                            List<SortingParameter> sortingArray = new List<SortingParameter>();
                            Params.Add("@SortingArray", DataTableHelper.ToDataTable(sortingArray), DbType.Object); // Ensure proper type
                            Params.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                            Params.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                            Params.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                            List<FilterParameter> list = new List<FilterParameter>();
                            list.Add(new FilterParameter { ParameterName = "GenericTitleId", ParameterValue = item.Id.ToString() });
                            list.Add(new FilterParameter { ParameterName = "ServiceTypeEnumId", ParameterValue = ((int)ServiceType.Room).ToString() });

                            Params.Add("@FilterArray", DataTableHelper.ToDataTable(list), DbType.Object); // Ensure proper type

                            var imageList = await connection.QueryAsync<Domain.Entities.GenericMedia.GenericMedia>(GenericMediaQueries.GetAll_HotelImage, Params, commandType: CommandType.StoredProcedure);
                            item.Medias = imageList.ToList();

                            var amenities = await connection.QueryAsync<Domain.Entities.AmenityMapping.AmenityMapping>(AmenityMappingQueries.GetByAmenityTypeEnumID_AmenityMapping, Params, commandType: CommandType.StoredProcedure);
                            item.RoomAmenities = amenities.ToList();

                            //var roomView = await connection.QueryAsync<Domain.Entities.RoomView.RoomViewLookUp>(RoomViewQueries.GetALL_RoomView, Params, commandType: CommandType.StoredProcedure);
                            //item.RoomView = roomView.ToList();

                            //var outDoor = await connection.QueryAsync<Domain.Entities.OutDoor.OutDoorLookUp>(OutDoorQueries.GetALL_OutDoor, Params, commandType: CommandType.StoredProcedure);
                            //item.OutDoor = outDoor.ToList();
                        }
                    }


                    //var result = await connection.QuerySingleOrDefaultAsync<RoomDetails>(RoomDetailQueries.GetByID_RoomDetail, parameters, commandType: CommandType.StoredProcedure);
                    (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    //
                    

                    //await connection.ExecuteAsync(
                    //        ActivityQueries.GetByID_Activity,
                    //        parameters,
                    //        commandType: CommandType.StoredProcedure);
                    int Code = parameters.Get<int>("@Code");
                    string Message = parameters.Get<string>("@Message");


                    var response = new SingleResponseWrapper<HotelDetail>
                    {
                        Data = hotelDetail,
                        Code = Code,
                        Message = Message
                    };
                    return response;
                }
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
                var roomViewTable = new DataTable();
                roomViewTable.Columns.Add("RoomViewLookUpId", typeof(int));

                foreach (var roomView in roomDetails.RoomViewLookUpId)
                    roomViewTable.Rows.Add(roomView);
                parameters.Add("@RoomView", roomViewTable.AsTableValuedParameter("RoomViewTableType"));

                var outDoorTable = new DataTable();
                outDoorTable.Columns.Add("OutDoorLookUpId", typeof(int));

                foreach (var outDoor in roomDetails.OutDoorLookUpId)
                    outDoorTable.Rows.Add(outDoor);
                parameters.Add("@OutDoor", outDoorTable.AsTableValuedParameter("OutDoorTableType"));
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(RoomDetailQueries.Update_RoomDetail, parameters, commandType: CommandType.StoredProcedure);
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
				return result;
			}
		}
	}
}



