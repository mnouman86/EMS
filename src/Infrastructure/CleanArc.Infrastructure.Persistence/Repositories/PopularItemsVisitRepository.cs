using Azure.Core;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.PopularItemsVisit;
using CleanArc.Application.Models.Advertisement;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.URL;
using CleanArc.Domain.Entities.PopularItemsVisit;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CleanArc.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for handling operations related to menus.
/// </summary>
/// <seealso cref="CleanArc.Application.Contracts.Persistence.IMenuRepository" />
public class PopularItemsVisitRepository:IPopularItemsVisitRepository
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
    private readonly ILogger<PopularItemsVisitRepository> _logger;

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
    public PopularItemsVisitRepository(IConfiguration configuration, IMapper mapper, ILogger<PopularItemsVisitRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
/// <inheritdoc/>
public async Task<ResponseEntity> AddAsync(PopularItemsVisit PopularItemsVisit)
{
    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, PopularItemsVisit))
    {
        using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
        {
            connection.Open();
                CreatePopularItemsVisitDTO createPopularItemsVisitDTO = _mapper.Map<CreatePopularItemsVisitDTO>(PopularItemsVisit);
                var parameters = new DynamicParameters(createPopularItemsVisitDTO);
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(PopularItemsVisitQueries.Create_PopularItemsVisit, parameters, commandType: CommandType.StoredProcedure);
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

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(PopularItemsVisitQueries.Delete_PopularItemsVisit, parameters, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }

    public async Task<IReadOnlyList<PopularItemsVisit>> GetAllAsync(SearchRequest searchRequest)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
                parameters.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
                //parameters.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
                parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object); // Ensure proper type
                parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object); // Ensure proper type
               // parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
               // parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

               


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
               var result = await connection.QueryAsync<PopularItemsVisit>(PopularItemsVisitQueries.GetAll_PopularItemsVisit, parameters, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //List<PopularItemsVisit> popularItemsVisit = new List<PopularItemsVisit>()
                //{
                //    new PopularItemsVisit()
                //    {
                //    PageId = 1,
                //    URL = "https://example.com/page1",
                //    PageTitle = "Example Page 1",
                //    Count = 10,
                //    ImageURL = "https://example.com/image1.jpg"
                //    },
                //    new PopularItemsVisit()
                //   {
                //    PageId = 2,
                //    URL = "https://example.com/page2",
                //    PageTitle = "Example Page 2",
                //    Count = 5,
                //    ImageURL = "https://example.com/image2.jpg"
                //}

                //};
                //return await Task.FromResult( popularItemsVisit);
                return result.ToList();


            }
        }
    }
    public async Task<PopularItemsVisit> GetByIdAsync(long id)
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

                var result = await connection.QuerySingleOrDefaultAsync<PopularItemsVisit>(PopularItemsVisitQueries.GetByID_PopularItemsVisit, parameters , commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }



    public async Task<ResponseEntity> UpdateAsync(PopularItemsVisit entity)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdatePopularItemsVisitDTO updatePopularItemsVisitDTO = _mapper.Map<UpdatePopularItemsVisitDTO>(entity);
                var parameters = new DynamicParameters(updatePopularItemsVisitDTO);
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(PopularItemsVisitQueries.update_PopularItemsVisit, parameters, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }
}

/// <inheritdoc/>


/// <inheritdoc/>

