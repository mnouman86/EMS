using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
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

namespace CleanArc.Application.Features.BusinessType.Queries.GetAllBusinessType;

internal class GetAllBusinessTypeQueryHandler : IRequestHandler<GetAllBusinessTypeQuery, OperationResult<List<GetAllBusinessTypeQueryResult>>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllBusinessTypeQueryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


    public GetAllBusinessTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllBusinessTypeQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;

    }

    public async ValueTask<OperationResult<List<GetAllBusinessTypeQueryResult>>> Handle(GetAllBusinessTypeQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var userId = int.Parse(_httpContextAccessor?.HttpContext.User.Identity.GetUserId());
            //if (request.searchRequest.FilterArray == null)
            //{
            //    request.searchRequest.FilterArray = new List<FilterParameter>();
            //}
           // request.searchRequest.FilterArray.Add(new FilterParameter { ParameterName = "CreatedBy", ParameterValue = Convert.ToString(userId) });
            var BusinessTypees = await _unitOfWork.BusinessTypeRepository.GetAllAsync(request.searchRequest);

            //var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
            var result = _mapper.Map<List<GetAllBusinessTypeQueryResult>>(BusinessTypees);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
            return OperationResult<List<GetAllBusinessTypeQueryResult>>.SuccessResult(result);
        }
    }
}




