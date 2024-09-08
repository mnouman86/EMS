using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.URL.Queries.GetURLById;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBTiming.Queries.GetKBTimingById
{
    internal class GetKBTimingByIdQueryHandler : IRequestHandler<GetKBTimingByIdQuery, OperationResult<GetKBTimingByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetKBTimingByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetKBTimingByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetKBTimingByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetKBTimingByIdQueryResult>> Handle(GetKBTimingByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var KBTiming = await _unitOfWork.KBTimingRepository.GetByIdAsync(request.Id);

                if (KBTiming == null)
                {
                    return OperationResult<GetKBTimingByIdQueryResult>.NotFoundResult("KBTiming not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetKBTimingByIdQueryResult>(KBTiming);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetKBTimingByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetKBTimingByIdQueryResult>> Handle(GetKBTimingByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
