using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.AgeType;
using CleanArc.Application.Models.Hotel;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.AgeType;
using CleanArc.Domain.Entities.Hotel;
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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class HotelRepository : IHotelRepository
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
    private readonly ILogger<HotelRepository> _logger;

    /// <summary>
    /// The HTTP context accessor for accessing HTTP context information.
    /// </summary>
    private readonly IHttpContextAccessor _httpContextAccessor;
    public HotelRepository(IConfiguration configuration, IMapper mapper, ILogger<HotelRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> AddAsync(Hotel hotel)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, hotel))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                CreateHotelDTO createHotelDTO = _mapper.Map<CreateHotelDTO>(hotel);
                var result = await connection.ExecuteAsync(HotelQueries.Create_Hotel, createHotelDTO, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result.ToString();
            }
        }
    }


    public async Task<string> DeleteAsync(string selectedIds, int updatedBy)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, new { selectedIds, updatedBy }))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@ID", selectedIds);
                parameters.Add("@UpdatedBy", updatedBy);
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = await connection.ExecuteAsync(HotelQueries.Delete_Hotel, parameters, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result.ToString();
            }
        }
    }


    public async Task<IReadOnlyList<Hotel>> GetAllAsync(SearchRequest searchRequest)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                var parameters = new
                {
                    PageNumber = searchRequest.PageNumber,
                    PageSize = searchRequest.PageSize,
                    //SortingColumnName = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnName,
                    //SortingColumnDirection = searchRequest.SortingArray?.FirstOrDefault()?.SortingColumnDirection,
                    //FilterParameterName = searchRequest.FilterArray?.FirstOrDefault()?.ParameterName,
                    //FilterParameterValue = searchRequest.FilterArray?.FirstOrDefault()?.ParameterValue
                    SortingArray = DataTableHelper.ToDataTable(searchRequest.SortingArray), // Convert list to DataTable
                    FilterArray = DataTableHelper.ToDataTable(searchRequest.FilterArray) // Convert list to DataTable
                };
                var result = await connection.QueryAsync<Hotel>(HotelQueries.usp_GetALL_Hotel, parameters, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result.ToList();
            }
        }
    }


    public async Task<Hotel> GetByIdAsync(long id)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, id))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Hotel>(HotelQueries.usp_GetByID_Hotel, new { ID = id }, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }


    public async Task<string> UpdateAsync(Hotel hotel)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, hotel))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdateHotelDTO updateHotelDTO = _mapper.Map<UpdateHotelDTO>(hotel);

                var result = await connection.ExecuteAsync(HotelQueries.update_Hotel, updateHotelDTO, commandType: CommandType.StoredProcedure);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result.ToString();
            }
        }
    }

}
