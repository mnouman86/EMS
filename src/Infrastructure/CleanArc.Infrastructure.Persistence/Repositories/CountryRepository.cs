using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Country;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.Country;
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
using CleanArc.Application.Common;
using CleanArc.Domain.Entities.PopularItemsVisit;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class CountryRepository : ICountryRepository
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
    private readonly ILogger<CountryRepository> _logger;

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
    public CountryRepository(IConfiguration configuration, IMapper mapper, ILogger<CountryRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        _mapper = mapper;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    /// <inheritdoc/>
    public async Task<ResponseEntity> AddAsync(Country country)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, country))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                CreateCountryDTO createCountryDTO = _mapper.Map<CreateCountryDTO>(country);
                var parameters = new DynamicParameters(createCountryDTO);
                //parameters.Add("@RecordId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(CountryQueries.Create_Country, parameters, commandType: CommandType.StoredProcedure);
                //if (result == null)
                //{
                //    result = parameters.MapToResponseEntity();
                //}
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
                //parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(CountryQueries.Delete_Country, parameters, commandType: CommandType.StoredProcedure);
                //if (result == null)
                //{
                //    result = parameters.MapToResponseEntity();
                //}
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                //if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
                return result;
            }
        }
    }

    public async Task<ListResponseWrapper<Country>> GetAllAsync(SearchRequest searchRequest)
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


				var result = await connection.QueryAsync<Country>(CountryQueries.GetALL_Country, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

				var response = new ListResponseWrapper<Country> { Data = result.ToList(), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") }; return response;


			}
		}
    }
    public async Task<SingleResponseWrapper<Country>> GetByIdAsync(SearchRequestById searchRequestById)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequestById))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
				var parameters = new DynamicParameters();
				parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
				parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
				parameters.Add("@CultureId", searchRequestById.CultureId, DbType.Int32);
				parameters.Add("@ID", searchRequestById.Id, DbType.Int32);
				var result = await connection.QuerySingleOrDefaultAsync<Country>(CountryQueries.GetByID_Country, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                var response = new SingleResponseWrapper<Country>
                {
                    Data = result,
                    Code = parameters.Get<int>("@Code"),
                    Message = parameters.Get<string>("@Message")
                };
                return response;
            }
        }
    }



    public async Task<ResponseEntity> UpdateAsync(Country country)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, country))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdateCountryDTO updateCountryDTO = _mapper.Map<UpdateCountryDTO>(country);
                var parameters = new DynamicParameters(updateCountryDTO);
                //parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(CountryQueries.Update_Country, parameters, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }
}


