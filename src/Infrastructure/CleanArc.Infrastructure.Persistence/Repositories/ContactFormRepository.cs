using Azure.Core;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.CoreArea;
using CleanArc.Application.Models.Advertisement;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.URL;
using CleanArc.Domain.Entities.ContactForm;
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
using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using CleanArc.Application.Common;
using CleanArc.Application.Models.ContactForm;
using CleanArc.Infrastructure.Persistence.Services;

namespace CleanArc.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for handling operations related to menus.
/// </summary>
/// <seealso cref="CleanArc.Application.Contracts.Persistence.IMenuRepository" />
public class ContactFormRepository:IContactFormRepository
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
    private readonly ILogger<ContactFormRepository> _logger;

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
    public ContactFormRepository(IConfiguration configuration, IMapper mapper, ILogger<ContactFormRepository> logger, IHttpContextAccessor httpContextAccessor
        , IEmailService emailService)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _emailService = emailService;
    }
/// <inheritdoc/>
public async Task<ResponseEntity> AddAsync(ContactForm ContactForm)
{
    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, ContactForm))
    {
        using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
        {
            connection.Open();
                CreateContactFormDTO createContactFormDTO = _mapper.Map<CreateContactFormDTO>(ContactForm);
                var parameters = new DynamicParameters(createContactFormDTO);
                
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ContactFormQueries.Create_ContactForm, parameters, commandType: CommandType.StoredProcedure);
             (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                if (result.IsSuccess)
                {
                    //        var emailBody = $@"
                    //<h3>New Contact Form Submission</h3>
                    //<p><strong>Name:</strong> {ContactForm.FullName}</p>
                    //<p><strong>Email:</strong> {ContactForm.Email}</p>
                    //<p><strong>Subject:</strong> {ContactForm.Subject}</p>
                    //<p><strong>Message:</strong><br/>{ContactForm.Message}</p>
                    //<p><em>Received on {DateTime.Now.ToString("D")}</em></p>";

                    //        await _emailService.SendEmailAsync(
                    //            ContactForm.Email,  // Replace with real support email
                    //            $"[Contact Form] {ContactForm.Subject}",
                    //            emailBody
                    //        );
                    var userAcknowledgementEmailBody = $@"
                            <h3>Thank you for contacting us, {ContactForm.FullName}!</h3>
                            <p>We have received your message and our support team will get back to you shortly.</p>

                            <h4>Your Submitted Details:</h4>
                            <p><strong>Subject:</strong> {ContactForm.Subject}</p>
                            <p><strong>Message:</strong><br/>{ContactForm.Message}</p>

                            <p><em>Submitted on {DateTime.Now:dddd, MMMM dd, yyyy}</em></p>

                            <p>Best regards,<br/>Support Team</p>";

                    await _emailService.SendEmailAsync(
                        ContactForm.Email,  // Sending TO the user who filled the form
                        $"We Received Your {ContactForm.Subject}",
                        userAcknowledgementEmailBody
                    );
                    // Send Internal Email to Support Too
                    //var adminNotificationBody = $@"
                    //    <h3>New Contact Form Submission</h3>
                    //    <p><strong>Name:</strong> {ContactForm.FullName}</p>
                    //    <p><strong>Email:</strong> {ContactForm.Email}</p>
                    //    <p><strong>Subject:</strong> {ContactForm.Subject}</p>
                    //    <p><strong>Message:</strong><br/>{ContactForm.Message}</p>
                    //    <p><em>Received on {DateTime.Now:dddd, MMMM dd, yyyy}</em></p>";

                    //await _emailService.SendEmailAsync(
                    //    "support@yourdomain.com",  // Replace with actual admin/support email
                    //    $"[Contact Form] {ContactForm.Subject}",
                    //    adminNotificationBody
                    //);
                }
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
                
                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ContactFormQueries.Delete_ContactForm, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                return result;
            }
        }
    }

    public async Task<ListResponseWrapper<ContactForm>> GetAllAsync(SearchRequest searchRequest)
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

                
                var result = await connection.QueryAsync<ContactForm>(ContactFormQueries.GetAll_ContactForm, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                
                var response = new ListResponseWrapper<ContactForm> { Data = result.ToList(), TotalCount = parameters.Get<int>("@TotalCount"), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") };return response;
            }
        }
    }
    public async Task<SingleResponseWrapper<ContactForm>> GetByIdAsync(SearchRequestById searchRequestById)
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

                var result = await connection.QuerySingleOrDefaultAsync<ContactForm>(ContactFormQueries.GetByID_ContactForm, parameters , commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                
                var response = new SingleResponseWrapper<ContactForm>
                {
                    Data = result,
                    Code = parameters.Get<int>("@Code"),
                    Message = parameters.Get<string>("@Message")
                };
                return response;
            }
        }
    }



    public async Task<ResponseEntity> UpdateAsync(ContactForm entity)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, entity))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdateContactFormDTO updateContactFormDTO = _mapper.Map<UpdateContactFormDTO>(entity);
                var parameters = new DynamicParameters(updateContactFormDTO);
                 var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(ContactFormQueries.Update_ContactForm, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                return result;
            }
        }
    }
}

/// <inheritdoc/>


/// <inheritdoc/>

