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
using CleanArc.Application.Features.BusinessType.Queries.GetBusinessTypeById;

namespace CleanArc.Application.Features.BusinessProfile.Queries.GetBusinessProfileById;

internal class GetBusinessProfileByIdQueryHandler : IRequestHandler<GetBusinessProfileByIdQuery, OperationResult<GetBusinessProfileByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetBusinessProfileByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetBusinessProfileByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetBusinessProfileByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetBusinessProfileByIdQueryResult>> Handle(GetBusinessProfileByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var BusinessProfile= await _unitOfWork.BusinessProfileRepository.GetByIdAsync(request.Id);

            //if (BusinessProfile == null)
            //{
            //    return OperationResult<GetBusinessProfileByIdQueryResult>.NotFoundResult("BusinessProfile not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetBusinessProfileByIdQueryResult>(BusinessProfile);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetBusinessProfileByIdQueryResult>.SuccessResult(result);

            var response = await _unitOfWork.BusinessProfileRepository.GetByIdAsync(request.Id);

            if (response.Code != 200)
            {
                return OperationResult<GetBusinessProfileByIdQueryResult>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<GetBusinessProfileByIdQueryResult>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<GetBusinessProfileByIdQueryResult>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }

  
}



