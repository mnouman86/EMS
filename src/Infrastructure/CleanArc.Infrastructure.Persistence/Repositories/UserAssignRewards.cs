using Azure.Core;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.UserAssignRewards;
using CleanArc.Application.Models.Advertisement;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.URL;
using CleanArc.Domain.Entities.UserAssignRewards;
using CleanArc.Domain.Entities.UserManagement;
using CleanArc.Infrastructure.Persistence.Helpers;
using CleanArc.Infrastructure.Sql;
using CleanArc.Infrastructure.Sql.SqlQueries;
using CleanArc.SharedKernel.Extensions;
using Dapper;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;using CleanArc.Application.Common;
using CleanArc.Domain.Entities.UserSignUpRewards;

namespace CleanArc.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for handling operations related to menus.
/// </summary>
/// <seealso cref="CleanArc.Application.Contracts.Persistence.IMenuRepository" />
public class UserAssignRewardsRepository : IUserAssignRewardsRepository
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
    private readonly ILogger<UserAssignRewardsRepository> _logger;

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
    public UserAssignRewardsRepository(IConfiguration configuration, IMapper mapper, ILogger<UserAssignRewardsRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
/// <inheritdoc/>
public async Task<ResponseEntity> AddAsync(UserAssignRewards UserAssignRewards)
{
    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, UserAssignRewards))
    {
        using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
        {
            connection.Open();
                CreateUserAssignRewardsDTO createUserAssignRewardsDTO = _mapper.Map<CreateUserAssignRewardsDTO>(UserAssignRewards);
                var parameters = new DynamicParameters(createUserAssignRewardsDTO);
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(UserSignUpRewardsQueries.Create_UserSignUpRewards, parameters, commandType: CommandType.StoredProcedure);
             (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
            return result;
        }
    }
}

    public Task<ResponseEntity> DeleteAsync(string selectedIds, int updatedBy, int? CultureId)
    {
        throw new NotImplementedException();
    }

    

   

    

    public Task<ResponseEntity> UpdateAsync(UserAssignRewards entity)
    {
        throw new NotImplementedException();
    }

    Task<ListResponseWrapper<UserAssignRewards>> IRepository<UserAssignRewards>.GetAllAsync(SearchRequest request)
    {
        throw new NotImplementedException();
    }

    Task<SingleResponseWrapper<UserAssignRewards>> IRepository<UserAssignRewards>.GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
}

/// <inheritdoc/>


/// <inheritdoc/>

