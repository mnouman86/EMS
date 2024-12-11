using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.RoomImages;
using CleanArc.Domain.Entities.SearchHotelDetail;
using CleanArc.Domain.Entities.CarRentalSearchFilter;
using CleanArc.Domain.Entities.SearchRoomAmenities;
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

namespace CleanArc.Infrastructure.Persistence.Repositories
{

    public class CarRentalSearchFilterRepository : ICarRentalSearchFilterRepository
    {/// <summary>
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
        private readonly ILogger<CarRentalSearchFilterRepository> _logger;

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
        public CarRentalSearchFilterRepository(IConfiguration configuration, IMapper mapper, ILogger<CarRentalSearchFilterRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this._mapper = mapper;
            this._logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
       

       

        public async Task<IReadOnlyList<CarRentalSearchFilter>> GetAllWithParamAsync(CarRentalSearchFilterRequest searchRequest)
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
                         Name = searchRequest.Name,
                         CityName = searchRequest.CityName,
                         CarModel = searchRequest.CarModel,
                         MaxPrice = searchRequest.MaxPrice,
                         MinPrice = searchRequest.MinPrice,
                         CarAmenities = searchRequest.CarAmenities,

                         //SortingColumnName = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnName,
                         //SortingColumnDirection = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnDirection,
                         //FilterParameterName = searchRequest.FilterArray?.FirstOrDefault()?.ParameterName,
                         //FilterParameterValue = searchRequest.FilterArray?.FirstOrDefault()?.ParameterValue
                         SortingArray = DataTableHelper.ToDataTable(searchRequest.SortingArray), // Convert list to DataTable
                        FilterArray = DataTableHelper.ToDataTable(searchRequest.FilterArray) // Convert list to DataTable
                    };
                    var result = await connection.QueryAsync<CarRentalSearchFilter>(CarRentalSearchFilterQueries.GetAll_CarSearchFilter, parameters, commandType: CommandType.StoredProcedure);
                    foreach (var item in result)
                    {
                        List<FilterParameter> FilterArray = new List<FilterParameter>();
                        List<SortingParameter> SortingArray = new List<SortingParameter>();
                        FilterArray.Add(new FilterParameter { ParameterName = "CarId", ParameterValue = item.CarID.ToString() });
                        var parameter = new
                        {
                            PageNumber = searchRequest.PageNumber,
                            PageSize = searchRequest.PageSize,
                            SortingArray = DataTableHelper.ToDataTable(SortingArray), // Convert list to DataTable
                            FilterArray = DataTableHelper.ToDataTable(FilterArray) // Convert list to DataTable
                        };
                        var imageList = await connection.QueryAsync<SearchCarImage>(SearchCarImageQueries.usp_GetByCarID_CarImage, parameter, commandType: CommandType.StoredProcedure);
                        var amenitiesList = await connection.QueryAsync<SearchCarAmenities>(SearchCarAmenitiesQuery.usp_GetByCarID_CarAmenities, parameter, commandType: CommandType.StoredProcedure);
                        item.SearchCarImage = new List<SearchCarImage>();
                        item.SearchCarImage.AddRange(imageList);
                        item.SearchCarAmenities = new List<SearchCarAmenities>();
                        item.SearchCarAmenities.AddRange(amenitiesList);
                    }
                        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    return result.ToList();
                }
            }
        }

       
    }
}

