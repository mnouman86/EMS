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
using CleanArc.Application.Features.Bank.Queries.GetBankById;
using Serilog.Core;

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
				var response = await _unitOfWork.ActivityScheduleRepository.GetByIdAsync(request.Id);

				if (response.Code != 200)
				{
					return OperationResult<GetActivityScheduleByIdQueryResult>.FailureResult(
					response.Message,
						response.Code
					);
				}

				var mappedResult = _mapper.Map<GetActivityScheduleByIdQueryResult>(response.Data);
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

				return OperationResult<GetActivityScheduleByIdQueryResult>.SuccessResult(
					mappedResult,
					response.Code,
					response.Message
				);

				if (mappedResult == null)
				{
					return OperationResult<GetActivityScheduleByIdQueryResult>.NotFoundResult("Schedule not found");
				}

			}
		}

        //public ValueTask<OperationResult<GetActivityScheduleByIdQueryResult>> Handle(GetActivityScheduleByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
