using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.SearchCountryCities;
using CleanArc.Domain.Entities.SearchHotelAmenities;
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
using CleanArc.Domain.Entities.KBDetail;

namespace CleanArc.Infrastructure.Persistence.Repositories
{
    public class SearchHotelAmenitiesRepository : ISearchHotelAmenitiesRepository
    {    /// <summary>
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
        private readonly ILogger<SearchHotelAmenitiesRepository> _logger;

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
        public SearchHotelAmenitiesRepository(IConfiguration configuration, IMapper mapper, ILogger<SearchHotelAmenitiesRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this._mapper = mapper;
            this._logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public Task<ResponseEntity> AddAsync(SearchHotelAmenities entity)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseEntity> DeleteAsync(string selectedIds, int updatedBy, int? CultureId)
        {
            throw new NotImplementedException();
        }

        public async Task<ListResponseWrapper<SearchHotelAmenities>> GetAllAsync(SearchRequest searchRequest)
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
					var result = await connection.QueryAsync<SearchHotelAmenities>(SearchHotelAmenitiesQuery.GetByHotelID_HotelAmenities, parameters, commandType: CommandType.StoredProcedure);
                

					(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
					var response = new ListResponseWrapper<SearchHotelAmenities> { Data = result.ToList(), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") }; return response;
				}
            }
        }

        public Task<SingleResponseWrapper<SearchHotelAmenities>> GetByIdAsync(SearchRequestById searchRequestById)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseEntity> UpdateAsync(SearchHotelAmenities entity)
        {
            throw new NotImplementedException();
        }
    }
}
