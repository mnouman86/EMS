using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.MappingHotelLanguage;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.MappingHotelLanguage;
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
    public class MappingHotelLanguageRepository : IMappingHotelLanguageRepository
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
        private readonly ILogger<MappingHotelLanguageRepository> _logger;

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
        public MappingHotelLanguageRepository(IConfiguration configuration, IMapper mapper, ILogger<MappingHotelLanguageRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this._mapper = mapper;
            this._logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <inheritdoc/>
        public async Task<ResponseEntity> AddAsync(MappingHotelLanguage mappingHotelLanguage)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, mappingHotelLanguage))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();
                    CreateMappingHotelLanguageDTO createMappingHotelLanguageDTO = _mapper.Map<CreateMappingHotelLanguageDTO>(mappingHotelLanguage);
                    var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(MappingHotelLanguageQueries.Create_Mapping_HotelLanguages, createMappingHotelLanguageDTO, commandType: CommandType.StoredProcedure);
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    return result;
                }
            }
        }

        public Task<ResponseEntity> DeleteAsync(string selectedIds, int updatedBy, int? CultureId)
        {
            throw new NotImplementedException();
        }

        public Task<ListResponseWrapper<MappingHotelLanguage>> GetAllAsync(SearchRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SingleResponseWrapper<MappingHotelLanguage>> GetByIdAsync(SearchRequestById searchRequestById)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseEntity> UpdateAsync(MappingHotelLanguage entity)
        {
            throw new NotImplementedException();
        }
    }
}
