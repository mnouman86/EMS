using CleanArc.Application.Contracts.Persistence;
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

namespace CleanArc.Application.Features.Business.Query.GetBusinessById;

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
            var business= await _unitOfWork.BusinessRepository.GetByIdAsync(request.Id);

            if (business == null)
            {
                return OperationResult<GetBusinessByIdQueryResult>.NotFoundResult("business not found");
            }

            //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            var result = _mapper.Map<GetBusinessByIdQueryResult>(business);

            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            return OperationResult<GetBusinessByIdQueryResult>.SuccessResult(result);
        }
    }

  
}



