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

namespace CleanArc.Application.Features.ActivityGroup.Queries.GetActivityGroupById
{
    internal class GetActivityGroupByIdQueryHandler : IRequestHandler<GetActivityGroupByIdQuery, OperationResult<GetActivityGroupByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetActivityGroupByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetActivityGroupByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetActivityGroupByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetActivityGroupByIdQueryResult>> Handle(GetActivityGroupByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var ActivityGroup = await _unitOfWork.ActivityGroupRepository.GetByIdAsync(request.Id);

                if (ActivityGroup == null)
                {
                    return OperationResult<GetActivityGroupByIdQueryResult>.NotFoundResult("ActivityGroup not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetActivityGroupByIdQueryResult>(ActivityGroup);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetActivityGroupByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetActivityGroupByIdQueryResult>> Handle(GetActivityGroupByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
