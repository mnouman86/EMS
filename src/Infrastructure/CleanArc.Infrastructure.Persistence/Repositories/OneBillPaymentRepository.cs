using Azure.Core;
using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.ActivityNature;
using CleanArc.Application.Models.BusinessProfile;
using CleanArc.Application.Models.OneBillPayment;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.URL;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.ActivityNature;
using CleanArc.Domain.Entities.OneBillPayment;
using CleanArc.Domain.Entities.UserManagement;
using CleanArc.Infrastructure.Persistence.Helpers;
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
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CleanArc.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for handling operations related to menus.
/// </summary>
/// <seealso cref="CleanArc.Application.Contracts.Persistence.IMenuRepository" />
public class OneBillPaymentRepository : IOneBillPaymentRepository
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
    private readonly ILogger<OneBillPaymentRepository> _logger;

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
    public OneBillPaymentRepository(IConfiguration configuration, IMapper mapper, ILogger<OneBillPaymentRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<SingleResponseWrapper<OneBillInquiryResponseDto>> GetOneBillPaymentAsync(string utilityConsumerNumber, string utilityCompanyId)
    {
        using (
            
            var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, utilityConsumerNumber))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                //parameters.Add("@CultureId", searchRequestById.CultureId, DbType.Int32);
                //parameters.Add("@ID", searchRequestById.Id, DbType.Int32);

                var sql = @"
            SELECT 
                ConsumerName AS ConsumerDetail,
                LoanStatus,
                DueDate,
                AmountDueDate,
                AmountAfterDueDate,
                InstallmentNo,
                RemainingInstallments,
                RemainingInstallmentAmount,
                NextPaymentDueDate
            FROM LoanConsumers
            WHERE UtilityConsumerNumber = @ConsumerNumber
              AND UtilityCompanyId = @CompanyId";

                //var result = await connection.QueryFirstOrDefaultAsync<OneBillPaymentResponseDto>(
                //    sql, new { ConsumerNumber = utilityConsumerNumber, CompanyId = utilityCompanyId });

                OneBillInquiryResponseDto result;
                //return result;
                await Task.Delay(10);
                if (utilityCompanyId == "KESC0001" && utilityConsumerNumber == "112233445566")
                {
                    result = new OneBillInquiryResponseDto
                    {
                        ResponseCode = "00",
                        ConsumerDetail = "MUHAMMAD AHMER",
                        LoanStatus = "U",
                        DueDate = "20081010",
                        AmountDueDate = "000000186900",
                        AmountAfterDueDate = "000000202500",
                        InstallmentNo = "09",
                        RemainingInstallments = "06",
                        RemainingInstallmentAmount = "000008100000",
                        NextPaymentDueDate = "20081010",
                        Reserved = ""
                    };
                }
                else
                    result = null;
                    //var result = await connection.QueryAsync<OneBillPaymentResponseDto>(OneBillPaymentQueries.Get_OneBillPayment, parameters, commandType: CommandType.StoredProcedure);
                    //var result = await connection.QuerySingleOrDefaultAsync<ActivityNature>(ActivityNatureQueries.GetByID_Natures, parameters, commandType: CommandType.StoredProcedure);
                    (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                var response = new SingleResponseWrapper<OneBillInquiryResponseDto>
                {
                    Data = result,
                    Code = 200, //parameters.Get<int>("@Code"),
                    Message =""// parameters.Get<string>("@Message")
                };
                return response;
            }
        }
    }

    public async Task<string> RecordPaymentAsync(OneBillPaymentRequestDto paymentRequest, CancellationToken cancellationToken)
    {
        // Mock DB save delay
        await Task.Delay(100, cancellationToken);

        // Normally, you’d insert into DB here and return transaction log ID
        //var transactionLogId = new Random().Next(100000, 999999).ToString();
        return paymentRequest.Stan;
    }
}