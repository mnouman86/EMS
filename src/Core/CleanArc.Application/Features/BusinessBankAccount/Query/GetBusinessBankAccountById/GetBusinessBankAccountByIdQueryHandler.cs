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

namespace CleanArc.Application.Features.BusinessBankAccount.Query.GetBusinessBankAccountById;

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
            var businessBankAccount = await _unitOfWork.BusinessBankAccountRepository.GetByIdAsync(request.Id);

            if (businessBankAccount == null)
            {
                return OperationResult<GetBusinessBankAccountByIdQueryResult>.NotFoundResult("businessBankAccount not found");
            }

            //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            var result = _mapper.Map<GetBusinessBankAccountByIdQueryResult>(businessBankAccount);

            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            return OperationResult<GetBusinessBankAccountByIdQueryResult>.SuccessResult(result);
        }
    }

    //public ValueTask<OperationResult<GetAgeTypeByIdQueryResult>> Handle(GetAgeTypeByIdQuery request, CancellationToken cancellationToken)
    //{
    //    throw new NotImplementedException();
    //}
}


