using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
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
using CleanArc.Application.Features.Activity.Queries.GetActivityById;
using Serilog.Core;

namespace CleanArc.Application.Features.ServiceCategory.Queries.GetServiceCategoryById;

internal class GetServiceCategoryByIdQueryHandler : IRequestHandler<GetServiceCategoryByIdQuery, OperationResult<GetServiceCategoryByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetServiceCategoryByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetServiceCategoryByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetServiceCategoryByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetServiceCategoryByIdQueryResult>> Handle(GetServiceCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
		{
			var response = await _unitOfWork.ServiceCategoryRepository.GetByIdAsync(request.Id);

			if (response.Code != 200)
			{
				return OperationResult<GetServiceCategoryByIdQueryResult>.FailureResult(
				response.Message,
					response.Code
				);
			}

			var mappedResult = _mapper.Map<GetServiceCategoryByIdQueryResult>(response.Data);
			(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

			return OperationResult<GetServiceCategoryByIdQueryResult>.SuccessResult(
				mappedResult,
				response.Code,
				response.Message
			);

			if (mappedResult == null)
			{
				return OperationResult<GetServiceCategoryByIdQueryResult>.NotFoundResult("Service Category not found");
			}

		}
	}

    
}


