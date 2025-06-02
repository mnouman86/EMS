using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.WishListName.Queries.GetWishListNameById;

internal class GetWishListNameByIdQueryHandler : IRequestHandler<GetWishListNameByIdQuery, OperationResult<GetWishListNameByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetWishListNameByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetWishListNameByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetWishListNameByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetWishListNameByIdQueryResult>> Handle(GetWishListNameByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var WishListName = await _unitOfWork.WishListNameRepository.GetByIdAsync(request.searchRequestById);

            //if (WishListName == null)
            //{
            //    return OperationResult<GetWishListNameByIdQueryResult>.NotFoundResult("WishListName not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetWishListNameByIdQueryResult>(WishListName);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetWishListNameByIdQueryResult>.SuccessResult(result);

            var response = await _unitOfWork.WishListNameRepository.GetByIdAsync(request.searchRequestById);

            if (response.Code != 200)
            {
                return OperationResult<GetWishListNameByIdQueryResult>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<GetWishListNameByIdQueryResult>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<GetWishListNameByIdQueryResult>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }

    
}

