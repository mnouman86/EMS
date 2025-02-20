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
using CleanArc.Application.Features.Business.Queries.GetBusinessById;
using Serilog.Core;

namespace CleanArc.Application.Features.BusinessBankAccount.Queries.GetBusinessBankAccountById;

internal class GetBusinessBankAccountByIdQueryHandler : IRequestHandler<GetBusinessBankAccountByIdQuery, OperationResult<GetBusinessBankAccountByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetBusinessBankAccountByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetBusinessBankAccountByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetBusinessBankAccountByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetBusinessBankAccountByIdQueryResult>> Handle(GetBusinessBankAccountByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
      

		{
			var response = await _unitOfWork.BusinessBankAccountRepository.GetByIdAsync(request.Id);

			if (response.Code != 200)
			{
				return OperationResult<GetBusinessBankAccountByIdQueryResult>.FailureResult(
				response.Message,
					response.Code
				);
			}

			var mappedResult = _mapper.Map<GetBusinessBankAccountByIdQueryResult>(response.Data);
			(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

			return OperationResult<GetBusinessBankAccountByIdQueryResult>.SuccessResult(
				mappedResult,
				response.Code,
				response.Message
			);

			if (mappedResult == null)
			{
				return OperationResult<GetBusinessBankAccountByIdQueryResult>.NotFoundResult("Bank account not found");
			}

		}


	}

    
}


