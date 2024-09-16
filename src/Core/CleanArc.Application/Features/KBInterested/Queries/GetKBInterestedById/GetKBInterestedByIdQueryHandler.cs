using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.URL.Queries.GetURLById;
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

namespace CleanArc.Application.Features.KBInterested.Queries.GetKBInterestedById
{
    internal class GetKBInterestedByIdQueryHandler : IRequestHandler<GetKBInterestedByIdQuery, OperationResult<GetKBInterestedByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetKBInterestedByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetKBInterestedByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetKBInterestedByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetKBInterestedByIdQueryResult>> Handle(GetKBInterestedByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var KBInterested = await _unitOfWork.KBInterestedRepository.GetByIdAsync(request.Id);

                if (KBInterested == null)
                {
                    return OperationResult<GetKBInterestedByIdQueryResult>.NotFoundResult("KBInterested not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetKBInterestedByIdQueryResult>(KBInterested);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetKBInterestedByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetKBInterestedByIdQueryResult>> Handle(GetKBInterestedByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
