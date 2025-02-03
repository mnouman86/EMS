using Azure.Core;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.KBDetail;
using CleanArc.Application.Models.Advertisement;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.URL;
using CleanArc.Domain.Entities.KBDetail;
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
using CleanArc.Domain.Entities.KBDescription;
using CleanArc.Domain.Entities.KBMedia;
using CleanArc.Domain.Entities.KBTiming;
using CleanArc.Domain.Entities.KBWhenToVisit;
using Mapster;
//using CleanArc.Application.Features.KBDetail.Queries.GetKBDetailByIdAll;

namespace CleanArc.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for handling operations related to menus.
/// </summary>
/// <seealso cref="CleanArc.Application.Contracts.Persistence.IMenuRepository" />
public class KBDetailRepository:IKBDetailRepository
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
    private readonly ILogger<KBDetailRepository> _logger;

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
    public KBDetailRepository(IConfiguration configuration, IMapper mapper, ILogger<KBDetailRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
/// <inheritdoc/>
public async Task<ResponseEntity> AddAsync(KBDetail KBDetail)
{
    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, KBDetail))
    {
        using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
        {
                connection.Open();
                CreateKBDetailDTO createKBDetailDTO = _mapper.Map<CreateKBDetailDTO>(KBDetail);
                //var timingsTable = new DataTable();
                //timingsTable.Columns.Add("GenericTitleID", typeof(string));
                //timingsTable.Columns.Add("Day", typeof(string));
                //timingsTable.Columns.Add("TimeFrom", typeof(string));
                //timingsTable.Columns.Add("TimeTo", typeof(string));
                //timingsTable.Columns.Add("IsAlwaysOpen", typeof(bool));

                //foreach (var timing in KBDetail.Timings)
                //{
                //    timingsTable.Rows.Add(timing.GenericTitleID, timing.Day, timing.TimeFrom, timing.TimeTo, timing.IsAlwaysOpen);
                //}
                var parameters = new DynamicParameters(createKBDetailDTO); 
                //parameters.Add("@TimingsTable", timingsTable.AsTableValuedParameter("AvailabilityTableType"));
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                parameters.Add("@GenericTitleID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(KBDetailQueries.Create_KBDetail, parameters, commandType: CommandType.StoredProcedure);
                //var result = await connection.QuerySingleOrDefaultAsync<int>(KBDetailQueries.Create_KBDetail, parameters, commandType: CommandType.StoredProcedure);

               // var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(KBDetailQueries.Create_KBDetail, parameters, commandType: CommandType.StoredProcedure);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
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

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(KBDetailQueries.Delete_KBDetail, parameters, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }

    public async Task<IReadOnlyList<KBDetail>> GetAllAsync(SearchRequest searchRequest)
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
                var result = await connection.QueryAsync<KBDetail>(KBDetailQueries.GetAll_KBDetail, parameters, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result.ToList();
            }
        }
    }

    public async Task<IReadOnlyList<KBMinimalDetail>> GetKBMinimalViewAsync(SearchRequest searchRequest)
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
                
                var result = await connection.QueryAsync<KBMinimalDetail>(KBDetailQueries.GetAll_KBMinimalDetail, parameters, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result.ToList();
            }
        }
    }

    public async Task<IReadOnlyList<CoreAreas>> GetKBCoreAreasMinimalViewAsync(SearchRequest searchRequest)
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
                parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object);
                parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object);
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = await connection.QueryAsync<CoreAreas, KBMinimalDetail,KBAddress, CoreAreas>(
                    KBDetailQueries.GetAll_KBCoreAreasMinimalDetail,
                    (coreArea, detail, address) =>
                    {
                        coreArea.KBMinimalDetails ??= new List<KBMinimalDetail>();
                        if (detail != null)
                        {
                            ((List<KBMinimalDetail>)coreArea.KBMinimalDetails).Add(detail);
                        }

                        coreArea.KBAddresses ??= new List<KBAddress>();
                        if (address != null)
                        {
                            ((List<KBAddress>)coreArea.KBAddresses).Add(address);
                        }
                        return coreArea;
                    },
                    parameters,
                    splitOn: "CoreAreaLookupID,KBDetailID",
                    commandType: CommandType.StoredProcedure
                );

                var groupedResults = result.GroupBy(ca => ca.CoreAreaLookupID)
                    .Select(g =>
                    {
                        var coreArea = g.First();
                        coreArea.KBMinimalDetails = g.SelectMany(x => x.KBMinimalDetails ?? Enumerable.Empty<KBMinimalDetail>()).ToList();
                        coreArea.KBAddresses = g.SelectMany(x => x.KBAddresses ?? Enumerable.Empty<KBAddress>()).ToList();
                        return coreArea;
                    });

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return groupedResults.ToList();
            }
        }
    }
    public async Task<KBDetail> GetByIdAsync(long id)
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

                //var resultKBDetail = await connection.QueryMultipleAsync(KBDetailQueries.GetByID_KBDetail, parameters, commandType: CommandType.StoredProcedure);
                //// var kbDetailAll = resultKBDetail.ReadFirst<KBDetail>();
                //var kbDetailSingle = resultKBDetail.Read<KBDetail>().ToList();
                //var kbDetailAddress = resultKBDetail.Read<KBAddress>().ToList();

                
                var result = await connection.QuerySingleOrDefaultAsync<KBDetail>(KBDetailQueries.GetByID_KBDetail, parameters , commandType: CommandType.StoredProcedure);
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
                return result;
            }
        }
    }

    public async Task<KnowledgeBaseByID> GetByIdAllAsync(long id)
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

                var result = await connection.QueryMultipleAsync(KBDetailQueries.GetByID_KBAllDetails, parameters, commandType: CommandType.StoredProcedure);
                // var kbDetailAll = resultKBDetail.ReadFirst<KBDetail>();
                var kbGenericTitle = result.Read<KnowledgeBaseByID>().FirstOrDefault();
                if (kbGenericTitle != null)
                {
                    var kbDetail = result.Read<KnowledgeBaseDetail>().FirstOrDefault();
                    kbGenericTitle.Detail=kbDetail;
                    var kbDescription = result.Read<KnowledgeBaseDescription>().ToList();
                    kbGenericTitle.Description = kbDescription;
                    var kbAddress = result.Read<KnowledgeBaseAddress>().ToList();
                    kbGenericTitle.Address = kbAddress;
                    var kbLocation = result.Read<KnowledgebaseLocation>().ToList();
                    kbGenericTitle.Location = kbLocation;
                    var kbMedia = result.Read<KnowledgeBaseMedia>().ToList();
                    kbGenericTitle.Media = kbMedia;
                    var kbTiming = result.Read<KnowledgeBaseTiming>().ToList();
                    kbGenericTitle.Timing = kbTiming;
                }

                //var result = await connection.QuerySingleOrDefaultAsync<KBDetail>(KBDetailQueries.GetByID_KBAllDetails, parameters, commandType: CommandType.StoredProcedure);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return kbGenericTitle;
            }
        }
    }

    public async Task<ResponseEntity> UpdateAsync(KBDetail entity)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdateKBDetailDTO updateKBDetailDTO = _mapper.Map<UpdateKBDetailDTO>(entity);
                //var timingsTable = new DataTable();
                //timingsTable.Columns.Add("GenericTitleID", typeof(string));
                //timingsTable.Columns.Add("Day", typeof(string));
                //timingsTable.Columns.Add("TimeFrom", typeof(string));
                //timingsTable.Columns.Add("TimeTo", typeof(string));
                //timingsTable.Columns.Add("IsAlwaysOpen", typeof(bool));

                //foreach (var timing in entity.Timings)
                //{
                //    timingsTable.Rows.Add(timing.GenericTitleID, timing.Day, timing.TimeFrom, timing.TimeTo, timing.IsAlwaysOpen);
                //}
                var parameters = new DynamicParameters(updateKBDetailDTO);
                //parameters.Add("@TimingsTable", timingsTable.AsTableValuedParameter("AvailabilityTableType"));
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(KBDetailQueries.Update_KBDetail, parameters, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }

    //public async Task<IReadOnlyList<KBDetail>> IKBDetailRepository.GetKBMinimalViewAsync(SearchRequest searchRequest)
    //{
    //    throw new NotImplementedException();
    //}
}

/// <inheritdoc/>


/// <inheritdoc/>

