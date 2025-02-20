using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.BusinessBankAccount;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.BusinessBankAccount;
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
using CleanArc.Domain.Entities.ActivityImageMapping;

namespace CleanArc.Infrastructure.Persistence.Repositories
{

    public class BusinessBankAccountRepository : IBusinessBankAccountRepository
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
        private readonly ILogger<BusinessBankAccountRepository> _logger;

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
        public BusinessBankAccountRepository(IConfiguration configuration, IMapper mapper, ILogger<BusinessBankAccountRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this._mapper = mapper;
            this._logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <inheritdoc/>

        public async Task<ResponseEntity> AddAsync(BusinessBankAccount businessBankAccount)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, businessBankAccount))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();
                    CreateBusinessBankAccountDTO createBusinessBankAccount = _mapper.Map<CreateBusinessBankAccountDTO>(businessBankAccount);
                    var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(BusinessBankAccountQueries.Create_BusinessBankAccount, createBusinessBankAccount, commandType: CommandType.StoredProcedure);
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
                    parameters.Add("@UpdatedBy", updatedBy);
                    parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                    var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(BusinessBankAccountQueries.Delete_BusinessBankAccount, parameters, commandType: CommandType.StoredProcedure);
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); if (result != null) { result.Code = parameters.Get<int>("@Code"); result.Message = parameters.Get<string>("@Message"); }
                    return result;
                }
            }
        }

        public async Task<ListResponseWrapper<BusinessBankAccount>> GetAllAsync(SearchRequest searchRequest)
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
					parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object); // Ensure proper type
					parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object); // Ensure proper type
					parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
					parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
					var result = await connection.QueryAsync<BusinessBankAccount>(BusinessBankAccountQueries.usp_GetAll_BusinessBankAccount, parameters, commandType: CommandType.StoredProcedure);
                   

					(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
					var response = new ListResponseWrapper<BusinessBankAccount> { Data = result.ToList(), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") }; return response;
				}
            }
        }
        public async Task<SingleResponseWrapper<BusinessBankAccount>> GetByIdAsync(long id)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, id))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();
					var parameters = new DynamicParameters();
					parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
					parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
					parameters.Add("@CultureId", 1, DbType.Int32);
					parameters.Add("@ID", id, DbType.Int32);
					var result = await connection.QuerySingleOrDefaultAsync<BusinessBankAccount>(BusinessBankAccountQueries.usp_GetByID_BusinessBankAccount, parameters, commandType: CommandType.StoredProcedure);
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    var response = new SingleResponseWrapper<BusinessBankAccount>
                    {
                        Data = result,
                       Code = parameters.Get<int>("@Code"),
                        Message = parameters.Get<string>("@Message")
                    };
                    return response;
                }
            }
        }



        public async Task<ResponseEntity> UpdateAsync(BusinessBankAccount businessBankAccount)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, businessBankAccount))
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
                {
                    connection.Open();
                    UpdateBusinessBankAccountDTO updateBusinessBankAccountDTO = _mapper.Map<UpdateBusinessBankAccountDTO>(businessBankAccount);

                    var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(BusinessBankAccountQueries.Update_BusinessBankAccount, updateBusinessBankAccountDTO, commandType: CommandType.StoredProcedure);
                     (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                    return result;
                }
            }
        }
    }
}
