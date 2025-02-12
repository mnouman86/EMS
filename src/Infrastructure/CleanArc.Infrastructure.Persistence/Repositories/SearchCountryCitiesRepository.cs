using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.SearchCountryCities;
using CleanArc.Domain.Entities.SearchHotelDetail;
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

public class SearchCountryCitiesRepository : ISearchCountryCitiesRepository
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
    private readonly ILogger<SearchCountryCitiesRepository> _logger;

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
    public SearchCountryCitiesRepository(IConfiguration configuration, IMapper mapper, ILogger<SearchCountryCitiesRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<ResponseEntity> AddAsync(SearchCountryCities entity)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseEntity> DeleteAsync(string selectedIds, int updatedBy, int? CultureId)
    {
        throw new NotImplementedException();
    }

    //public Task<ListResponseWrapper<SearchCountryCities>> GetAllAsync(SearchRequest request)
    //{
    //    throw new NotImplementedException();
    //}
    public async Task<ListResponseWrapper<SearchCountryCities>> GetAllAsync(SearchRequest searchRequest)
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
                var result = await connection.QueryAsync<SearchCountryCities>(SearchCountryCitiesQueries.usp_GetALL_Cities, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                var response = new ListResponseWrapper<SearchCountryCities> { Data = result.ToList() };return response;
            }
        }
    }


    public Task<SingleResponseWrapper<SearchCountryCities>> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseEntity> UpdateAsync(SearchCountryCities entity)
    {
        throw new NotImplementedException();
    }
}
