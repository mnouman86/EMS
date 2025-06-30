using Asp.Versioning;
using CleanArc.Application.Features.Currency.Commands.CreateCurrencyCommand;
using CleanArc.Application.Features.Currency.Commands.DeleteCurrencyCommand;
using CleanArc.Application.Features.Currency.Commands.UpdateCurrencyCommand;
using CleanArc.Application.Features.Currency.Queries.GetCurrencyById;
using CleanArc.Application.Features.Currency.Queries.GetAllCurrency;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.Currency
{
    /// <summary>
    /// CurrencyController is responsible for handling HTTP requests related to Currency operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateCurrency: Handles the creation of a new age type.
    /// 2. UpdateCurrency: Handles the updating of an existing age type.
    /// 3. DeleteCurrency: Handles the deletion of an existing age type.
    /// 4. GetAllCurrency: Retrieves all age types.
    /// 5. GetCurrencyById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/Currency".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateCurrency")]
    /// public async Task<IActionResult> CreateCurrency(CreateCurrencyCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.Currency.Commands.CreateCurrencyCommand.CreateCurrencyCommand, CleanArc.Application.Features.Currency.Commands.UpdateCurrencyCommand.UpdateCurrencyCommand, CleanArc.Application.Features.Currency.Commands.DeleteCurrencyCommand.DeleteCurrencyCommand, System.ResponseEntity, CleanArc.Application.Features.Currency.Queries.GetAllCurrency.GetAllCurrencyQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.Currency.Queries.GetAllCurrency.GetAllCurrencyQueryResult&gt;, CleanArc.Application.Features.Currency.Queries.GetCurrencyById.GetCurrencyByIdQuery, CleanArc.Application.Features.Currency.Queries.GetCurrencyById.GetCurrencyByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/Currency")]
    //[Authorize]
    public class CurrencyController : _BaseController<CreateCurrencyCommand, UpdateCurrencyCommand, DeleteCurrencyCommand, ResponseEntity, GetAllCurrencyQuery,
    List<GetAllCurrencyQueryResult>, GetCurrencyByIdQuery, GetCurrencyByIdQueryResult>
    {
        private readonly ISender _sender;

        //public CurrencyController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateCurrency")]
        //public async Task<IActionResult> CreateCurrency(CreateCurrencyCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateCurrency")]
        //public async Task<IActionResult> UpdateCurrency(UpdateCurrencyCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteCurrency")]
        //public async Task<IActionResult> DeleteCurrency(DeleteCurrencyCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllCurrency")]
        //public async Task<IActionResult> GetAllCurrency( )
        //{
        //    var queryResult = await _sender.Send(new GetAllCurrencyQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public CurrencyController(ISender sender, ILogger<_BaseController<CreateCurrencyCommand, UpdateCurrencyCommand, DeleteCurrencyCommand, ResponseEntity, GetAllCurrencyQuery,
   List<GetAllCurrencyQueryResult>, GetCurrencyByIdQuery, GetCurrencyByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {
            _sender = sender;
        }
        [HttpPost("GetCurrencyRates")]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> GetCurrencyRates(GetCurrencyRatesQuery query)
        {
            var result = await _sender.Send(query);

            return base.OperationResult(result);
        }
    }
}
