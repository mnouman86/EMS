using Asp.Versioning;
using CleanArc.Application.Features.CustomerReview.Command.CreateCustomerReviewCommand;
using CleanArc.Application.Features.CustomerReview.Command.DeleteCustomerReviewCommand;
using CleanArc.Application.Features.CustomerReview.Command.UpdateCustomerReviewCommand;
using CleanArc.Application.Features.CustomerReview.Queries.GetAllCustomerReviews;
using CleanArc.Application.Features.CustomerReview.Queries.GetCustomerReviewById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.CustomerReview;
/// <summary>
/// CustomerReviewController is responsible for handling HTTP requests related to CustomerReview operations
/// such as creating, updating, deleting, and retrieving CustomerReviews. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateCustomerReview: Handles the creation of a new CustomerReview.
/// 2. UpdateCustomerReview: Handles the updating of an existing CustomerReview.
/// 3. DeleteCustomerReview: Handles the deletion of an existing CustomerReview.
/// 4. GetAllCustomerReviews: Retrieves all CustomerReviews.
/// 5. GetCustomerReviewById: Retrieves a specific CustomerReview by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/CustomerReview".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// The controller is part of the CleanArc architecture, ensuring a clean separation of concerns and adherence to 
/// SOLID principles.
/// </summary>
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.CustomerReview.Command.CreateCustomerReviewCommand.CreateCustomerReviewCommand, CleanArc.Application.Features.CustomerReview.Command.UpdateCustomerReviewCommand.UpdateCustomerReviewCommand, CleanArc.Application.Features.CustomerReview.Command.DeleteCustomerReviewCommand.DeleteCustomerReviewCommand, System.ResponseEntity, CleanArc.Application.Features.CustomerReview.Queries.GetAllCustomerReviews.GetAllCustomerReviewsQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.CustomerReview.Queries.GetAllCustomerReviews.GetAllCustomerReviewsQueryResult&gt;, CleanArc.Application.Features.CustomerReview.Queries.GetCustomerReviewById.GetCustomerReviewByIdQuery, CleanArc.Application.Features.CustomerReview.Queries.GetCustomerReviewById.GetCustomerReviewByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/CustomerReview")]
//[Authorize]
public class CustomerReviewController : _BaseController<CreateCustomerReviewCommand, UpdateCustomerReviewCommand, DeleteCustomerReviewCommand, ResponseEntity, GetAllCustomerReviewsQuery,
    List<GetAllCustomerReviewsQueryResult>, GetCustomerReviewByIdQuery, GetCustomerReviewByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerReviewController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public CustomerReviewController(ISender sender, ILogger<_BaseController<CreateCustomerReviewCommand, UpdateCustomerReviewCommand, DeleteCustomerReviewCommand, ResponseEntity, GetAllCustomerReviewsQuery,
List<GetAllCustomerReviewsQueryResult>, GetCustomerReviewByIdQuery, GetCustomerReviewByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}
