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
using CleanArc.Application.Features.CampaignSchedule.Queries.GetCampaignScheduleById;

namespace CleanArc.Application.Features.Campaign.Queries.GetCampaignById;

internal class GetCampaignByIdQueryHandler : IRequestHandler<GetCampaignByIdQuery, OperationResult<GetCampaignByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetCampaignByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetCampaignByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetCampaignByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetCampaignByIdQueryResult>> Handle(GetCampaignByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var Campaign = await _unitOfWork.CampaignRepository.GetByIdAsync(request.searchRequestById);

            //if (Campaign == null)
            //{
            //    return OperationResult<GetCampaignByIdQueryResult>.NotFoundResult("Campaign not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetCampaignByIdQueryResult>(Campaign);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetCampaignByIdQueryResult>.SuccessResult(result);

            var response = await _unitOfWork.CampaignRepository.GetByIdAsync(request.searchRequestById);

            if (response.Code != 200)
            {
                return OperationResult<GetCampaignByIdQueryResult>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<GetCampaignByIdQueryResult>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<GetCampaignByIdQueryResult>.SuccessResult(
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

