using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.SearchFilterStay;
using CleanArc.Domain.Entities.SearchHotelImage;
using CleanArc.Infrastructure.Persistence.Helpers;
using CleanArc.Infrastructure.Sql.SqlQueries;
using CleanArc.SharedKernel.Extensions;
using Dapper;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class SearchFilterStayRepository : ISearchFilterStayRepository
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
    private readonly ILogger<SearchFilterStayRepository> _logger;

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
    public SearchFilterStayRepository(IConfiguration configuration, IMapper mapper, ILogger<SearchFilterStayRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public Task<string> AddAsync(SearchFilterStay entity)
    {
        throw new NotImplementedException();
    }

    public Task<string> DeleteAsync(string selectedIds, int updatedBy, int? CultureId)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<SearchFilterStay>> GetAllAsync(SearchRequest searchRequest)
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
                var result = await connection.QueryAsync<SearchFilterStay>(SearchFilterStayQueries.GetAll_SearchDetail, parameters, commandType: CommandType.StoredProcedure);
                foreach (var item in result) {
                    List<FilterParameter> ImagesFilterArray = new List<FilterParameter>();
                    List<SortingParameter> ImagesSortingArray = new List<SortingParameter>();

                    ImagesFilterArray.Add(new FilterParameter { ParameterName = "HotelID", ParameterValue = item.HotelID.ToString() });
    var parameter = new
                    {
                        PageNumber = searchRequest.PageNumber,
                        PageSize = searchRequest.PageSize,
                        //SortingColumnName = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnName,
                        //SortingColumnDirection = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnDirection,
                        //FilterParameterName = searchRequest.FilterArray?.FirstOrDefault()?.ParameterName,
                        //FilterParameterValue = searchRequest.FilterArray?.FirstOrDefault()?.ParameterValue
                        SortingArray = DataTableHelper.ToDataTable(ImagesSortingArray), // Convert list to DataTable
                        FilterArray = DataTableHelper.ToDataTable(ImagesFilterArray) // Convert list to DataTable
                    };
                    var imageList = await connection.QueryAsync<HotelImage>(SearchHotelImageQueries.usp_GetByHotelID_HotelImage, parameter, commandType: CommandType.StoredProcedure);
                    item.HotelImages = new List<HotelImage>();
                    item.HotelImages.AddRange(imageList);
                }
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result.ToList();
            }
        }
    }

    public Task<SearchFilterStay> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<string> UpdateAsync(SearchFilterStay entity)
    {
        throw new NotImplementedException();
    }
}
