using Asp.Versioning;
using CleanArc.Application.Features.WishList.Command.CreateWishListCommand;
using CleanArc.Application.Features.WishList.Command.DeleteWishListCommand;
using CleanArc.Application.Features.WishList.Command.UpdateWishListCommand;
using CleanArc.Application.Features.WishList.Queries.GetAllWishList;
using CleanArc.Application.Features.WishList.Queries.GetWishListById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; 
using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.WishList;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/WishList")]
public class WishListController : _BaseController<CreateWishListCommand, UpdateWishListCommand, DeleteWishListCommand, ResponseEntity, GetAllWishListQuery,
    List<GetAllWishListQueryResult>, GetWishListByIdQuery, GetWishListByIdQueryResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WishListController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public WishListController(ISender sender, ILogger<_BaseController<CreateWishListCommand, UpdateWishListCommand, DeleteWishListCommand, ResponseEntity, GetAllWishListQuery,
List<GetAllWishListQueryResult>, GetWishListByIdQuery, GetWishListByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

