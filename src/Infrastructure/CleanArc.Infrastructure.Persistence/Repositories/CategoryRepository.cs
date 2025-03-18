using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.AgeType;
using CleanArc.Application.Models.Category;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.AgeType;
using CleanArc.Domain.Entities.Category;
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Common;
using CleanArc.Domain.Entities.AdvertisementPage;

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

    public async Task<ResponseEntity> AddAsync(Category category)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, category))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                CreateCategoryDTO createCategoryDTO = _mapper.Map<CreateCategoryDTO>(category);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(CategoryQueries.Create_Category, createCategoryDTO, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }

    public async Task<ResponseEntity> DeleteAsync(string selectedIds, int updatedBy, int? CultureId)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, new { selectedIds, updatedBy }))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@ID", selectedIds);
                parameters.Add("@CultureId", CultureId);
                parameters.Add("@UpdatedBy", updatedBy);
                
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(CategoryQueries.Delete_Category, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
                return result;
            }
        }
    }

    public async Task<ListResponseWrapper<Category>> GetAllAsync(SearchRequest searchRequest)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
				var parameters = new DynamicParameters();

				parameters.Add("@PageNumber", searchRequest.PageNumber);
				parameters.Add("@PageSize", searchRequest.PageSize);
				parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), dbType: DbType.Object);
				parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), dbType: DbType.Object);
				parameters.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
				parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
				parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
				var result = await connection.QueryAsync<Category>(CategoryQueries.GetAll_Category, parameters, commandType: CommandType.StoredProcedure);
          
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
				var response = new ListResponseWrapper<Category> { Data = result.ToList(), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") }; return response;

			}
		}
    }
    public async Task<SingleResponseWrapper<Category>> GetByIdAsync(SearchRequestById searchRequestById)
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
				var result = await connection.QuerySingleOrDefaultAsync<Category>(CategoryQueries.GetByID_Category, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                var response = new SingleResponseWrapper<Category>
                {
                    Data = result,
                    Code = parameters.Get<int>("@Code"),
                    Message = parameters.Get<string>("@Message")
                };
                return response;
            }
        }
    }

    public async Task<ResponseEntity> UpdateAsync(Category category)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, category))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdateCategoryDTO updateCategoryDTO = _mapper.Map<UpdateCategoryDTO>(category);

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(CategoryQueries.Update_Category, updateCategoryDTO, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }
}

/// <inheritdoc/>










