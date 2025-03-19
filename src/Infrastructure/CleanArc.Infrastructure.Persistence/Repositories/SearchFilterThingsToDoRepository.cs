using Azure.Core;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Activities;
using CleanArc.Application.Models.BusinessProfile;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.URL;
using CleanArc.Domain.Entities.SearchFilterThingsToDo;
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
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;using CleanArc.Application.Common;
using CleanArc.Application.Models.SearchFilterThingsToDo;

namespace CleanArc.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for handling operations related to menus.
/// </summary>
/// <seealso cref="CleanArc.Application.Contracts.Persistence.IMenuRepository" />
public class SearchFilterThingsToDoRepository:ISearchFilterThingsToDoRepository
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
    private readonly ILogger<SearchFilterThingsToDoRepository> _logger;

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
    public SearchFilterThingsToDoRepository(IConfiguration configuration, IMapper mapper, ILogger<SearchFilterThingsToDoRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
/// <inheritdoc/>
//public async Task<ResponseEntity> AddAsync(SearchFilterThingsToDo SearchFilterThingsToDo)
//{
//    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, SearchFilterThingsToDo))
//    {
//        using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
//        {
//            connection.Open();
//                CreateSearchFilterThingsToDoDTO createSearchFilterThingsToDoDTO = _mapper.Map<CreateSearchFilterThingsToDoDTO>(SearchFilterThingsToDo);
//                var parameters = new DynamicParameters(createSearchFilterThingsToDoDTO);
//                //parameters.AddDynamicParams(createSearchFilterThingsToDoDTO);
//                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
//                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
//                parameters.Add("@SearchFilterThingsToDoID ", dbType: DbType.Int32, direction: ParameterDirection.Output);

//                // var result = await connection.ExecuteScalarAsync(SearchFilterThingsToDoQueries.Create_SearchFilterThingsToDo, parameters, commandType: CommandType.StoredProcedure);
//                var result = await connection.QuerySingleOrDefaultAsync<int>(SearchFilterThingsToDoQueries.Create_SearchFilterThingsToDo, parameters, commandType: CommandType.StoredProcedure);

//                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
//            return result;
//        }
//    }
//}
//    public async Task<ResponseEntity> DeleteAsync(DeleteRequest deleteRequest, int? updatedBy)
//    {
//        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, new { selectedIds, updatedBy,CultureId }))
//        {
//            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
//            {
//                connection.Open();
//                var parameters = new DynamicParameters();
//                parameters.Add("@ID", deleteRequest.SelectedIds);
//                parameters.Add("@CultureId", deleteRequest.CultureId);
//                parameters.Add("@UpdatedBy", updatedBy);
//                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
//                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

//                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(SearchFilterThingsToDoQueries.Delete_SearchFilterThingsToDo, parameters, commandType: CommandType.StoredProcedure);
//               // var result = await connection.QuerySingleOrDefaultAsync<int>(SearchFilterThingsToDoQueries.Delete_SearchFilterThingsToDo, parameters, commandType: CommandType.StoredProcedure);

//                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
//                return result;
//            }
//        }
//    }

    public async Task<ListResponseWrapper<SearchFilterThingsToDo>> GetAllWithParamAsync(ThingsToDoSearchFilterRequest searchRequest)

    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                var parameters = new
                {
                    PageNumber = searchRequest.PageNumber,
                    PageSize = searchRequest.PageSize,
                    Title = searchRequest.Title,
                    CityName= searchRequest.CityName,
                    MaxPrice = searchRequest.MaxPrice,
                    MinPrice = searchRequest.MinPrice,
                    ActivityCategory= searchRequest.ActivityCategory,
                    SeasonName= searchRequest.SeasonName,
                    SortingArray = DataTableHelper.ToDataTable(searchRequest.SortingArray), // Convert list to DataTable
                    FilterArray = DataTableHelper.ToDataTable(searchRequest.FilterArray) // Convert list to DataTable


			};

               
                var result = await connection.QueryAsync<SearchFilterThingsToDo>(SearchFilterThingsToDoQueries.GetAll_SearchFilterThingsToDo, parameters, commandType: CommandType.StoredProcedure);
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

                //foreach (var item in result)
                //{
                //    List<FilterParameter> FilterArray = new List<FilterParameter>();
                //    List<SortingParameter> SortingArray = new List<SortingParameter>();
                //    FilterArray.Add(new FilterParameter { ParameterName = "ActivityID", ParameterValue = item.ActivityID.ToString() });
                //    var parameter = new DynamicParameters();
                //    parameters.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
                //    parameters.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
                //    parameters.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
                //    parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object); // Ensure proper type
                //    parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object); // Ensure proper type
                //    parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //    parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                //    var imageList = await connection.QueryAsync<ActivityImageMapping>(ActivityImageMappingQueries.Mapping_GetByID_Activity_Image, parameter, commandType: CommandType.StoredProcedure);
                //    item.ActivityImages = new List<ActivityImageMapping>();
                //    item.ActivityImages.AddRange(imageList);

                //}
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                var response = new ListResponseWrapper<SearchFilterThingsToDo> { Data = result.ToList() };return response;
            }
        }
  
    }
    //public async Task<SearchFilterThingsToDo> GetByIdAsync(long id)
    //{
    //    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequestById))
    //    {
    //        using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
    //        {
                
    //            connection.Open();
    //            var parameters = new DynamicParameters();
    //            parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
    //            parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
    //            parameters.Add("@CultureId", searchRequestById.CultureId, DbType.Int32);
    //            parameters.Add("@ID", searchRequestById.Id, DbType.Int32);

    //            var result = await connection.QuerySingleOrDefaultAsync<SearchFilterThingsToDo>(SearchFilterThingsToDoQueries.GetByID_SearchFilterThingsToDo, parameters, commandType: CommandType.StoredProcedure);
    //             (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
    //            return result;
    //        }
    //    }
    //}



    //public async Task<ResponseEntity> UpdateAsync(SearchFilterThingsToDo entity)
    //{
    //    try
    //    {

       
    //    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity))
    //    {
    //        using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
    //        {
    //            connection.Open();
    //            UpdateSearchFilterThingsToDoDTO updateSearchFilterThingsToDoDTO = _mapper.Map<UpdateSearchFilterThingsToDoDTO>(entity);
    //            var parameters = new DynamicParameters(updateSearchFilterThingsToDoDTO);
    //            parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
    //            parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
    //           // parameters.Add("@SearchFilterThingsToDoID ", dbType: DbType.Int32, direction: ParameterDirection.Output);

    //            var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(SearchFilterThingsToDoQueries.Update_SearchFilterThingsToDo, parameters, commandType: CommandType.StoredProcedure);
    //             (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
    //            return result;
    //        }
    //    }
    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }
    //}
}

/// <inheritdoc/>


/// <inheritdoc/>

