using Asp.Versioning;
using CleanArc.Application.Features.WishListName.Command.CreateWishListNameCommand;
using CleanArc.Application.Features.WishListName.Command.DeleteWishListNameCommand;
using CleanArc.Application.Features.WishListName.Command.UpdateWishListNameCommand;
using CleanArc.Application.Features.WishListName.Queries.GetAllWishListName;
using CleanArc.Application.Features.WishListName.Queries.GetWishListNameById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; 
using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.WishListName;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/WishListName")]
public class WishListNameController : _BaseController<CreateWishListNameCommand, UpdateWishListNameCommand, DeleteWishListNameCommand, ResponseEntity, GetAllWishListNameQuery,
    List<GetAllWishListNameQueryResult>, GetWishListNameByIdQuery, GetWishListNameByIdQueryResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WishListNameController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public WishListNameController(ISender sender, ILogger<_BaseController<CreateWishListNameCommand, UpdateWishListNameCommand, DeleteWishListNameCommand, ResponseEntity, GetAllWishListNameQuery,
List<GetAllWishListNameQueryResult>, GetWishListNameByIdQuery, GetWishListNameByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

