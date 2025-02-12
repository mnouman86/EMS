using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Business;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.Business;
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

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class BusinessRepository : IBusinessRepository
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
    private readonly ILogger<BusinessRepository> _logger;

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
    public BusinessRepository(IConfiguration configuration, IMapper mapper, ILogger<BusinessRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    /// <inheritdoc/>
    public async Task<ResponseEntity> AddAsync(Business business)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, business))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                CreateBusinessDTO createBusinessDTO = _mapper.Map<CreateBusinessDTO>(business);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(BusinessQueries.Create_Business, createBusinessDTO, commandType: CommandType.StoredProcedure);
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
                parameters.Add("@UpdatedBy", updatedBy);
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(BusinessQueries.Delete_Business, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
                return result;
            }
        }
    }

    public async Task<ListResponseWrapper<Business>> GetAllAsync(SearchRequest searchRequest)
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
                    //SortingColumnName = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnName,
                    //SortingColumnDirection = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnDirection,
                    //FilterParameterName = searchRequest.FilterArray?.FirstOrDefault()?.ParameterName,
                    //FilterParameterValue = searchRequest.FilterArray?.FirstOrDefault()?.ParameterValue
                    SortingArray = DataTableHelper.ToDataTable(searchRequest.SortingArray), // Convert list to DataTable
                    FilterArray = DataTableHelper.ToDataTable(searchRequest.FilterArray) // Convert list to DataTable
                };
                var result = await connection.QueryAsync<Business>(BusinessQueries.usp_GetAll_Business, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                var response = new ListResponseWrapper<Business> { Data = result.ToList() };return response;
            }
        }
    }
    public async Task<SingleResponseWrapper<Business>> GetByIdAsync(long id)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, id))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Business>(BusinessQueries.usp_GetByID_Business, new { ID = id }, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                var response = new SingleResponseWrapper<Business>
                {
                    Data = result,
                    //Code = parameters.Get<int>("@Code"),
                    //Message = parameters.Get<string>("@Message")
                };
                return response;
            }
        }
    }



    public async Task<ResponseEntity> UpdateAsync(Business business)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, business))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdateBusinessDTO updateBusinessDTO = _mapper.Map<UpdateBusinessDTO>(business);

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(BusinessQueries.Update_Business, updateBusinessDTO, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }
}
/// <inheritdoc/>


/// <inheritdoc/>


