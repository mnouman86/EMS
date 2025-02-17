using CleanArc.Application.Contracts.Persistence;
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
using CleanArc.Application.Features.Campaign.Queries.GetCampaignById;
using CleanArc.Application.Features.CarDetail.Queries.GetCarDetailById;

namespace CleanArc.Application.Features.CampaignTargetItems.Queries.GetCampaignTargetItemsById;

internal class GetCampaignTargetItemsByIdQueryHandler : IRequestHandler<GetCampaignTargetItemsByIdQuery, OperationResult<GetCampaignTargetItemsByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetCampaignTargetItemsByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetCampaignTargetItemsByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetCampaignTargetItemsByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetCampaignTargetItemsByIdQueryResult>> Handle(GetCampaignTargetItemsByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var CampaignTargetItems = await _unitOfWork.CampaignTargetItemsRepository.GetByIdAsync(request.Id);

            //if (CampaignTargetItems == null)
            //{
            //    return OperationResult<GetCampaignTargetItemsByIdQueryResult>.NotFoundResult("Campaign not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetCampaignTargetItemsByIdQueryResult>(CampaignTargetItems);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetCampaignTargetItemsByIdQueryResult>.SuccessResult(result);

            var response = await _unitOfWork.CampaignTargetItemsRepository.GetByIdAsync(request.Id);

            if (response.Code != 200)
            {
                return OperationResult<GetCampaignTargetItemsByIdQueryResult>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<GetCampaignTargetItemsByIdQueryResult>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<GetCampaignTargetItemsByIdQueryResult>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }

    //public ValueTask<OperationResult<GetAgeTypeByIdQueryResult>> Handle(GetAgeTypeByIdQuery request, CancellationToken cancellationToken)
    //{
    //    throw new NotImplementedException();
    //}
}

