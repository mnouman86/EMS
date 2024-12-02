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
using CleanArc.Application.Features.CampaignTarget.Queries.GetAllCampaignTarget;

namespace CleanArc.Application.Features.CampaignTarget.Queries.GetAllCampaignTarget
{
    internal class GetAllCampaignTargetQueryHandler : IRequestHandler<GetAllCampaignTargetQuery, OperationResult<List<GetAllCampaignTargetQueryResult>>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCampaignTargetQueryHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


        public GetAllCampaignTargetQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllCampaignTargetQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }

        public async ValueTask<OperationResult<List<GetAllCampaignTargetQueryResult>>> Handle(GetAllCampaignTargetQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var CampaignTarget = await _unitOfWork.CampaignTargetRepository.GetAllAsync(request.searchRequest);
                var result = _mapper.Map<List<GetAllCampaignTargetQueryResult>>(CampaignTarget);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return OperationResult<List<GetAllCampaignTargetQueryResult>>.SuccessResult(result);
            }
        }
    }

}
