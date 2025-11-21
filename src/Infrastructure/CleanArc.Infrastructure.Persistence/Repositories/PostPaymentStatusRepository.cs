using Azure.Core;
using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.BusinessType;
using CleanArc.Application.Models.Language;
using CleanArc.Application.Models.PostPaymentStatus;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.URL;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.ActivityDisabilityOption;
using CleanArc.Domain.Entities.ActivityIncludedOption;
using CleanArc.Domain.Entities.ActivitySeason;
using CleanArc.Domain.Entities.Language;
using CleanArc.Domain.Entities.PostPaymentStatus;
using CleanArc.Domain.Entities.UserManagement;
using CleanArc.Infrastructure.Persistence.Helpers;
using CleanArc.Infrastructure.Persistence.Services;
using CleanArc.Infrastructure.Sql;
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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Voice;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CleanArc.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for handling operations related to menus.
/// </summary>
/// <seealso cref="CleanArc.Application.Contracts.Persistence.IMenuRepository" />
public class PostPaymentStatusRepository:IPostPaymentStatusRepository
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
    private readonly ILogger<PostPaymentStatusRepository> _logger;

    /// <summary>
    /// The HTTP context accessor for accessing HTTP context information.
    /// </summary>
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmailService _emailService;


    /// <summary>
    /// Initializes a new instance of the <see cref="MenuRepository"/> class.
    /// </summary>
    /// <param name="configuration">The configuration for accessing application settings.</param>
    /// <param name="mapper">The mapper for mapping between different object types.</param>
    /// <param name="logger">The logger for logging repository-related information.</param>
    /// <param name="httpContextAccessor">The HTTP context accessor for accessing HTTP context information.</param>
    public PostPaymentStatusRepository(IConfiguration configuration, IMapper mapper, ILogger<PostPaymentStatusRepository> logger, 
        IHttpContextAccessor httpContextAccessor, IEmailService emailService)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor; 
        _emailService = emailService;

    }
    /// <inheritdoc/>
    public async Task<ResponseEntity> AddAsync(PostPaymentStatus PostPaymentStatus)
{
    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, PostPaymentStatus))
    {
        using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
        {
            connection.Open();
                CreatePostPaymentStatusDTO postPaymentStatusDTO = _mapper.Map<CreatePostPaymentStatusDTO>(PostPaymentStatus);

                var Result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(PostPaymentStatusQueries.Create_PostPaymentStatus, postPaymentStatusDTO, commandType: CommandType.StoredProcedure);
                CreatePostPaymentStatusDTO createPostPaymentStatusDTO = _mapper.Map<CreatePostPaymentStatusDTO>(PostPaymentStatus);
                
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(Result);
                // Send email only if booking is successful and user email is available
                return Result;
        }

    }
}

    public Task<ResponseEntity> DeleteAsync(DeleteRequest deleteRequest, int? updatedBy)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, deleteRequest))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                //connection.Open();
                //var parameters = new DynamicParameters();
                //parameters.Add("@Ids", deleteRequest.SelectedIds);
                //parameters.Add("@CultureId", deleteRequest.CultureId);
                //parameters.Add("@IsDeleted", deleteRequest.isDeleted);
                //parameters.Add("@UpdatedBy", updatedBy);
                
                //var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(PostPaymentStatusQueries.Delete_OrderPayment, parameters, commandType: CommandType.StoredProcedure);
                // (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                return null;
            }
        }
    }

    public async Task<ListResponseWrapper<PostPaymentStatus>> GetAllAsync(SearchRequest searchRequest)
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

                //var parameters = new
                //{
                //    PageNumber = searchRequest.PageNumber,
                //    PageSize = searchRequest.PageSize,
                //    //SortingColumnName = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnName,
                //    //SortingColumnDirection = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnDirection,
                //    //FilterParameterName = searchRequest.FilterArray?.FirstOrDefault()?.ParameterName,
                //    //FilterParameterValue = searchRequest.FilterArray?.FirstOrDefault()?.ParameterValue
                //    SortingArray = DataTableHelper.ToDataTable(searchRequest.SortingArray), // Convert list to DataTable
                //    FilterArray = DataTableHelper.ToDataTable(searchRequest.FilterArray) // Convert list to DataTable
                //};
                var result = await connection.QueryAsync<PostPaymentStatus>(PostPaymentStatusQueries.GetAll_PostPaymentStatus, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                
                var response = new ListResponseWrapper<PostPaymentStatus> { Data = result.ToList(), TotalCount = parameters.Get<int>("@TotalCount"), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") };return response;
            }
        }
    }
    public async Task<SingleResponseWrapper<PostPaymentStatus>> GetByIdAsync(SearchRequestById searchRequestById)
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
                //var result = await connection.QuerySingleOrDefaultAsync<PostPaymentStatuss>(PostPaymentStatusQueries.GetByID_OrderPayment, parameters, commandType: CommandType.StoredProcedure);

                var result = await connection.QueryMultipleAsync(PostPaymentStatusQueries.GetByID_PostPaymentStatus, parameters, commandType: CommandType.StoredProcedure);



                var order = result.Read<PostPaymentStatus>().FirstOrDefault();
                
                if (!result.IsConsumed)
                {
                    result.Dispose();
                }
                //await connection.ExecuteAsync(
                //        ActivityQueries.GetByID_Activity,
                //        parameters,
                //        commandType: CommandType.StoredProcedure);
                int Code = parameters.Get<int>("@Code");
                string? Message = parameters.Get<string>("@Message");

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                var response = new SingleResponseWrapper<PostPaymentStatus>
                {
                    Data = order,
                    Code = Code,
                    Message = Message
                };
                return response;
            }
        }
    }



    public async Task<ResponseEntity> UpdateAsync(PostPaymentStatus entity)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdatePostPaymentStatusDTO updatePostPaymentStatusDTO = _mapper.Map<UpdatePostPaymentStatusDTO>(entity);
                //var parameters = new DynamicParameters(updatePostPaymentStatusDTO);
                var parameters = new DynamicParameters();
                parameters.Add("@id", entity.Id, DbType.Int32);
                parameters.Add("@cultureId", entity.CultureId, DbType.Int32);
                parameters.Add("@updatedBy", entity.UpdatedBy, DbType.Int32);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(PostPaymentStatusQueries.Update_PostPaymentStatus, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                return result;
            }
        }
    }
}

/// <inheritdoc/>


/// <inheritdoc/>

