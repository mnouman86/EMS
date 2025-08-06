using Asp.Versioning;
using CleanArc.Application.Features.Gender.Commands.CreateGenderCommand;
using CleanArc.Application.Features.Gender.Commands.DeleteGenderCommand;
using CleanArc.Application.Features.Gender.Commands.UpdateGenderCommand;
using CleanArc.Application.Features.Gender.Queries.GetGenderById;
using CleanArc.Application.Features.Gender.Queries.GetAllGender;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.Gender
{
    /// <summary>
    /// GenderController is responsible for handling HTTP requests related to Gender operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateGender: Handles the creation of a new age type.
    /// 2. UpdateGender: Handles the updating of an existing age type.
    /// 3. DeleteGender: Handles the deletion of an existing age type.
    /// 4. GetAllGender: Retrieves all age types.
    /// 5. GetGenderById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/Gender".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateGender")]
    /// public async Task<IActionResult> CreateGender(CreateGenderCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.Gender.Commands.CreateGenderCommand.CreateGenderCommand, CleanArc.Application.Features.Gender.Commands.UpdateGenderCommand.UpdateGenderCommand, CleanArc.Application.Features.Gender.Commands.DeleteGenderCommand.DeleteGenderCommand, System.ResponseEntity, CleanArc.Application.Features.Gender.Queries.GetAllGender.GetAllGenderQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.Gender.Queries.GetAllGender.GetAllGenderQueryResult&gt;, CleanArc.Application.Features.Gender.Queries.GetGenderById.GetGenderByIdQuery, CleanArc.Application.Features.Gender.Queries.GetGenderById.GetGenderByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/Gender")]
    //[Authorize]
    public class GenderController : _BaseController<CreateGenderCommand, UpdateGenderCommand, DeleteGenderCommand, ResponseEntity, GetAllGenderQuery,
    List<GetAllGenderQueryResult>, GetGenderByIdQuery, GetGenderByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public GenderController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateGender")]
        //public async Task<IActionResult> CreateGender(CreateGenderCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateGender")]
        //public async Task<IActionResult> UpdateGender(UpdateGenderCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteGender")]
        //public async Task<IActionResult> DeleteGender(DeleteGenderCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllGender")]
        //public async Task<IActionResult> GetAllGender( )
        //{
        //    var queryResult = await _sender.Send(new GetAllGenderQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="GenderController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public GenderController(ISender sender, ILogger<_BaseController<CreateGenderCommand, UpdateGenderCommand, DeleteGenderCommand, ResponseEntity, GetAllGenderQuery,
   List<GetAllGenderQueryResult>, GetGenderByIdQuery, GetGenderByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
