using Azure.Core;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.URL;
using CleanArc.Domain.Entities.Order;
using CleanArc.Domain.Entities.UserManagement;
using CleanArc.Infrastructure.Persistence.Helpers;
using CleanArc.Infrastructure.Persistence.Repositories.Common;
using CleanArc.Infrastructure.Sql;
using CleanArc.SharedKernel.Extensions;
using Dapper;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace CleanArc.Infrastructure.Persistence.Repositories;

//internal class URLRepository:DapperORM,IURLRepository
/// <summary>
/// Repository implementation for handling operations related to URLs.
/// </summary>
/// <seealso cref="CleanArc.Application.Contracts.Persistence.IURLRepository" />
public class URLRepository:IURLRepository
{
    /// <summary>
    /// The configuration
    /// </summary>
    private readonly IConfiguration configuration;
    /// <summary>
    /// The mapper
    /// </summary>
    private readonly IMapper _mapper;
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<URLRepository> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


    /// <summary>
    /// Initializes a new instance of the <see cref="URLRepository"/> class.
    /// </summary>
    /// <param name="configuration">The configuration for accessing application settings.</param>
    /// <param name="mapper">The mapper for mapping between different object types.</param>
    /// <param name="logger">The logger for logging repository-related information.</param>
    public URLRepository(IConfiguration configuration, IMapper mapper, ILogger<URLRepository> logger, IHttpContextAccessor httpContextAccessor)//:base()
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;

    }
    #region ===[ Private Members ]=============================================================




    #endregion

    #region ===[ Constructor ]=================================================================

    //public URLRepository(IConfiguration configuration)  
    //{
    //    this.configuration = configuration;
    //}

    #endregion

    #region ===[ IContactRepository Methods ]==================================================

    /// <inheritdoc/>
    public async Task<IReadOnlyList<URL>> GetAllAsync(SearchRequest searchRequest)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var parameters = new
                {
                    PageNumber = searchRequest.PageNumber,
                    PageSize = searchRequest.PageSize,
                    //SortingColumnName = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnName,
                    //SortingColumnDirection = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnDirection,
                    //FilterParameterName = searchRequest.FilterArray?.FirstOrDefault()?.ParameterName,
                    //FilterParameterValue = searchRequest.FilterArray?.FirstOrDefault()?.ParameterValue
                    SortingArray = DataTableHelper.ToDataTable(searchRequest.SortingArray), // Convert list to DataTable
                    FilterArray = DataTableHelper.ToDataTable(searchRequest.FilterArray) // Convert list to DataTable
                };
                var result = await connection.QueryAsync<URL>(UrlQueries.AllUrls, parameters, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result.ToList();
            }
        }
    }

    /// <inheritdoc/>
    public async Task<URL> GetByIdAsync(long id)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, id))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<URL>(UrlQueries.UrlById, new { ID = id }, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }

    /// <inheritdoc/>
    public async Task<ResponseEntity> AddAsync(URL entity)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                AddUrlDto addUrlDto = _mapper.Map<AddUrlDto>(entity);
                var result = await connection.ExecuteScalarAsync<ResponseEntity>(UrlQueries.AddUrl, addUrlDto, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }

    /// <inheritdoc/>
    public async Task<ResponseEntity> UpdateAsync(URL entity)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                UpdateUrlDto updateUrlDto = _mapper.Map<UpdateUrlDto>(entity);

                var result = await connection.ExecuteScalarAsync<ResponseEntity>(UrlQueries.UpdateUrl, updateUrlDto, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }

    /// <inheritdoc/>
    public async Task<ResponseEntity> DeleteAsync(string selectedIds, int updatedBy, int? CultureId)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, new { selectedIds, updatedBy }))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@ID", selectedIds);
                parameters.Add("@UpdatedBy", updatedBy);
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = await connection.ExecuteScalarAsync<ResponseEntity>(UrlQueries.DeleteURL, parameters, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }

    #endregion
   
}