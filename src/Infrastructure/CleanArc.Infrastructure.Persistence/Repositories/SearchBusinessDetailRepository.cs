using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.SearchBusinessDetail;
using CleanArc.Domain.Entities.SearchHotelImage;
using CleanArc.Infrastructure.Persistence.Helpers;
using CleanArc.Infrastructure.Sql.SqlQueries;
using CleanArc.SharedKernel.Extensions;
using Dapper;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class SearchBusinessDetailRepository : ISearchBusinessDetailRepository
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
    private readonly ILogger<SearchBusinessDetailRepository> _logger;

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
    public SearchBusinessDetailRepository(IConfiguration configuration, IMapper mapper, ILogger<SearchBusinessDetailRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public Task<ResponseEntity> AddAsync(SearchBusinessDetail entity)
    {
        throw new NotImplementedException();
    }

    public Task<string> DeleteAsync(string selectedIds, int updatedBy, int? CultureId)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<SearchBusinessDetail>> GetAllAsync(SearchRequest searchRequest)
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
                var result = await connection.QueryAsync<SearchBusinessDetail>(SearchBusinessDetailQueries.GetAll_SearchBusinessDetail, parameters, commandType: CommandType.StoredProcedure);
                foreach (var item in result) {
                    List<FilterParameter> ImagesFilterArray = new List<FilterParameter>();
                    List<SortingParameter> ImagesSortingArray = new List<SortingParameter>();

                    ImagesFilterArray.Add(new FilterParameter { ParameterName = "CarID", ParameterValue = item.CarID.ToString() });
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
                    var imageList = await connection.QueryAsync<SearchCarImage>(SearchCarImageQueries.usp_GetByCarID_CarImage, parameter, commandType: CommandType.StoredProcedure);
                    item.CarImages = new List<SearchCarImage>();
                    item.CarImages.AddRange(imageList);
                }
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result.ToList();
            }
        }
    }

    public Task<SearchBusinessDetail> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<string> UpdateAsync(SearchBusinessDetail entity)
    {
        throw new NotImplementedException();
    }
}
