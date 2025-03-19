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
using Microsoft.Extensions.Logging; 
using CleanArc.Domain.Common;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using CleanArc.Application.Common;

namespace CleanArc.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for handling operations related to menus.
/// </summary>
/// <seealso cref="CleanArc.Application.Contracts.Persistence.IMenuRepository" />
public class MenuRepository : IMenuRepository
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
    private readonly ILogger<MenuRepository> _logger;

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
    public MenuRepository(IConfiguration configuration, IMapper mapper, ILogger<MenuRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc/>
    public async Task<ListResponseWrapper<Menu>> GetAllAsync(SearchRequest request)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var parameters = new
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    SortingArray = DataTableHelper.ToDataTable(request.SortingArray),
                    FilterArray = DataTableHelper.ToDataTable(request.FilterArray)
                };
                var result = await connection.QueryAsync<Menu>(UrlQueries.AllUrls, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
             
                var response = new ListResponseWrapper<Menu> { Data = result.ToList() };return response;
            }
        }
    }

    /// <inheritdoc/>
    public async Task<SingleResponseWrapper<Menu>> GetByIdAsync(SearchRequestById searchRequestById)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequestById))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Menu>(UrlQueries.UrlById, new { searchRequestById.Id,searchRequestById.CultureId }, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                var response = new SingleResponseWrapper<Menu>
                {
                    Data = result,
                    //Code = parameters.Get<int>("@Code"),
                    //Message = parameters.Get<string>("@Message")
                };
                return response;
            }
        }
    }

    /// <inheritdoc/>
    public async Task<ResponseEntity> AddAsync(Menu entity)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                AddUrlDto addUrlDto = _mapper.Map<AddUrlDto>(entity);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(UrlQueries.AddUrl, addUrlDto, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                
                return result;
            }
        }
    }

    /// <inheritdoc/>
    public async Task<ResponseEntity> UpdateAsync(Menu entity)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                UpdateUrlDto updateUrlDto = _mapper.Map<UpdateUrlDto>(entity);

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(UrlQueries.UpdateUrl, updateUrlDto, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }

    /// <inheritdoc/>
    public async Task<ResponseEntity> DeleteAsync(DeleteRequest deleteRequest, int? updatedBy)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, deleteRequest))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@Ids", deleteRequest.SelectedIds);
                parameters.Add("@UpdatedBy", updatedBy);
                parameters.Add("@CultureId", deleteRequest.CultureId);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(UrlQueries.DeleteURL, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
                return result;
            }
        }
    }
}