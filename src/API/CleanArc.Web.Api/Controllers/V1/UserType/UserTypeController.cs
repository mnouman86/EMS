using Asp.Versioning;
using CleanArc.Application.Features.UserType.Command.CreateUserTypeCommand;
using CleanArc.Application.Features.UserType.Command.DeleteUserTypeCommand;
using CleanArc.Application.Features.UserType.Command.UpdateUserTypeCommand;
using CleanArc.Application.Features.UserType.Queries.GetAllUserTypes;
using CleanArc.Application.Features.UserType.Queries.GetUserTypeById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.UserType
{
    /// <summary>
    /// UserTypeController is responsible for handling HTTP requests related to room type operations
    /// such as creating, updating, deleting, and retrieving room types. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateUserType: Handles the creation of a new room type.
    /// 2. UpdateUserType: Handles the updating of an existing room type.
    /// 3. DeleteUserType: Handles the deletion of an existing room type.
    /// 4. GetAllUserTypes: Retrieves all room types.
    /// 5. GetUserTypeById: Retrieves a specific room type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/UserType".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateUserType")]
    /// public async Task<IActionResult> CreateUserType(CreateUserTypeCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.UserType.Command.CreateUserTypeCommand.CreateUserTypeCommand, CleanArc.Application.Features.UserType.Command.UpdateUserTypeCommand.UpdateUserTypeCommand, CleanArc.Application.Features.UserType.Command.DeleteUserTypeCommand.DeleteUserTypeCommand, System.ResponseEntity, CleanArc.Application.Features.UserType.Queries.GetAllUserTypes.GetAllUserTypesQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.UserType.Queries.GetAllUserTypes.GetAllUserTypesQueryResult&gt;, CleanArc.Application.Features.UserType.Queries.GetUserTypeById.GetUserTypeByIdQuery, CleanArc.Application.Features.UserType.Queries.GetUserTypeById.GetUserTypeByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/UserType")]
    //[Authorize]
    public class UserTypeController : _BaseController<CreateUserTypeCommand, UpdateUserTypeCommand, DeleteUserTypeCommand, ResponseEntity, GetAllUserTypesQuery,
    List<GetAllUserTypesQueryResult>, GetUserTypeByIdQuery, GetUserTypeByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UserTypeController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public UserTypeController(ISender sender, ILogger<_BaseController<CreateUserTypeCommand, UpdateUserTypeCommand, DeleteUserTypeCommand, ResponseEntity, GetAllUserTypesQuery,
   List<GetAllUserTypesQueryResult>, GetUserTypeByIdQuery, GetUserTypeByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
