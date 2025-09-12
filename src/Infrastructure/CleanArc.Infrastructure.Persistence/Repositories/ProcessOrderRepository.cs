using Azure.Core;
using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.BusinessType;
using CleanArc.Application.Models.ProcessOrder;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.URL;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.ActivityDisabilityOption;
using CleanArc.Domain.Entities.ActivityIncludedOption;
using CleanArc.Domain.Entities.ActivitySeason;
using CleanArc.Domain.Entities.Language;
using CleanArc.Domain.Entities.ProcessOrder;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CleanArc.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for handling operations related to menus.
/// </summary>
/// <seealso cref="CleanArc.Application.Contracts.Persistence.IMenuRepository" />
public class ProcessOrderRepository:IProcessOrderRepository
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
    private readonly ILogger<ProcessOrderRepository> _logger;

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
    public ProcessOrderRepository(IConfiguration configuration, IMapper mapper, ILogger<ProcessOrderRepository> logger, 
        IHttpContextAccessor httpContextAccessor, IEmailService emailService)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor; 
        _emailService = emailService;

    }
    /// <inheritdoc/>
    public async Task<ResponseEntity> AddAsync(ProcessOrders ProcessOrder)
{
    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, ProcessOrder))
    {
        using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
        {
            connection.Open();
                var paramsForPackageId = new DynamicParameters();
                paramsForPackageId.Add("@PackageTypeEnumID", ProcessOrder.PackageTypeEnumID);
                paramsForPackageId.Add("@CultureId", ProcessOrder.CultureId);
                paramsForPackageId.Add("@CreatedBy", ProcessOrder.CreatedBy);
                var Result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ProcessOrderQueries.Create_PackageDetail, paramsForPackageId, commandType: CommandType.StoredProcedure);
                CreateProcessOrderDTO createProcessOrderDTO = _mapper.Map<CreateProcessOrderDTO>(ProcessOrder);
                string OrderNumber = "-1";
                var parameters = new DynamicParameters(createProcessOrderDTO);
                if (ProcessOrder.Stay!=null)
                {
                    createProcessOrderDTO.Amount = ProcessOrder.Stay.Amount;
                    createProcessOrderDTO.DiscountAmount = ProcessOrder.Stay.DiscountAmount;
                    createProcessOrderDTO.GenericTitleId = ProcessOrder.Stay.GenericTitleId;
                    createProcessOrderDTO.ServiceTypeEnumId = ProcessOrder.Stay.ServiceTypeEnumId;
                    createProcessOrderDTO.PackageDetailID = Convert.ToInt32(Result.RecordID);
                    createProcessOrderDTO.Tax = ProcessOrder.Stay.Tax;
                    //createProcessOrderDTO.Title = ProcessOrder.Stay.Title;
                    createProcessOrderDTO.SubTitleID = ProcessOrder.Stay.SubTitleID;

                    var resultStay = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ProcessOrderQueries.Create_OrderPayment, parameters, commandType: CommandType.StoredProcedure);
                    OrderNumber = resultStay.RecordID;
                }


                if (ProcessOrder.CarRental != null)
                {
                    createProcessOrderDTO.Amount = ProcessOrder.CarRental.Amount;
                    createProcessOrderDTO.DiscountAmount = ProcessOrder.CarRental.DiscountAmount;
                    createProcessOrderDTO.GenericTitleId = ProcessOrder.CarRental.GenericTitleId;
                    createProcessOrderDTO.ServiceTypeEnumId = ProcessOrder.CarRental.ServiceTypeEnumId;
                    createProcessOrderDTO.PackageDetailID = Convert.ToInt32(Result.RecordID);
                    createProcessOrderDTO.Tax = ProcessOrder.CarRental.Tax;
                    //createProcessOrderDTO.Title = ProcessOrder.CarRental.Title;
                    createProcessOrderDTO.SubTitleID = ProcessOrder.CarRental.SubTitleID;

                    var resultCar = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ProcessOrderQueries.Create_OrderPayment, parameters, commandType: CommandType.StoredProcedure);
                    OrderNumber = resultCar.RecordID;

                }


                if (ProcessOrder.Flight != null)
                {
                    createProcessOrderDTO.Amount = ProcessOrder.Flight.Amount;
                    createProcessOrderDTO.DiscountAmount = ProcessOrder.Flight.DiscountAmount;
                    createProcessOrderDTO.GenericTitleId = ProcessOrder.Flight.GenericTitleId;
                    createProcessOrderDTO.ServiceTypeEnumId = ProcessOrder.Flight.ServiceTypeEnumId;
                    createProcessOrderDTO.PackageDetailID = Convert.ToInt32(Result.RecordID);
                    createProcessOrderDTO.Tax = ProcessOrder.Flight.Tax;
                    //createProcessOrderDTO.Title = ProcessOrder.Flight.Title;
                    createProcessOrderDTO.SubTitleID = ProcessOrder.Flight.SubTitleID;

                    var resultFlights = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ProcessOrderQueries.Create_OrderPayment, parameters, commandType: CommandType.StoredProcedure);
                    OrderNumber = resultFlights.RecordID;

                }


                if (ProcessOrder.Activities != null)
                {
                    createProcessOrderDTO.Amount = ProcessOrder.Activities.Amount;
                    createProcessOrderDTO.DiscountAmount = ProcessOrder.Activities.DiscountAmount;
                    createProcessOrderDTO.GenericTitleId = ProcessOrder.Activities.GenericTitleId;
                    createProcessOrderDTO.ServiceTypeEnumId = ProcessOrder.Activities.ServiceTypeEnumId;
                    createProcessOrderDTO.PackageDetailID = Convert.ToInt32(Result.RecordID);
                    createProcessOrderDTO.Tax = ProcessOrder.Activities.Tax;
                    //createProcessOrderDTO.Title = ProcessOrder.Activities.Title;
                    createProcessOrderDTO.SubTitleID = ProcessOrder.Activities.SubTitleID;

                    var resultActivities = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ProcessOrderQueries.Create_OrderPayment, parameters, commandType: CommandType.StoredProcedure);
                    OrderNumber = resultActivities.RecordID;

                }



                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(Result);
                // Send email only if booking is successful and user email is available
                if (Result.IsSuccess && Result.RecordID !="-1" && !string.IsNullOrWhiteSpace(ProcessOrder.Email))
                {
                    var subtitleHtml = string.IsNullOrWhiteSpace(createProcessOrderDTO.SubTitleID.ToString())
                                    ? string.Empty
                                    : $"<li><strong> {createProcessOrderDTO.SubTitleID}</strong></li>";
                    var cityHtml = string.IsNullOrWhiteSpace(ProcessOrder.City)
                                    ? string.Empty
                                    : $"<li><strong>City:</strong> {ProcessOrder.City}</li>";
                    var emailBody = $@"
                                    <h3>Booking Confirmation</h3>
                                    <p>Dear {ProcessOrder.FirstName} {ProcessOrder.LastName},</p>
                                    <p>Thank you for your booking. Your order has been confirmed.</p>
                                    <h4>Service(s) Info:</h4>
                                    <ul>
                                        <li><strong> {createProcessOrderDTO.GenericTitleId}</strong></li>
                                        {subtitleHtml}
                                        {cityHtml}
                                    </ul>
                                    <h4>Booking Details:</h4>
                                    <ul>
                                        <li><strong>Booking ID:</strong> {Result.RecordID}</li>
                                        <li><strong>Order Number:</strong> {ProcessOrder.OrderNumber ?? "Auto-generated"}</li>
                                        <li><strong>From Date:</strong> {ProcessOrder.FromDate?.ToString("yyyy-MM-dd")}</li>
                                        <li><strong>To Date:</strong> {ProcessOrder.ToDate?.ToString("yyyy-MM-dd")}</li>
                                        <li><strong>Amount:</strong> {createProcessOrderDTO.Amount?.ToString("C")}</li>
                                        <li><strong>Status:</strong> Confirmed</li>
                                    </ul>
                                    <p>We look forward to hosting you!</p>
                                    <p>Best regards,<br/>The Booking Team</p>
                                ";

                    await _emailService.SendEmailAsync(
                        ProcessOrder.Email,
                        "Your Booking is Confirmed",
                        emailBody
                    );
                }
                Result.RecordID = OrderNumber;
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
                
                //var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ProcessOrderQueries.Delete_OrderPayment, parameters, commandType: CommandType.StoredProcedure);
                // (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                return null;
            }
        }
    }

    public async Task<ListResponseWrapper<ProcessOrders>> GetAllAsync(SearchRequest searchRequest)
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
                //parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object); // Ensure proper type
                //parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object); // Ensure proper type
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
                var result = await connection.QueryAsync<ProcessOrders>(ProcessOrderQueries.GetAll_OrderPayment, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                
                var response = new ListResponseWrapper<ProcessOrders> { Data = result.ToList(), TotalCount = parameters.Get<int>("@TotalCount"), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") };return response;
            }
        }
    }
    public async Task<SingleResponseWrapper<ProcessOrders>> GetByIdAsync(SearchRequestById searchRequestById)
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
                //var result = await connection.QuerySingleOrDefaultAsync<ProcessOrders>(ProcessOrderQueries.GetByID_OrderPayment, parameters, commandType: CommandType.StoredProcedure);

                var result = await connection.QueryMultipleAsync(ProcessOrderQueries.GetByID_OrderPayment, parameters, commandType: CommandType.StoredProcedure);



                var order = result.Read<ProcessOrders>().FirstOrDefault();
                if (order != null)
                {
                    var stay = result.Read<OrderCategory>();
                    if (stay != null && stay?.Count()>0)
                    order.Stay =(OrderCategory)stay;

                    var carRental = result.Read<OrderCategory>();
                    if (carRental != null && carRental?.Count() > 0)
                        order.CarRental = (OrderCategory)carRental;

                    var flight = result.Read<OrderCategory>();
                    if (flight != null && flight?.Count() > 0)
                        order.Flight = (OrderCategory)flight;

                    var activity = result.Read<OrderCategory>();
                    if (activity != null && activity?.Count() > 0)
                        order.Activities = (OrderCategory)activity;
                }
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
                var response = new SingleResponseWrapper<ProcessOrders>
                {
                    Data = order,
                    Code = Code,
                    Message = Message
                };
                return response;
            }
        }
    }



    public async Task<ResponseEntity> UpdateAsync(ProcessOrders entity)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdateProcessOrderDTO updateProcessOrderDTO = _mapper.Map<UpdateProcessOrderDTO>(entity);
                //var parameters = new DynamicParameters(updateProcessOrderDTO);
                var parameters = new DynamicParameters();
                parameters.Add("@id", entity.Id, DbType.Int32);
                parameters.Add("@orderStatusEnumID ", entity.OrderStatusEnumId, DbType.Int32);
                parameters.Add("@cultureId", entity.CultureId, DbType.Int32);
                parameters.Add("@updatedBy", entity.UpdatedBy, DbType.Int32);
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ProcessOrderQueries.Update_OrderPayment, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                return result;
            }
        }
    }
}

/// <inheritdoc/>


/// <inheritdoc/>

