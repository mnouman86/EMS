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
using CleanArc.Application.Features.Activity.Queries.GetAllActivity;

namespace CleanArc.Application.Features.ServiceCategory.Queries.GetAllServiceCategories;

internal class GetAllServiceCategoriesQueryHandler : IRequestHandler<GetAllServiceCategoriesQuery, OperationResult<List<GetAllServiceCategoriesQueryResult>>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllServiceCategoriesQueryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


    public GetAllServiceCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllServiceCategoriesQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;

    }

    public async ValueTask<OperationResult<List<GetAllServiceCategoriesQueryResult>>> Handle(GetAllServiceCategoriesQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var Product = 

            //var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
            //var result = _mapper.Map<List<GetAllServiceCategoriesQueryResult>>(Product);
            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
            //return OperationResult<List<GetAllServiceCategoriesQueryResult>>.SuccessResult(result);

            var response = await _unitOfWork.ServiceCategoryRepository.GetAllAsync(request.searchRequest);

            if (response.Code != 200)
            {
                return OperationResult<List<GetAllServiceCategoriesQueryResult>>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<List<GetAllServiceCategoriesQueryResult>>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<List<GetAllServiceCategoriesQueryResult>>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }
}



