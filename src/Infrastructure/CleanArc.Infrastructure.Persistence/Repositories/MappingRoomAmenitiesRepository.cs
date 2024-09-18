using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.AgeType;
using CleanArc.Application.Models.MappingRoomAmenities;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.AgeType;
using CleanArc.Domain.Entities.MappingRoomAmenities;
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
    public class MappingRoomAmenitiesRepository : IMappingRoomAmenitiesRepository
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
        private readonly ILogger<MappingRoomAmenitiesRepository> _logger;

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
        public MappingRoomAmenitiesRepository(IConfiguration configuration, IMapper mapper, ILogger<MappingRoomAmenitiesRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this._mapper = mapper;
            this._logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <inheritdoc/>
        public async Task<ResponseEntity> AddAsync(MappingRoomAmenities mappingRoomAmenities)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, mappingRoomAmenities))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();
                    CreateMappingRoomAmenities createMappingRoomAmenities = _mapper.Map<CreateMappingRoomAmenities>(mappingRoomAmenities);
                    var result = await connection.ExecuteScalarAsync<ResponseEntity>(MappingRoomAmenitiesQueries.Create_Mapping_RoomAmenities, createMappingRoomAmenities, commandType: CommandType.StoredProcedure);
                    (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    return result;
                }
            }
        }

        public Task<ResponseEntity> DeleteAsync(string selectedIds, int updatedBy, int? CultureId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<MappingRoomAmenities>> GetAllAsync(SearchRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<MappingRoomAmenities> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseEntity> UpdateAsync(MappingRoomAmenities entity)
        {
            throw new NotImplementedException();
        }
    }
}
