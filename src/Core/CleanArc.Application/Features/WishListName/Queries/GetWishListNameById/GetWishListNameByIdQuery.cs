using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Features.WishListName.Queries.GetWishListNameById;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.WishListName.Queries.GetWishListNameById;

public record GetWishListNameByIdQuery(SearchRequestById searchRequestById):IRequest<OperationResult<GetWishListNameByIdQueryResult>>;
