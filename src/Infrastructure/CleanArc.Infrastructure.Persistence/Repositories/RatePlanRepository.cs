using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.AgeType;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.RatePlan;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.AgeType;
using CleanArc.Domain.Entities.City;
using CleanArc.Domain.Entities.RatePlan;
using CleanArc.Infrastructure.Persistence.Helpers;
using CleanArc.Infrastructure.Sql.SqlQueries;
using CleanArc.SharedKernel.Extensions;
using Dapper;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class RatePlanRepository : IRatePlanRepository 
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
    private readonly ILogger<RatePlanRepository> _logger;

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
    public RatePlanRepository(IConfiguration configuration, IMapper mapper, ILogger<RatePlanRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResponseEntity> AddAsync(RatePlanRequestDto RatePlan)
    {
        ResponseEntity result = new ResponseEntity();
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, RatePlan))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                CreateRatePlanDTO createRatePlanDTO = _mapper.Map<CreateRatePlanDTO>(RatePlan);
                using var transaction = connection.BeginTransaction();

                try
                {
                    int totalProcessed = 0;
                    int inserted = 0;
                    int updated = 0;
                    
                    foreach (var roomRatePlan in RatePlan.RoomRatePlans)
                    {
                        foreach (var dailyRate in roomRatePlan.DailyRates)
                        {
                            var existingRate = await connection.QueryFirstOrDefaultAsync(
                                @"SELECT DailyRatePlanId 
                          FROM DailyRatePlans 
                          WHERE RatePlanTypeId = @RatePlanTypeId AND RateDate = @RateDate",
                                new { roomRatePlan.RatePlanTypeId, dailyRate.RateDate },
                                transaction
                            );

                            if (existingRate.HasValue)
                            {
                                // Update existing record
                                await connection.ExecuteAsync(
                                    @"UPDATE DailyRatePlans 
                              SET AvailableRooms = @AvailableRooms,
                                  Rate = @Rate,
                                  StopSell = @StopSell,
                                  MinStay = @MinStay,
                                  MaxStay = @MaxStay,
                                  UpdatedAt = GETDATE()
                              WHERE DailyRatePlanId = @DailyRatePlanId",
                                    new
                                    {
                                        DailyRatePlanId = existingRate.Value,
                                        dailyRate.AvailableRooms,
                                        dailyRate.Rate,
                                        dailyRate.StopSell,
                                        dailyRate.MinStay,
                                        dailyRate.MaxStay
                                    },
                                    transaction
                                );
                                updated++;
                            }
                            else
                            {
                                // Insert new record
                                await connection.ExecuteAsync(
                                    @"INSERT INTO DailyRatePlans 
                              (RatePlanTypeId, RateDate, AvailableRooms, Rate, StopSell, MinStay, MaxStay, CreatedAt, UpdatedAt)
                              VALUES 
                              (@RatePlanTypeId, @RateDate, @AvailableRooms, @Rate, @StopSell, @MinStay, @MaxStay, GETDATE(), GETDATE())",
                                    new
                                    {
                                        roomRatePlan.RatePlanTypeId,
                                        dailyRate.RateDate,
                                        dailyRate.AvailableRooms,
                                        dailyRate.Rate,
                                        dailyRate.StopSell,
                                        dailyRate.MinStay,
                                        dailyRate.MaxStay
                                    },
                                    transaction
                                );
                                inserted++;
                            }
                            totalProcessed++;
                        }
                    }

                    transaction.Commit();
                    result.IsSuccess = true;
                    result.Message = "Daily rate plans processed successfully.";
                    result.RecordID = $"Total Processed: {totalProcessed}, Inserted: {inserted}, Updated: {updated}";
                    result.Code=200;
                    (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    //return (totalProcessed, inserted, updated);
                }
                catch
                {
                    transaction.Rollback();
                    result.IsSuccess = false;
                    result.Message = "An error occurred while processing daily rate plans.";
                    result.RecordID = null;
                    result.Code = 200;
                    throw;
                }
                //var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(RatePlanQueries.Create_RatePlan, createRatePlanDTO, commandType: CommandType.StoredProcedure);
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
                parameters.Add("@UpdatedBy", updatedBy);
				parameters.Add("@IsDeleted", deleteRequest.isDeleted);
				parameters.Add("@CultureId", deleteRequest.CultureId);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(RatePlanQueries.Delete_RatePlan, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                //
                return result;
            }
        }
    }

    public async Task<ListResponseWrapper<RatePlanRequestDto>> GetAllAsync(SearchRequest searchRequest)
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
				var result = await connection.QueryAsync<RatePlanRequestDto>(RatePlanQueries.GetALL_RatePlan, parameters, commandType: CommandType.StoredProcedure);
      

				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
				var response = new ListResponseWrapper<RatePlanRequestDto> { Data = result.ToList(), TotalCount = parameters.Get<int>("@TotalCount"), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") }; return response;

			}
		}
    }
    public async Task<SingleResponseWrapper<RatePlanRequestDto>> GetByIdAsync(SearchRequestById searchRequestById)
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
				var result = await connection.QuerySingleOrDefaultAsync<RatePlanRequestDto>(RatePlanQueries.GetByID_RatePlan, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                var response = new SingleResponseWrapper<RatePlanRequestDto>
                {
                    Data = result,
                    Code = parameters.Get<int>("@Code"),
                    Message = parameters.Get<string>("@Message")
                };
                return response;
            }
        }
    }



    public async Task<ResponseEntity> UpdateAsync(RatePlanRequestDto RatePlan)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, RatePlan))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdateRatePlanDTO updateRatePlanDTO = _mapper.Map<UpdateRatePlanDTO>(RatePlan);

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(RatePlanQueries.Update_RatePlan, updateRatePlanDTO, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }
}

