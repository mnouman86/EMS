using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.MappingHotelAmenities;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.MappingHotelAmenities;
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

namespace CleanArc.Infrastructure.Persistence.Repositories
{
    public class AmenityMappingRepository : IAmenityMappingRepository
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
        private readonly ILogger<AmenityMappingRepository> _logger;

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
        public AmenityMappingRepository(IConfiguration configuration, IMapper mapper, ILogger<AmenityMappingRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this._mapper = mapper;
            this._logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseEntity> AddAsync(MappingHotelAmenities mappingHotelAmenities)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, mappingHotelAmenities))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();
                    CreateMappingHotelAmenities createMappingHotelAmenities = _mapper.Map<CreateMappingHotelAmenities>(mappingHotelAmenities);
                    var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(MappingHotelAmenitiesQueries.Create_Mapping_HotelAmenities, createMappingHotelAmenities, commandType: CommandType.StoredProcedure);
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    return result;
                }
            }
        }


        public Task<ResponseEntity> DeleteAsync(DeleteRequest deleteRequest, int? updatedBy)
        {
            throw new NotImplementedException();
        }

        public Task<ListResponseWrapper<MappingHotelAmenities>> GetAllAsync(SearchRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SingleResponseWrapper<MappingHotelAmenities>> GetByIdAsync(SearchRequestById searchRequestById)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseEntity> UpdateAsync(MappingHotelAmenities entity)
        {
            throw new NotImplementedException();
        }
    }
}
