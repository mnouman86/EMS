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

namespace CleanArc.Application.Features.ActivitySchedule.Queries.GetActivityScheduleById
{
    internal class GetActivityScheduleByIdQueryHandler : IRequestHandler<GetActivityScheduleByIdQuery, OperationResult<GetActivityScheduleByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetActivityScheduleByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetActivityScheduleByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetActivityScheduleByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetActivityScheduleByIdQueryResult>> Handle(GetActivityScheduleByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var ActivitySchedule = await _unitOfWork.ActivityScheduleRepository.GetByIdAsync(request.Id);

                if (ActivitySchedule == null)
                {
                    return OperationResult<GetActivityScheduleByIdQueryResult>.NotFoundResult("ActivitySchedule not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetActivityScheduleByIdQueryResult>(ActivitySchedule);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetActivityScheduleByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetActivityScheduleByIdQueryResult>> Handle(GetActivityScheduleByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
