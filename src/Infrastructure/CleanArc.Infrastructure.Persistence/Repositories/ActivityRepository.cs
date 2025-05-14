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
using CleanArc.Domain.Entities.Hotel;
using CleanArc.Domain.Entities.Amenity;
using CleanArc.Domain.Entities.Language;
using CleanArc.Domain.Entities.ActivityDisabilityOption;
using CleanArc.Domain.Entities.ActivityIncludedOption;
using CleanArc.Domain.Entities.ActivitySeason;
using CleanArc.Domain.Entities.GenericMedia;
using CleanArc.Domain.Entities.GenericAddress;
using CleanArc.Domain.Enums;
using CleanArc.Domain.Entities.CarDetail;
using CleanArc.Domain.Entities.CustomerReview;
using CleanArc.Domain.Entities.FAQs;
using CleanArc.Domain.Entities.ActivityPerGroupPrice;
using CleanArc.Domain.Entities.ActivitySchedule;
//using System.Diagnostics;

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
	public async Task<ResponseEntity> AddAsync(Activity activity)
	{
		using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, activity))
		{
			using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
			{
				connection.Open();
				CreateActivityDTO createActivityDTO = _mapper.Map<CreateActivityDTO>(activity);
				var parameters = new DynamicParameters(createActivityDTO);
                var languageTable = new DataTable();
                languageTable.Columns.Add("LanguageTypeLookUpId", typeof(int));
                foreach (var language in activity.LanguageLookUpId)
                    languageTable.Rows.Add(language);
                parameters.Add("@Languages", languageTable.AsTableValuedParameter("LanguageTableType"));

                var seasonTable = new DataTable();
                seasonTable.Columns.Add("SeasonLookUpIds", typeof(int));
                foreach (var season in activity.SeasonLookUpId)
                    seasonTable.Rows.Add(season);
                parameters.Add("@Seasons", seasonTable.AsTableValuedParameter("SeasonIDTableType"));

                var includeOptionsTable = new DataTable();
                includeOptionsTable.Columns.Add("IncludeOptionIds", typeof(int));
                foreach (var includeOption in activity.IncludeOptionLookUpId)
                    includeOptionsTable.Rows.Add(includeOption);
                parameters.Add("@IncludeOptions", includeOptionsTable.AsTableValuedParameter("IncludeOptionsTableType"));

                var disabilityOptionsTable = new DataTable();
                disabilityOptionsTable.Columns.Add("DisabilityOptionIds", typeof(int));
                foreach (var disabilityOption in activity.DisabilityOptionLookUpId)
                    disabilityOptionsTable.Rows.Add(disabilityOption);
                parameters.Add("@DisabilityOptions", disabilityOptionsTable.AsTableValuedParameter("DisabilityOptionTableType"));
                //parameters.AddDynamicParams(createActivityDTO);

                // var result = await connection.ExecuteScalarAsync(ActivityQueries.Create_Activity, parameters, commandType: CommandType.StoredProcedure);
                //var result = await connection.QuerySingleOrDefaultAsync<int>(ActivityQueries.Create_Activity, parameters, commandType: CommandType.StoredProcedure);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ActivityQueries.Create_Activity, parameters, commandType: CommandType.StoredProcedure);

				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); //
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
				
				var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ActivityQueries.Delete_Activity, parameters, commandType: CommandType.StoredProcedure);
				// var result = await connection.QuerySingleOrDefaultAsync<int>(ActivityQueries.Delete_Activity, parameters, commandType: CommandType.StoredProcedure);

				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
				//
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
                    var ParamsAddress = new DynamicParameters();
                    var ParamsMedia = new DynamicParameters();

                    Params.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
                    Params.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
                    Params.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
                    Params.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object); // Ensure proper type

                    Params.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    Params.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                    ParamsMedia = Params;
                    List<FilterParameter> list = new List<FilterParameter>();
                    list.Add(new FilterParameter { ParameterName = "GenericTitleId", ParameterValue = item.Id.ToString() });
                    list.Add(new FilterParameter { ParameterName = "ServiceTypeEnumId", ParameterValue = ((int)ServiceType.ThingsToDo).ToString() });

                    ParamsMedia.Add("@FilterArray", DataTableHelper.ToDataTable(list), DbType.Object); // Ensure proper type
                    ParamsMedia.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    var imageList = await connection.QueryAsync<Domain.Entities.GenericMedia.GenericMedia>(GenericMediaQueries.GetAll_HotelImage, ParamsMedia, commandType: CommandType.StoredProcedure);

                    ParamsAddress = Params;
                    List<FilterParameter> listAddress = new List<FilterParameter>();
                    listAddress.Add(new FilterParameter { ParameterName = "GenericTitleId", ParameterValue = item.Id.ToString() });

                    ParamsAddress.Add("@FilterArray", DataTableHelper.ToDataTable(listAddress), DbType.Object); // Ensure proper type
                    ParamsAddress.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    var AddressList = await connection.QueryAsync<GenericAddress>(GenericAddressQueries.GetAll_GenericAddress, ParamsAddress, commandType: CommandType.StoredProcedure);

                    item.ActivityImages = imageList.ToList();
                    item.ActivityAddress = AddressList.ToList();
                }

					 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
				var response = new ListResponseWrapper<Activity> { Data = result.ToList(), TotalCount = parameters.Get<int>("@TotalCount"), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") }; return response;
			}
		}

	}

    public async Task<SingleResponseWrapper<SearchActivityDetail>> GetAllActivitySearchDetailAsync(ActivitySearchRequest searchRequest)
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
                parameters.Add("@DateFrom", searchRequest.StartDate, DbType.DateTime);
                parameters.Add("@DateTo", searchRequest.EndDate, DbType.DateTime);
                parameters.Add("@Rating", searchRequest.Rating, DbType.Int32);
                parameters.Add("@MinPrice", searchRequest.MinPrice, DbType.Int32);
                parameters.Add("@MaxPrice", searchRequest.MaxPrice, DbType.Int32);
                //parameters.Add("@Amenities", searchRequest.Amenities, DbType.String);
                parameters.Add("@Name", searchRequest.Name, DbType.String);
                parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object); // Ensure proper type
                parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object); // Ensure proper type
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = await connection.QueryAsync<Activity>(ActivityQueries.GetAllByBusinessID_Activities, parameters, commandType: CommandType.StoredProcedure);
                int code = parameters.Get<int>("@Code");
                string message = parameters.Get<string>("@Message");
                foreach (var item in result)
                {
                    //var imageParams = new DynamicParameters();
                    //imageParams.Add("@Id", item.Id, DbType.Int32);
                    //imageParams.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
                    //imageParams.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    //imageParams.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                    var Params = new DynamicParameters();
                    var ParamsAddress = new DynamicParameters();
                    var ParamsMedia = new DynamicParameters();
                    
                    Params.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
                    Params.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
                    Params.Add("@cultureId", searchRequest.CultureId, DbType.Int32);

                    List<SortingParameter> sortingArray = new List<SortingParameter>();
                    Params.Add("@SortingArray", DataTableHelper.ToDataTable(sortingArray), DbType.Object); // Ensure proper type
                    
                    Params.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    Params.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                    ParamsMedia = Params;
                    List<FilterParameter> list = new List<FilterParameter>();
                    list.Add(new FilterParameter { ParameterName = "GenericTitleId", ParameterValue = item.Id.ToString() });
                    list.Add(new FilterParameter { ParameterName = "ServiceTypeEnumId", ParameterValue = ((int)ServiceType.ThingsToDo).ToString() });

                    ParamsMedia.Add("@FilterArray", DataTableHelper.ToDataTable(list), DbType.Object); // Ensure proper type
                    ParamsMedia.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    var imageList = await connection.QueryAsync<Domain.Entities.GenericMedia.GenericMedia>(GenericMediaQueries.GetAll_HotelImage, ParamsMedia, commandType: CommandType.StoredProcedure);

                    ParamsAddress = Params;
                    List<FilterParameter> listAddress = new List<FilterParameter>();
                    listAddress.Add(new FilterParameter { ParameterName = "GenericTitleId", ParameterValue = item.Id.ToString() });

                    ParamsAddress.Add("@FilterArray", DataTableHelper.ToDataTable(listAddress), DbType.Object); // Ensure proper type
                    ParamsAddress.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    var AddressList = await connection.QueryAsync<GenericAddress>(GenericAddressQueries.GetAll_GenericAddress, ParamsAddress, commandType: CommandType.StoredProcedure);

                    item.ActivityImages = imageList.ToList();
                    item.ActivityAddress = AddressList.ToList();
                }
                SearchActivityDetail searchActivityDetail = new SearchActivityDetail();
                searchActivityDetail.ActivityDetail = result.ToList();
                searchActivityDetail.ActivityPriceMinimum = result.Count() > 0 ? result.Min(x => x.ActivityPrice) : 0;
                searchActivityDetail.ActivityPriceMaximum = result.Count() > 0 ? result.Max(x => x.ActivityPrice) : 0;
                var response = new SingleResponseWrapper<SearchActivityDetail>
                {
                    Data = searchActivityDetail,
                    Code = code,
                    Message = message
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
				parameters.Add("@Id", searchRequestById.Id, DbType.Int32);

				//var result = await connection.QuerySingleOrDefaultAsync<Activity>(ActivityQueries.GetByID_Activity, parameters, commandType: CommandType.StoredProcedure);
                var result = await connection.QueryMultipleAsync(ActivityQueries.GetByID_Activity, parameters, commandType: CommandType.StoredProcedure);

                

                var activity = result.Read<Activity>().FirstOrDefault();
                if (activity != null)
                {
                    var language = result.Read<LanguageLookUp>().ToList();
                    activity.Languages = language;

                    var disabilityOptions = result.Read<DisabilityOptionsLookUp>().ToList();
                    activity.DisabilityOptions = disabilityOptions;
                    var includedOptions = result.Read<IncludedOptionsLookup>().ToList();
                    activity.IncludedOptions = includedOptions;
                    var activitySeason = result.Read<ActivitySeasonLookUp>().ToList();
                    activity.Seasons = activitySeason;

                }
                if (!result.IsConsumed)
                {
                    result.Dispose();
                }
                //await connection.ExecuteAsync(
                //        ActivityQueries.GetByID_Activity,
                //        parameters,
                //        commandType: CommandType.StoredProcedure);
                int? Code = parameters.Get<int>("@Code");
                string? Message = parameters.Get<string>("@Message");
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(activity);

				var response = new SingleResponseWrapper<Activity>
				{
					Data = activity,
					Code =(int) Code,
					Message = Message
				};
				return response;
			}
		}
	}

    public async Task<SingleResponseWrapper<Activity>> GetActivityDetailByBusinessAsync(SearchRequestById searchRequest)
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

                //var result = await connection.QuerySingleOrDefaultAsync<CarDetail>(CarDetailQueries.GetByID_CarDetail, parameters, commandType: CommandType.StoredProcedure);
                //var result = await connection.QueryMultipleAsync(ActivityQueries.GetByID_ActivityDetailByBusiness, parameters, commandType: CommandType.StoredProcedure);

                var result = await connection.QueryMultipleAsync(ActivityQueries.GetByID_ActivityDetailByBusiness, parameters, commandType: CommandType.StoredProcedure);
                // var kbDetailAll = resultKBDetail.ReadFirst<KBDetail>();
                var activity = result.Read<Activity>().FirstOrDefault();
                if (activity != null)
                {
                    var language = result.Read<LanguageLookUp>().ToList();
                    activity.Languages = language;

                    var disabilityOptions = result.Read<DisabilityOptionsLookUp>().ToList();
                    activity.DisabilityOptions = disabilityOptions;
                    var includedOptions = result.Read<IncludedOptionsLookup>().ToList();
                    activity.IncludedOptions = includedOptions;
                    var activitySeason = result.Read<ActivitySeasonLookUp>().ToList();
                    activity.Seasons = activitySeason;
                    var schedule = result.Read<ActivitySchedule>().ToList();
                    activity.Schedule = schedule;
                    var groupPrice = result.Read<ActivityPerGroupPrice>().ToList();
                    activity.GroupPrice = groupPrice;
                    var FAQs = result.Read<FAQs>().ToList();
                    activity.FAQs = FAQs;
                    var reviews = result.Read<CustomerReview>().ToList();
                    activity.Reviews = reviews;
                    var activityImages = result.Read<GenericMedia>().ToList();
                    activity.ActivityImages = activityImages;
                    var activityAddress = result.Read<GenericAddress>().ToList();
                    activity.ActivityAddress = activityAddress;

                    if (!result.IsConsumed)
                    {
                        result.Dispose();
                    }

                    List<FilterParameter> filter = new List<FilterParameter>();
                    List<SortingParameter> sorting = new List<SortingParameter>();
                    SearchRequest request = new SearchRequest {PageSize=100,PageNumber=1,CultureId=searchRequest.CultureId,FilterArray=filter,SortingArray=sorting };
                    var activities = GetAllAsync(request);
                    activity.RelatedActivities = activities?.Result?.Data?.Where(x=>x.Id!=searchRequest.Id).ToList();
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


                var response = new SingleResponseWrapper<Activity>
                {
                    Data = activity,
                    Code = Code,
                    Message = Message
                };
                return response;
            }


        }
    }

    public async Task<ResponseEntity> UpdateAsync(Activity activity)
	{

		using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, activity))
		{
			using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
			{
				connection.Open();
				UpdateActivityDTO updateActivityDTO = _mapper.Map<UpdateActivityDTO>(activity);
				var parameters = new DynamicParameters(updateActivityDTO);
                var languageTable = new DataTable();
                languageTable.Columns.Add("LanguageTypeLookUpId", typeof(int));
                foreach (var language in activity.LanguageLookUpId)
                    languageTable.Rows.Add(language);
                parameters.Add("@Languages", languageTable.AsTableValuedParameter("LanguageTableType"));

                var seasonTable = new DataTable();
                seasonTable.Columns.Add("SeasonLookUpIds", typeof(int));
                foreach (var season in activity.SeasonLookUpId)
                    seasonTable.Rows.Add(season);
                parameters.Add("@Seasons", seasonTable.AsTableValuedParameter("SeasonIDTableType"));

                var includeOptionsTable = new DataTable();
                includeOptionsTable.Columns.Add("IncludeOptionIds", typeof(int));
                foreach (var includeOption in activity.IncludeOptionLookUpId)
                    includeOptionsTable.Rows.Add(includeOption);
                parameters.Add("@IncludeOptions", includeOptionsTable.AsTableValuedParameter("IncludeOptionsTableType"));

                var disabilityOptionsTable = new DataTable();
                disabilityOptionsTable.Columns.Add("DisabilityOptionIds", typeof(int));
                foreach (var disabilityOption in activity.DisabilityOptionLookUpId)
                    disabilityOptionsTable.Rows.Add(disabilityOption);
                parameters.Add("@DisabilityOptions", disabilityOptionsTable.AsTableValuedParameter("DisabilityOptionTableType"));
                //parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ActivityQueries.Update_Activity, parameters, commandType: CommandType.StoredProcedure);
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
				
				return result;
			}
		}
	}
}

/// <inheritdoc/>


/// <inheritdoc/>

