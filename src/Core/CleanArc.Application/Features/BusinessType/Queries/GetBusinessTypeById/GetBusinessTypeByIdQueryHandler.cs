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
using CleanArc.Application.Features.Campaign.Queries.GetCampaignById;

namespace CleanArc.Application.Features.BusinessType.Queries.GetBusinessTypeById;

internal class GetBusinessTypeByIdQueryHandler : IRequestHandler<GetBusinessTypeByIdQuery, OperationResult<GetBusinessTypeByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetBusinessTypeByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetBusinessTypeByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetBusinessTypeByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetBusinessTypeByIdQueryResult>> Handle(GetBusinessTypeByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var BusinessType= await _unitOfWork.BusinessTypeRepository.GetByIdAsync(request.searchRequestById);

            //if (BusinessType == null)
            //{
            //    return OperationResult<GetBusinessTypeByIdQueryResult>.NotFoundResult("BusinessType not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetBusinessTypeByIdQueryResult>(BusinessType);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetBusinessTypeByIdQueryResult>.SuccessResult(result);

            var response = await _unitOfWork.BusinessTypeRepository.GetByIdAsync(request.searchRequestById);

            if (response.Code != 200)
            {
                return OperationResult<GetBusinessTypeByIdQueryResult>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<GetBusinessTypeByIdQueryResult>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<GetBusinessTypeByIdQueryResult>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }

  
}



