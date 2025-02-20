using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.SearchHotelDetail;
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
using System.Reflection.Metadata;
using CleanArc.Domain.Entities.PopularItemsVisit;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class SearchHotelRepository : ISearchHotelRepository
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
    private readonly ILogger<SearchHotelRepository> _logger;

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
    public SearchHotelRepository(IConfiguration configuration, IMapper mapper, ILogger<SearchHotelRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public Task<ResponseEntity> AddAsync(SearchHotelDetail entity)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseEntity> DeleteAsync(string selectedIds, int updatedBy, int? CultureId)
    {
        throw new NotImplementedException();
    }

    public async Task<ListResponseWrapper<SearchHotelDetail>> GetAllAsync(SearchRequest searchRequest)
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


				var result = await connection.QueryAsync<SearchHotelDetail>(SearchHotelDetailQueries.GetAll_SearchDetail, parameters, commandType: CommandType.StoredProcedure);
                foreach (var item in result) {
                    List<FilterParameter> ImagesFilterArray = new List<FilterParameter>();
                    List<SortingParameter> ImagesSortingArray = new List<SortingParameter>();

                    ImagesFilterArray.Add(new FilterParameter { ParameterName = "genericTitleID", ParameterValue = item.HotelID.ToString() });
					

					var parameter = new DynamicParameters();
					parameter.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
					parameter.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
					parameter.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
					parameter.Add("@SortingArray", DataTableHelper.ToDataTable(ImagesSortingArray), DbType.Object); // Ensure proper type
					parameter.Add("@FilterArray", DataTableHelper.ToDataTable(ImagesFilterArray), DbType.Object); // Ensure proper type
					parameter.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
					parameter.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
					var imageList = await connection.QueryAsync<HotelImage>(SearchHotelImageQueries.usp_GetByHotelID_HotelImage, parameter, commandType: CommandType.StoredProcedure);
                    item.HotelImages = new List<HotelImage>();
                    item.HotelImages.AddRange(imageList);
                }
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
				var response = new ListResponseWrapper<SearchHotelDetail> { Data = result.ToList(), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") }; return response;

			}
		}
    }

    public Task<SingleResponseWrapper<SearchHotelDetail>> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseEntity> UpdateAsync(SearchHotelDetail entity)
    {
        throw new NotImplementedException();
    }
}
