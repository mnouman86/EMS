using Asp.Versioning;
using CleanArc.Application.Features.BusinessBankAccount.Command.BusinessBankAccountCommand;
using CleanArc.Application.Features.BusinessBankAccount.Command.DeleteBusinessBankAccountCommand;
using CleanArc.Application.Features.BusinessBankAccount.Command.UpdateBusinessBankAccountCommand;
using CleanArc.Application.Features.BusinessBankAccount.Query.GetAllBusinessBankAccount;
using CleanArc.Application.Features.BusinessBankAccount.Query.GetBusinessBankAccountById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.BusinessBankAccount
{
    /// <summary>
    /// BusinessBankAccountController is responsible for handling HTTP requests related to business bank account operations
    /// such as creating, updating, deleting, and retrieving business bank account information. It extends from a 
    /// base controller which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateBusinessBankAccount: Handles the creation of a new business bank account.
    /// 2. UpdateBusinessBankAccount: Handles the updating of an existing business bank account.
    /// 3. DeleteBusinessBankAccount: Handles the deletion of an existing business bank account.
    /// 4. GetAllBusinessBankAccount: Retrieves all business bank accounts.
    /// 5. GetBusinessBankAccountById: Retrieves a specific business bank account by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/BusinessBankAccount".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateBusinessBankAccount")]
    /// public async Task<IActionResult> CreateBusinessBankAccount(CreateBusinessBankAccountCommand model)
    /// {
    ///     model.UserId = base.UserId;
    ///     var command = await _sender.Send(model);
    ///     return base.OperationResult(command);
    /// }
    /// 
    /// This ensures that the UserId is set from the base controller before sending the command and that the operation
    /// result is properly formatted for the response.
    /// 
    /// The controller is part of the CleanArc architecture, ensuring a clean separation of concerns and adherence to 
    /// SOLID principles.
    /// </summary>
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.BusinessBankAccount.Command.BusinessBankAccountCommand.CreateBusinessBankAccountCommand, CleanArc.Application.Features.BusinessBankAccount.Command.UpdateBusinessBankAccountCommand.UpdateBusinessBankAccountCommand, CleanArc.Application.Features.BusinessBankAccount.Command.DeleteBusinessBankAccountCommand.DeleteBusinessBankAccountCommand, System.ResponseEntity, CleanArc.Application.Features.BusinessBankAccount.Query.GetAllBusinessBankAccount.GetAllBusinessBankAccountQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.BusinessBankAccount.Query.GetAllBusinessBankAccount.GetAllBusinessBankAccountQueryResult&gt;, CleanArc.Application.Features.BusinessBankAccount.Query.GetBusinessBankAccountById.GetBusinessBankAccountByIdQuery, CleanArc.Application.Features.BusinessBankAccount.Query.GetBusinessBankAccountById.GetBusinessBankAccountByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/BusinessBankAccount")]
    public class BusinessBankAccountController : _BaseController<CreateBusinessBankAccountCommand, UpdateBusinessBankAccountCommand, DeleteBusinessBankAccountCommand, ResponseEntity, GetAllBusinessBankAccountQuery,
    List<GetAllBusinessBankAccountQueryResult>, GetBusinessBankAccountByIdQuery, GetBusinessBankAccountByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessBankAccountController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public BusinessBankAccountController(ISender sender, ILogger<_BaseController<CreateBusinessBankAccountCommand, UpdateBusinessBankAccountCommand, DeleteBusinessBankAccountCommand, ResponseEntity, GetAllBusinessBankAccountQuery,
   List<GetAllBusinessBankAccountQueryResult>, GetBusinessBankAccountByIdQuery, GetBusinessBankAccountByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}

