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
using CleanArc.Application.Features.CampaignTarget.Queries.GetCampaignTargetById;

namespace CleanArc.Application.Features.CampaignTarget.Queries.GetCampaignTargetById;

internal class GetCampaignTargetByIdQueryHandler : IRequestHandler<GetCampaignTargetByIdQuery, OperationResult<GetCampaignTargetByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetCampaignTargetByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetCampaignTargetByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetCampaignTargetByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetCampaignTargetByIdQueryResult>> Handle(GetCampaignTargetByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            var CampaignTarget = await _unitOfWork.CampaignTargetRepository.GetByIdAsync(request.Id);

            if (CampaignTarget == null)
            {
                return OperationResult<GetCampaignTargetByIdQueryResult>.NotFoundResult("CampaignTarget not found");
            }

            //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            var result = _mapper.Map<GetCampaignTargetByIdQueryResult>(CampaignTarget);

            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            return OperationResult<GetCampaignTargetByIdQueryResult>.SuccessResult(result);
        }
    }

    //public ValueTask<OperationResult<GetAgeTypeByIdQueryResult>> Handle(GetAgeTypeByIdQuery request, CancellationToken cancellationToken)
    //{
    //    throw new NotImplementedException();
    //}
}

