using Azure.Core;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.KBDetail;
using CleanArc.Application.Models.Advertisement;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.URL;
using CleanArc.Domain.Entities.KBAddress;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;using CleanArc.Application.Common;
using CleanArc.Domain.Entities.KBDescription;
using CleanArc.Domain.Entities.KBMedia;
using CleanArc.Domain.Entities.KBTiming;
using CleanArc.Domain.Entities.KBWhenToVisit;
using Mapster;
using CleanArc.Application.Models.KBAddress;
//using CleanArc.Application.Features.KBDetail.Queries.GetKBDetailByIdAll;

namespace CleanArc.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for handling operations related to menus.
/// </summary>
/// <seealso cref="CleanArc.Application.Contracts.Persistence.IMenuRepository" />
public class KBAddressRepository : IKBAddressRepository
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
    private readonly ILogger<KBAddressRepository> _logger;

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
    public KBAddressRepository(IConfiguration configuration, IMapper mapper, ILogger<KBAddressRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
/// <inheritdoc/>
public async Task<ResponseEntity> AddAsync(KBAddress KBAddress)
{
    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, KBAddress))
    {
        using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
        {
                connection.Open();
                CreateKBAddressDTO createKBAddressDTO = _mapper.Map<CreateKBAddressDTO>(KBAddress);
                var timingsTable = new DataTable();
                //timingsTable.Columns.Add("GenericTitleID", typeof(string));
                timingsTable.Columns.Add("Day", typeof(string));
                timingsTable.Columns.Add("TimeFrom", typeof(string));
                timingsTable.Columns.Add("TimeTo", typeof(string));
                timingsTable.Columns.Add("IsAlwaysOpen", typeof(bool));
                timingsTable.Columns.Add("IsClosed", typeof(bool));

                foreach (var timing in KBAddress.Timings)
                {
                    timingsTable.Rows.Add(timing.Day, timing.TimeFrom, timing.TimeTo, timing.IsAlwaysOpen,timing.IsClosed);
                }
                var parameters = new DynamicParameters(createKBAddressDTO); 
                parameters.Add("@TimingsTable", timingsTable.AsTableValuedParameter("AvailabilityTableType"));
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                //parameters.Add("@GenericTitleID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(KBAddressQueries.Create_KBAddress, parameters, commandType: CommandType.StoredProcedure);
                //var result = await connection.QuerySingleOrDefaultAsync<int>(KBDetailQueries.Create_KBDetail, parameters, commandType: CommandType.StoredProcedure);

               // var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(KBDetailQueries.Create_KBDetail, parameters, commandType: CommandType.StoredProcedure);
             (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
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
                
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(KBAddressQueries.Delete_KBAddress, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
                return result;
            }
        }
    }

    public async Task<ListResponseWrapper<KBAddress>> GetAllAsync(SearchRequest searchRequest)
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
                //parameters.Add("@KbDetailID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                //    var parameters = new
                //    {
                //        PageNumber = searchRequest.PageNumber,
                //        PageSize = searchRequest.PageSize,
                //        //SortingColumnName = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnName,
                //        //SortingColumnDirection = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnDirection,
                //        //FilterParameterName = searchRequest.FilterArray?.FirstOrDefault()?.ParameterName,
                //        //FilterParameterValue = searchRequest.FilterArray?.FirstOrDefault()?.ParameterValue
                //        SortingArray = DataTableHelper.ToDataTable(searchRequest.SortingArray), // Convert list to DataTable
                //        FilterArray = DataTableHelper.ToDataTable(searchRequest.FilterArray), // Convert list to DataTable
                //        Code = ("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output),
                //        Message = ("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output)

                //};
                var result = await connection.QueryAsync<KBAddress>(KBAddressQueries.GetAll_KBAddresses, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                var response = new ListResponseWrapper<KBAddress> { Data = result.ToList(), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") };return response;
            }
        }
    }

     public async Task<SingleResponseWrapper<KBAddress>> GetByIdAsync(SearchRequestById searchRequestById)
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

                //var resultKBDetail = await connection.QueryMultipleAsync(KBDetailQueries.GetByID_KBDetail, parameters, commandType: CommandType.StoredProcedure);
                //// var kbDetailAll = resultKBDetail.ReadFirst<KBDetail>();
                //var kbDetailSingle = resultKBDetail.Read<KBDetail>().ToList();
                //var kbDetailAddress = resultKBDetail.Read<KBAddress>().ToList();

                
                var result = await connection.QuerySingleOrDefaultAsync<KBAddress>(KBAddressQueries.GetByID_KBAddress, parameters , commandType: CommandType.StoredProcedure);
                //var result = await connection.QueryAsync<KBDetail, KBDescription, KBAddress,KBMedia, KBDetail>(
                //    KBDetailQueries.GetByID_KBDetail,
                //    (detail, description, address,media) =>
                //    {
                //        detail.KBDescriptions ??= new List<KBDescription>();
                //        if (description != null)
                //        {
                //            ((List<KBDescription>)detail.KBDescriptions).Add(description);
                //        }
                //        detail.KBAddresses ??= new List<KBAddress>();
                //        if (address != null)
                //        {
                //            ((List<KBAddress>)detail.KBAddresses).Add(address);
                //        }
                //        detail.KBMedias ??= new List<KBMedia>();
                //        if (media != null)
                //        {
                //            ((List<KBMedia>)detail.KBMedias).Add(media);
                //        }
                //        //detail.KBTimings ??= new List<KBTiming>();
                //        //if (timing != null)
                //        //{
                //        //    ((List<KBTiming>)detail.KBTimings).Add(timing);
                //        //}
                //        //detail.KBWhenToVisits ??= new List<KBWhenToVisit>();
                //        //if (whenToVisit != null)
                //        //{
                //        //    ((List<KBWhenToVisit>)detail.KBWhenToVisits).Add(whenToVisit);
                //        //}
                //        return detail;
                //    },
                //    parameters,
                //    splitOn: "KBDetailID,KBDescriptionID,AddressID,MediaID",
                //    commandType: CommandType.StoredProcedure
                //);

                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                var response = new SingleResponseWrapper<KBAddress>
                {
                    Data = result,
                    Code = parameters.Get<int>("@Code"),
                    Message = parameters.Get<string>("@Message")
                };
                return response;
            }
        }
    }

    public async Task<ResponseEntity> UpdateAsync(KBAddress entity)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdateKBAddressDTO updateKBAddressDTO = _mapper.Map<UpdateKBAddressDTO>(entity);
                var timingsTable = new DataTable();
                //timingsTable.Columns.Add("GenericTitleID", typeof(string));
                timingsTable.Columns.Add("Day", typeof(string));
                timingsTable.Columns.Add("TimeFrom", typeof(string));
                timingsTable.Columns.Add("TimeTo", typeof(string));
                timingsTable.Columns.Add("IsAlwaysOpen", typeof(bool));
                timingsTable.Columns.Add("IsClosed", typeof(bool));

                foreach (var timing in entity.Timings)
                {
                    timingsTable.Rows.Add(timing.Day, timing.TimeFrom, timing.TimeTo, timing.IsAlwaysOpen,timing.IsClosed);
                }
                var parameters = new DynamicParameters(updateKBAddressDTO);
                parameters.Add("@TimingsTable", timingsTable.AsTableValuedParameter("AvailabilityTableType"));
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(KBAddressQueries.Update_KBAddress, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
                return result;
            }
        }
    }

    //public async Task<ListResponseWrapper<KBDetail>> IKBDetailRepository.GetKBMinimalViewAsync(SearchRequest searchRequest)
    //{
    //    throw new NotImplementedException();
    //}
}

/// <inheritdoc/>


/// <inheritdoc/>

