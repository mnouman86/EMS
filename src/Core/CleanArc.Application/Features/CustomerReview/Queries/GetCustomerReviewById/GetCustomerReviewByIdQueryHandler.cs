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

namespace CleanArc.Application.Features.CustomerReview.Queries.GetCustomerReviewById;

internal class GetCustomerReviewByIdQueryHandler : IRequestHandler<GetCustomerReviewByIdQuery, OperationResult<GetCustomerReviewByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetCustomerReviewByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetCustomerReviewByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetCustomerReviewByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetCustomerReviewByIdQueryResult>> Handle(GetCustomerReviewByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            var CustomerReview = await _unitOfWork.CustomerReviewRepository.GetByIdAsync(request.Id);

            if (CustomerReview == null)
            {
                return OperationResult<GetCustomerReviewByIdQueryResult>.NotFoundResult("CustomerReview not found");
            }

            //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            var result = _mapper.Map<GetCustomerReviewByIdQueryResult>(CustomerReview);

            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            return OperationResult<GetCustomerReviewByIdQueryResult>.SuccessResult(result);
        }
    }

    //public ValueTask<OperationResult<GetAgeTypeByIdQueryResult>> Handle(GetAgeTypeByIdQuery request, CancellationToken cancellationToken)
    //{
    //    throw new NotImplementedException();
    //}
}

