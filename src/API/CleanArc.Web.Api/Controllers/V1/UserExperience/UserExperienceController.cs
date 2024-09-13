using Asp.Versioning;
using CleanArc.Application.Features.UserExperience.Commands.CreateUserExperienceCommand;
using CleanArc.Application.Features.UserExperience.Commands.DeleteUserExperienceCommand;
using CleanArc.Application.Features.UserExperience.Commands.UpdateUserExperienceCommand;
using CleanArc.Application.Features.UserExperience.Queries.GetUserExperienceById;
using CleanArc.Application.Features.UserExperience.Queries.GetAllUserExperience;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.UserExperience
{
    /// <summary>
    /// UserExperienceController is responsible for handling HTTP requests related to UserExperience operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateUserExperience: Handles the creation of a new age type.
    /// 2. UpdateUserExperience: Handles the updating of an existing age type.
    /// 3. DeleteUserExperience: Handles the deletion of an existing age type.
    /// 4. GetAllUserExperience: Retrieves all age types.
    /// 5. GetUserExperienceById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/UserExperience".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateUserExperience")]
    /// public async Task<IActionResult> CreateUserExperience(CreateUserExperienceCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.UserExperience.Commands.CreateUserExperienceCommand.CreateUserExperienceCommand, CleanArc.Application.Features.UserExperience.Commands.UpdateUserExperienceCommand.UpdateUserExperienceCommand, CleanArc.Application.Features.UserExperience.Commands.DeleteUserExperienceCommand.DeleteUserExperienceCommand, System.Boolean, CleanArc.Application.Features.UserExperience.Queries.GetAllUserExperience.GetAllUserExperienceQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.UserExperience.Queries.GetAllUserExperience.GetAllUserExperienceQueryResult&gt;, CleanArc.Application.Features.UserExperience.Queries.GetUserExperienceById.GetUserExperienceByIdQuery, CleanArc.Application.Features.UserExperience.Queries.GetUserExperienceById.GetUserExperienceByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/UserExperience")]
    //[Authorize]
    public class UserExperienceController : _BaseController<CreateUserExperienceCommand, UpdateUserExperienceCommand, DeleteUserExperienceCommand, bool, GetAllUserExperienceQuery,
    List<GetAllUserExperienceQueryResult>, GetUserExperienceByIdQuery, GetUserExperienceByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public UserExperienceController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateUserExperience")]
        //public async Task<IActionResult> CreateUserExperience(CreateUserExperienceCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateUserExperience")]
        //public async Task<IActionResult> UpdateUserExperience(UpdateUserExperienceCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteUserExperience")]
        //public async Task<IActionResult> DeleteUserExperience(DeleteUserExperienceCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllUserExperience")]
        //public async Task<IActionResult> GetAllUserExperience( )
        //{
        //    var queryResult = await _sender.Send(new GetAllUserExperienceQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="UserExperienceController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public UserExperienceController(ISender sender, ILogger<_BaseController<CreateUserExperienceCommand, UpdateUserExperienceCommand, DeleteUserExperienceCommand, bool, GetAllUserExperienceQuery,
   List<GetAllUserExperienceQueryResult>, GetUserExperienceByIdQuery, GetUserExperienceByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
