using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.RoomVisual;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.RoomVisual;
using CleanArc.Infrastructure.Persistence.Helpers;
using CleanArc.Infrastructure.Sql.SqlQueries;
using CleanArc.SharedKernel.Extensions;
using Dapper;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CleanArc.Infrastructure.Persistence.Repositories
{
    public class RoomVisualRepository : IRoomVisualRepository
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
        private readonly ILogger<RoomVisualRepository> _logger;

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
        public RoomVisualRepository(IConfiguration configuration, IMapper mapper, ILogger<RoomVisualRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this._mapper = mapper;
            this._logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <inheritdoc/>
        public async Task<string> AddAsync(RoomVisual RoomVisual)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, RoomVisual))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();
                    CreateRoomVisualDTO createRoomVisualDTO = _mapper.Map<CreateRoomVisualDTO>(RoomVisual);
                    var result = await connection.ExecuteAsync(RoomVisualQueries.Creat_RoomImage, createRoomVisualDTO, commandType: CommandType.StoredProcedure);
                    (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    return result.ToString();
                }
            }
        }

        //public Task<string> DeleteAsync(string selectedIds, int updatedBy)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<IReadOnlyList<RoomVisual>> GetAllAsync(SearchRequest request)
        {
            throw new NotImplementedException();
        }

        //public async Task<IReadOnlyList<CarDetail>> GetAllAsync(SearchRequest searchRequest)
        //{
        //    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
        //    {
        //        using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
        //        {
        //            connection.Open();
        //            var parameters = new
        //            {
        //                PageNumber = searchRequest.PageNumber,
        //                PageSize = searchRequest.PageSize,
        //                //SortingColumnName = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnName,
        //                //SortingColumnDirection = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnDirection,
        //                //FilterParameterName = searchRequest.FilterArray?.FirstOrDefault()?.ParameterName,
        //                //FilterParameterValue = searchRequest.FilterArray?.FirstOrDefault()?.ParameterValue
        //                SortingArray = DataTableHelper.ToDataTable(searchRequest.SortingArray), // Convert list to DataTable
        //                FilterArray = DataTableHelper.ToDataTable(searchRequest.FilterArray) // Convert list to DataTable
        //            };
        //            var result = await connection.QueryAsync<CarDetail>(CarDetailQueries.usp_GetALL_CarDetail, parameters, commandType: CommandType.StoredProcedure);
        //            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        //            return result.ToList();
        //        }
        //    }
        //}
        public async Task<RoomVisual> GetByIdAsync(long id)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, id))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();
                    var result = await connection.QuerySingleOrDefaultAsync<RoomVisual>(RoomVisualQueries.usp_GetByID_RoomImage, new { ID = id }, commandType: CommandType.StoredProcedure);
                    (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    return result;
                }
            }
        }

        public async Task<string> UpdateAsync(RoomVisual RoomVisual)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, RoomVisual))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();
                    UpdateRoomVisualDTO updateRoomVisualDTO = _mapper.Map<UpdateRoomVisualDTO>(RoomVisual);

                    var result = await connection.ExecuteAsync(RoomVisualQueries.Update_RoomImage, updateRoomVisualDTO, commandType: CommandType.StoredProcedure);
                    (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    return result.ToString();
                }
            }
        }
        public async Task<string> DeleteAsync(string selectedIds, int updatedBy, int? CultureId)
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
                    var result = await connection.ExecuteAsync(RoomVisualQueries.Delete_RoomImage, parameters, commandType: CommandType.StoredProcedure);
                    (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    return result.ToString();
                }
            }
        }
    }
}
