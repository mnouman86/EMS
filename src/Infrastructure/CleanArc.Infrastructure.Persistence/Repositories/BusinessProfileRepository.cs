using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.ActivityType;
using CleanArc.Application.Models.BusinessProfile;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.BusinessProfile;
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

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class BusinessProfileRepository : IBusinessProfileRepository
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
    private readonly ILogger<BusinessProfileRepository> _logger;

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
    public BusinessProfileRepository(IConfiguration configuration, IMapper mapper, ILogger<BusinessProfileRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    /// <inheritdoc/>
    public async Task<ResponseEntity> AddAsync(BusinessProfile BusinessProfile)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, BusinessProfile))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                CreateBusinessProfileDTO createBusinessProfileDTO = _mapper.Map<CreateBusinessProfileDTO>(BusinessProfile);
                var parameters = new DynamicParameters(createBusinessProfileDTO);
                //parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(BusinessProfileQueries.Create_BusinessProfile, createBusinessProfileDTO, commandType: CommandType.StoredProcedure);
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
                parameters.Add("@CultureId", deleteRequest.CultureId);
                parameters.Add("@IsDeleted", deleteRequest.isDeleted);
                parameters.Add("@UpdatedBy", updatedBy);
                //parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(BusinessProfileQueries.Delete_BusinessProfile, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                return result;
            }
        }
    }

    
    public async Task<ListResponseWrapper<BusinessProfile>> GetAllAsync(SearchRequest searchRequest)

    {
     using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
     {
         using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
         {
             connection.Open();
             var parameters = new DynamicParameters();
              parameters.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
             parameters.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
             parameters.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
                //parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object); // Ensure proper type
                //parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object); // Ensure proper type				
                parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
             parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

             //var parameters = new
             //{
             //    PageNumber = searchRequest.PageNumber,
             //    PageSize = searchRequest.PageSize,
             //    cultureId = searchRequest.CultureId,
             //    //SortingColumnName = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnName,
             //    //SortingColumnDirection = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnDirection,
             //    //FilterParameterName = searchRequest.FilterArray?.FirstOrDefault()?.ParameterName,
             //    //FilterParameterValue = searchRequest.FilterArray?.FirstOrDefault()?.ParameterValue
             //    SortingArray = DataTableHelper.ToDataTable(searchRequest.SortingArray), // Convert list to DataTable
             //    FilterArray = DataTableHelper.ToDataTable(searchRequest.FilterArray), // Convert list to DataTable
             //    Code = ("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output),
             //    Message = ("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output)

             //};
             var result = await connection.QueryAsync<BusinessProfile>(BusinessProfileQueries.GetALl_BusinessProfile, parameters, commandType: CommandType.StoredProcedure);
     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
             var response = new ListResponseWrapper<BusinessProfile> { Data = result.ToList(), TotalCount = parameters.Get<int>("@TotalCount"), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") };return response;
         }
     }
 }
    public async Task<SingleResponseWrapper<BusinessProfile>> GetByIdAsync(SearchRequestById searchRequestById)
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


                var result = await connection.QuerySingleOrDefaultAsync<BusinessProfile>(BusinessProfileQueries.GetByID_BusinessProfile, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                var response = new SingleResponseWrapper<BusinessProfile>
                {
                    Data = result,
                    //Code = parameters.Get<int>("@Code"),
                    //Message = parameters.Get<string>("@Message")
                };
                return response;
            }
        }
    }



    public async Task<ResponseEntity> UpdateAsync(BusinessProfile BusinessProfile)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, BusinessProfile))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdateBusinessProfileDTO updateBusinessProfileDTO = _mapper.Map<UpdateBusinessProfileDTO>(BusinessProfile);
                var parameters = new DynamicParameters(updateBusinessProfileDTO);
                //parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(BusinessProfileQueries.Update_BusinessProfile, updateBusinessProfileDTO, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                //
                return result;
            }
        }
    }
}
/// <inheritdoc/>


/// <inheritdoc/>


