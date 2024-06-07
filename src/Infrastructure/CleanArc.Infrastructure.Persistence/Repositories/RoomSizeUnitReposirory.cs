using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.RoomSizeUnit;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class RoomSizeUnitReposirory : IRoomSizeUnitReposirory
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
    private readonly ILogger<RoomSizeUnitReposirory> _logger;

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
    public RoomSizeUnitReposirory(IConfiguration configuration, IMapper mapper, ILogger<RoomSizeUnitReposirory> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        _mapper = mapper;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public Task<string> AddAsync(RoomSizeUnit entity)
    {
        throw new NotImplementedException();
    }

    public Task<string> DeleteAsync(string selectedIds, int updatedBy)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<RoomSizeUnit>> GetAllAsync(SearchRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<RoomSizeUnit> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<string> UpdateAsync(RoomSizeUnit entity)
    {
        throw new NotImplementedException();
    }
}
