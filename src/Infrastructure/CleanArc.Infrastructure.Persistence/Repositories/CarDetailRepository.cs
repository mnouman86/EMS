using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.CarDetail;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.CarDetail;
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
using System.Data;
using CleanArc.Application.Common;
using CleanArc.Domain.Entities.KBDetail;

namespace CleanArc.Infrastructure.Persistence.Repositories
{
    public class CarDetailRepository : ICarDetailRepository
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
        private readonly ILogger<CarDetailRepository> _logger;

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
        public CarDetailRepository(IConfiguration configuration, IMapper mapper, ILogger<CarDetailRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this._mapper = mapper;
            this._logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <inheritdoc/>
        public async Task<ResponseEntity> AddAsync(CarDetail carDetail)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, carDetail))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();
                    CreateCarDetailDTO createCarDetailDTO = _mapper.Map<CreateCarDetailDTO>(carDetail);
                    var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(CarDetailQueries.Create_CarDetail, createCarDetailDTO, commandType: CommandType.StoredProcedure);
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    return result;
                }
            }
        }

        public async Task<ResponseEntity> DeleteAsync(DeleteRequest deleteRequest, int? updatedBy)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, deleteRequest))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("@Ids", deleteRequest.SelectedIds);
                    parameters.Add("@UpdatedBy", updatedBy);
                    parameters.Add("@CultureId", 1);
                    
                    var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(CarDetailQueries.Delete_CarDetail, parameters, commandType: CommandType.StoredProcedure);
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
                    return result;
                }
            }
        }

        public async Task<ListResponseWrapper<CarDetail>> GetAllAsync(SearchRequest searchRequest)
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
					var result = await connection.QueryAsync<CarDetail>(CarDetailQueries.GetALL_CarDetail, parameters, commandType: CommandType.StoredProcedure);

					(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
					var response = new ListResponseWrapper<CarDetail> { Data = result.ToList(), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") }; return response;
				}
            }
        }
        public async Task<SingleResponseWrapper<CarDetail>> GetByIdAsync(SearchRequestById searchRequestById)
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
					var result = await connection.QuerySingleOrDefaultAsync<CarDetail>(CarDetailQueries.GetByID_CarDetail, parameters, commandType: CommandType.StoredProcedure);
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    var response = new SingleResponseWrapper<CarDetail>
                    {
                        Data = result,
                        Code = parameters.Get<int>("@Code"),
                        Message = parameters.Get<string>("@Message")
                    };
                    return response;
                }
            }
        }



        public async Task<ResponseEntity> UpdateAsync(CarDetail carDetail)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, carDetail))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();
                    UpdateCarDetailDTO updateCarDetailDTO = _mapper.Map<UpdateCarDetailDTO>(carDetail);

                    var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(CarDetailQueries.Update_CarDetail, updateCarDetailDTO, commandType: CommandType.StoredProcedure);
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    return result;
                }
            }
        }
    }
}
