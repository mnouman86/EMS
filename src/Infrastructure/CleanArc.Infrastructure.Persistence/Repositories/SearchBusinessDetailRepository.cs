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

    public Task<ResponseEntity> DeleteAsync(DeleteRequest deleteRequest, int? updatedBy)
    {
        throw new NotImplementedException();
    }

    public async Task<ListResponseWrapper<SearchBusinessDetail>> GetAllAsync(SearchRequest searchRequest)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
                if (searchRequest.PageSize > 0) parameters.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
                parameters.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
                parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object); // Ensure proper type
                parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object); // Ensure proper type
                parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
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
                    var imageList = await connection.QueryAsync<SearchCarImage>(SearchCarImageQueries.GetByCarID_CarImage, parameter, commandType: CommandType.StoredProcedure);
                    item.CarImages = new List<SearchCarImage>();
                    item.CarImages.AddRange(imageList);
                }
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                var response = new ListResponseWrapper<SearchBusinessDetail> { Data = result.ToList(), TotalCount = parameters.Get<int>("@TotalCount") };return response;
            }
        }
    }

    public Task<SingleResponseWrapper<SearchBusinessDetail>> GetByIdAsync(SearchRequestById searchRequestById)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseEntity> UpdateAsync(SearchBusinessDetail entity)
    {
        throw new NotImplementedException();
    }
}
