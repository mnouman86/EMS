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
using CleanArc.Application.Features.KBDetail.Queries.GetKBDetailById;
using Serilog.Core;

namespace CleanArc.Application.Features.Business.Queries.GetBusinessById;

internal class GetBusinessByIdQueryHandler : IRequestHandler<GetBusinessByIdQuery, OperationResult<GetBusinessByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetBusinessByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetBusinessByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetBusinessByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetBusinessByIdQueryResult>> Handle(GetBusinessByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
       

		{
			var response = await _unitOfWork.BusinessRepository.GetByIdAsync(request.Id);

			if (response.Code != 200)
			{
				return OperationResult<GetBusinessByIdQueryResult>.FailureResult(
				response.Message,
					response.Code
				);
			}

			var mappedResult = _mapper.Map<GetBusinessByIdQueryResult>(response.Data);
			(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

			return OperationResult<GetBusinessByIdQueryResult>.SuccessResult(
				mappedResult,
				response.Code,
				response.Message
			);

			if (mappedResult == null)
			{
				return OperationResult<GetBusinessByIdQueryResult>.NotFoundResult("Business not found");
			}

		}
	}

  
}



