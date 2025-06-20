using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.GenericMedia;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.GenericMedia;
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
using CleanArc.Domain.Entities.SearchHotelImage;
using Azure.Core;

namespace CleanArc.Infrastructure.Persistence.Repositories
{
    public class GenericMediaRepository : IGenericMediaRepository
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
        private readonly ILogger<GenericMediaRepository> _logger;

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
        public GenericMediaRepository(IConfiguration configuration, IMapper mapper, ILogger<GenericMediaRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this._mapper = mapper;
            this._logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <inheritdoc/>
        public async Task<ResponseEntity> AddAsync(GenericMedia Images)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, Images))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();
                    CreateGenericMediaDTO createHotelImageDTO = _mapper.Map<CreateGenericMediaDTO>(Images);
                    var parameters = new DynamicParameters(createHotelImageDTO);
                    var imagePathsTable = new DataTable();
                    imagePathsTable.Columns.Add("ImagePath", typeof(string));
                    imagePathsTable.Columns.Add("ImageTitle", typeof(string));
                    imagePathsTable.Columns.Add("IsMain", typeof(bool));

                    if (Images.Images != null)
                    {
                        foreach (var image in Images.Images)
                        {
                            imagePathsTable.Rows.Add(image.ImagePath, image.ImageTitle,
                                image.IsMain);
                        }
                    }
                    parameters.Add("@ImagePaths", imagePathsTable.AsTableValuedParameter("GenericImageTableType"));

                    var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(GenericMediaQueries.Create_HotelImage, parameters, commandType: CommandType.StoredProcedure);
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    return result;
                }
            }
        }

        //public Task<ResponseEntity> DeleteAsync(string selectedIds, int updatedBy)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<ListResponseWrapper<GenericMedia>> GetAllAsync(SearchRequest searchRequest)
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

                    var result = await connection.QueryAsync<GenericMedia>(GenericMediaQueries.GetAll_HotelImage, parameters, commandType: CommandType.StoredProcedure);
                    (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                    var response = new ListResponseWrapper<GenericMedia> { Data = result.ToList(), TotalCount = parameters.Get<int>("@TotalCount"), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") }; return response;
                }
            }
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
        public async Task<SingleResponseWrapper<GenericMedia>> GetByIdAsync(SearchRequestById searchRequestById)
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
					var result = await connection.QuerySingleOrDefaultAsync<GenericMedia>(GenericMediaQueries.GetByID_HotelImage, parameters, commandType: CommandType.StoredProcedure);
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    var response = new SingleResponseWrapper<GenericMedia>
                    {
                        Data = result,
                        Code = parameters.Get<int>("@Code"),
                        Message = parameters.Get<string>("@Message")
                    };
                    return response;
                }
            }
        }

        public async Task<ResponseEntity> UpdateAsync(GenericMedia HotelImage)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, HotelImage))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();
                    UpdateGenericMediaDTO updateHotelImageDTO = _mapper.Map<UpdateGenericMediaDTO>(HotelImage);
					var parameters = new DynamicParameters(updateHotelImageDTO);
                    var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(GenericMediaQueries.Update_HotelImage, parameters, commandType: CommandType.StoredProcedure);
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
					parameters.Add("@CultureID", deleteRequest.CultureId);
					parameters.Add("@UpdatedBy", updatedBy);
                    var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(GenericMediaQueries.Delete_HotelImage, parameters, commandType: CommandType.StoredProcedure);
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                    return result;
                }
            }
        }
    }
}
