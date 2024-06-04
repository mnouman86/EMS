using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.Category;
using CleanArc.Infrastructure.Persistence.Helpers;
using CleanArc.Infrastructure.Sql.SqlQueries;
using CleanArc.SharedKernel.Extensions;
using Dapper;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
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
    private readonly ILogger<CategoryRepository> _logger;

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
    public CategoryRepository(IConfiguration configuration, IMapper mapper, ILogger<CategoryRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        _mapper = mapper;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<string> AddAsync(Category entity)
    {
        throw new NotImplementedException();
    }

    public Task<string> DeleteAsync(string selectedIds, int updatedBy)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Category>> GetAllAsync(SearchRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Category> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<string> UpdateAsync(Category entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    //    public async Task<string> AddAsync(AgeType ageType)
    //    {
    //        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, ageType))
    //        {
    //            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
    //            {
    //                connection.Open();
    //                CreateAgeTypeDTO createAgeTypeDTO = _mapper.Map<CreateAgeTypeDTO>(ageType);
    //                var result = await connection.ExecuteAsync(AgeTypeQueries.Create_AgeType, createAgeTypeDTO, commandType: CommandType.StoredProcedure);
    //                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
    //                return result.ToString();
    //            }
    //        }
    //    }

    //    public async Task<string> DeleteAsync(string selectedIds, int updatedBy)
    //    {
    //        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, new { selectedIds, updatedBy }))
    //        {
    //            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
    //            {
    //                connection.Open();
    //                var parameters = new DynamicParameters();
    //                parameters.Add("@ID", selectedIds);
    //                parameters.Add("@UpdatedBy", updatedBy);
    //                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
    //                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

    //                var result = await connection.ExecuteAsync(AgeTypeQueries.Delete_AgeType, parameters, commandType: CommandType.StoredProcedure);
    //                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
    //                return result.ToString();
    //            }
    //        }
    //    }

    //    public async Task<IReadOnlyList<AgeType>> GetAllAsync(SearchRequest searchRequest)
    //    {
    //        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
    //        {
    //            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
    //            {
    //                connection.Open();
    //                var parameters = new
    //                {
    //                    PageNumber = searchRequest.PageNumber,
    //                    PageSize = searchRequest.PageSize,
    //                    //SortingColumnName = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnName,
    //                    //SortingColumnDirection = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnDirection,
    //                    //FilterParameterName = searchRequest.FilterArray?.FirstOrDefault()?.ParameterName,
    //                    //FilterParameterValue = searchRequest.FilterArray?.FirstOrDefault()?.ParameterValue
    //                    SortingArray = DataTableHelper.ToDataTable(searchRequest.SortingArray), // Convert list to DataTable
    //                    FilterArray = DataTableHelper.ToDataTable(searchRequest.FilterArray) // Convert list to DataTable
    //                };
    //                var result = await connection.QueryAsync<AgeType>(AgeTypeQueries.usp_GetAll_AgeType, parameters, commandType: CommandType.StoredProcedure);
    //                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
    //                return result.ToList();
    //            }
    //        }
    //    }
    //    public async Task<AgeType> GetByIdAsync(long id)
    //    {
    //        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, id))
    //        {
    //            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
    //            {
    //                connection.Open();
    //                var result = await connection.QuerySingleOrDefaultAsync<AgeType>(AgeTypeQueries.usp_GetByID_AgeType, new { ID = id }, commandType: CommandType.StoredProcedure);
    //                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
    //                return result;
    //            }
    //        }
    //    }



    //    public async Task<string> UpdateAsync(AgeType entity)
    //    {
    //        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity))
    //        {
    //            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
    //            {
    //                connection.Open();
    //                UpdateAgeTypeDTO updateAgeTypeDTO = _mapper.Map<UpdateAgeTypeDTO>(entity);

    //                var result = await connection.ExecuteAsync(AgeTypeQueries.update_AgeType, updateAgeTypeDTO, commandType: CommandType.StoredProcedure);
    //                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
    //                return result.ToString();
    //            }
    //        }
    //    }
    //}
}