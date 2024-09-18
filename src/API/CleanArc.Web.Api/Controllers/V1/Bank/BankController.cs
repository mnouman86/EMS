using Asp.Versioning;
using CleanArc.Application.Features.Bank.Command.CreateBankCommand;
using CleanArc.Application.Features.Bank.Command.DeleteBankCommand;
using CleanArc.Application.Features.Bank.Command.UpdateBankCommand;
using CleanArc.Application.Features.Bank.Queries.GetAllBank;
using CleanArc.Application.Features.Bank.Queries.GetBankById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.Bank
{
    /// <summary>
    /// BankController is responsible for handling HTTP requests related to bank operations
    /// such as creating, updating, deleting, and retrieving bank information. It extends from a 
    /// base controller which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateBank: Handles the creation of a new bank.
    /// 2. UpdateBank: Handles the updating of an existing bank.
    /// 3. DeleteBank: Handles the deletion of an existing bank.
    /// 4. GetAllBank: Retrieves all banks.
    /// 5. GetBankById: Retrieves a specific bank by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/BankController".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller's 
    /// operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateBank")]
    /// public async Task<IActionResult> CreateBank(CreateBankCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.Bank.Command.CreateBankCommand.CreateBankCommand, CleanArc.Application.Features.Bank.Command.UpdateBankCommand.UpdateBankCommand, CleanArc.Application.Features.Bank.Command.DeleteBankCommand.DeleteBankCommand, System.ResponseEntity, CleanArc.Application.Features.Bank.Queries.GetAllBank.GetAllBankQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.Bank.Queries.GetAllBank.GetAllBankQueryResult&gt;, CleanArc.Application.Features.Bank.Queries.GetBankById.GetBankByIdQuery, CleanArc.Application.Features.Bank.Queries.GetBankById.GetBankByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/BankController")]
    public class BankController : _BaseController<CreateBankCommand, UpdateBankCommand, DeleteBankCommand, ResponseEntity, GetAllBankQuery,
    List<GetAllBankQueryResult>, GetBankByIdQuery, GetBankByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BankController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public BankController(ISender sender, ILogger<_BaseController<CreateBankCommand, UpdateBankCommand, DeleteBankCommand, ResponseEntity, GetAllBankQuery,
   List<GetAllBankQueryResult>, GetBankByIdQuery, GetBankByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}


