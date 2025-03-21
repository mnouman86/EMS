using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.HotelImage;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.HotelImage;
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
using System.Globalization;
using CleanArc.Application.Models.KBDetail;
using CleanArc.Application.Common;
using CleanArc.Application.Models.ActivityIDImageMapping;

namespace CleanArc.Infrastructure.Persistence.Repositories
{
    public class HotelImageRepository : IHotelImageRepository
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
        private readonly ILogger<HotelImageRepository> _logger;

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
        public HotelImageRepository(IConfiguration configuration, IMapper mapper, ILogger<HotelImageRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this._mapper = mapper;
            this._logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <inheritdoc/>
        public async Task<ResponseEntity> AddAsync(Hotel_Image HotelImage)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, HotelImage))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();
                    CreateHotelImageDTO createHotelImageDTO = _mapper.Map<CreateHotelImageDTO>(HotelImage);
                    var parameters = new DynamicParameters(createHotelImageDTO);
                    var imagePathsTable = new DataTable();
                    imagePathsTable.Columns.Add("ImagePath", typeof(string));

                    if (HotelImage.ImagePaths != null)
                    {
                        foreach (var path in HotelImage.ImagePaths)
                        {
                            imagePathsTable.Rows.Add(path);
                        }
                    }
                    parameters.Add("@ImagePaths", imagePathsTable.AsTableValuedParameter("ImagePathTableType"));
                    var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(HotelImageQueries.Create_HotelImage, createHotelImageDTO, commandType: CommandType.StoredProcedure);
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    return result;
                }
            }
        }

        //public Task<ResponseEntity> DeleteAsync(string selectedIds, int updatedBy)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<ListResponseWrapper<Hotel_Image>> GetAllAsync(SearchRequest request)
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
        //             (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
        //            var response = new ListResponseWrapper<ActivityAddressMapping> { Data = result.ToList(), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") };return response;
        //        }
        //    }
        //}
        public async Task<SingleResponseWrapper<Hotel_Image>> GetByIdAsync(SearchRequestById searchRequestById)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequestById))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {

                    connection.Open();
					var parameters = new DynamicParameters();
                    parameters.Add("@CultureId", searchRequestById.CultureId, DbType.Int32);
                    parameters.Add("@ID", searchRequestById.Id, DbType.Int32);
                    parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
					parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
					var result = await connection.QuerySingleOrDefaultAsync<Hotel_Image>(HotelImageQueries.GetByID_HotelImage, parameters, commandType: CommandType.StoredProcedure);
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    var response = new SingleResponseWrapper<Hotel_Image>
                    {
                        Data = result,
                        Code = parameters.Get<int>("@Code"),
                        Message = parameters.Get<string>("@Message")
                    };
                    return response;
                }
            }
        }

        public async Task<ResponseEntity> UpdateAsync(Hotel_Image HotelImage)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, HotelImage))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();
                    UpdateHotelImageDTO updateHotelImageDTO = _mapper.Map<UpdateHotelImageDTO>(HotelImage);
					var parameters = new DynamicParameters(updateHotelImageDTO);
                    var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(HotelImageQueries.Update_HotelImage, parameters, commandType: CommandType.StoredProcedure);
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
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
					parameters.Add("@CultureID", 1);
					parameters.Add("@UpdatedBy", updatedBy);
                    var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(HotelImageQueries.Delete_HotelImage, parameters, commandType: CommandType.StoredProcedure);
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
                    return result;
                }
            }
        }
    }
}
