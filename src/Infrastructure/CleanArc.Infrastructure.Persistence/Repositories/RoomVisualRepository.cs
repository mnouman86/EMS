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
using CleanArc.Domain.Common;
using System.Data;
using CleanArc.Application.Common;

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
        public async Task<ResponseEntity> AddAsync(RoomVisual roomVisual)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, roomVisual))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();

                    // Create DataTable for image paths
                    var imagePathsTable = new DataTable();
                    imagePathsTable.Columns.Add("ImagePath", typeof(string));

                    if (roomVisual.ImagePaths != null)
                    {
                        foreach (var path in roomVisual.ImagePaths)
                        {
                            imagePathsTable.Rows.Add(path);
                        }
                    }

                    var parameters = new DynamicParameters();
                    parameters.Add("@HotelID", roomVisual.HotelID);
                    parameters.Add("@RoomID", roomVisual.RoomID);
                    parameters.Add("@CategoryID", roomVisual.CategoryID);
                    parameters.Add("@ImageTitle", roomVisual.ImageTitle);
                    parameters.Add("@ImagePaths", imagePathsTable.AsTableValuedParameter("ImagePathTableType"));
                    parameters.Add("@IsMain", roomVisual.IsMain);
                    parameters.Add("@CreatedBy", roomVisual.CreatedBy);
                    
                    var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
                        RoomVisualQueries.Creat_RoomImage,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    

                    (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    return result;
                }
            }
        }

        //public Task<ResponseEntity> DeleteAsync(string selectedIds, int updatedBy)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<ListResponseWrapper<RoomVisual>> GetAllAsync(SearchRequest request)
        {
            throw new NotImplementedException();
        }

        //public async Task<ListResponseWrapper<CarDetail>> GetAllAsync(SearchRequest searchRequest)
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
        //            var result = await connection.QueryAsync<CarDetail>(CarDetailQueries.GetALL_CarDetail, parameters, commandType: CommandType.StoredProcedure);
        //             (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
        //            var response = new ListResponseWrapper<ActivityAddressMapping> { Data = result.ToList(), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") };return response;
        //        }
        //    }
        //}
        public async Task<SingleResponseWrapper<RoomVisual>> GetByIdAsync(SearchRequestById searchRequestById)
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
					var result = await connection.QuerySingleOrDefaultAsync<RoomVisual>(RoomVisualQueries.GetByID_RoomImage, parameters, commandType: CommandType.StoredProcedure);
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                    var response = new SingleResponseWrapper<RoomVisual>
                    {
                        Data = result,
                        Code = parameters.Get<int>("@Code"),
                        Message = parameters.Get<string>("@Message")
                    };
                    return response;
                }
            }
        }

        public async Task<ResponseEntity> UpdateAsync(RoomVisual RoomVisual)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, RoomVisual))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();
                    UpdateRoomVisualDTO updateRoomVisualDTO = _mapper.Map<UpdateRoomVisualDTO>(RoomVisual);

                    var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(RoomVisualQueries.Update_RoomImage, updateRoomVisualDTO, commandType: CommandType.StoredProcedure);
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
                    parameters.Add("@CultureId", deleteRequest.CultureId);
                    var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(RoomVisualQueries.Delete_RoomImage, parameters, commandType: CommandType.StoredProcedure);
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                    return result;
                }
            }
        }
    }
}
