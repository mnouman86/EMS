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
using CleanArc.Application.Features.CheckProfileStatus.Queries.GetCheckProfileStatusById;

namespace CleanArc.Application.Features.Category.Queries.GetCategoryById;

internal class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, OperationResult<GetCategoryByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetCategoryByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetCategoryByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetCategoryByIdQueryResult>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var url = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);

            //if (url == null)
            //{
            //    return OperationResult<GetCategoryByIdQueryResult>.NotFoundResult("URL not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetCategoryByIdQueryResult>(url);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetCategoryByIdQueryResult>.SuccessResult(result);

            var response = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);

            if (response.Code != 200)
            {
                return OperationResult<GetCategoryByIdQueryResult>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<GetCategoryByIdQueryResult>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<GetCategoryByIdQueryResult>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }

    //public ValueTask<OperationResult<GetAgeTypeByIdQueryResult>> Handle(GetAgeTypeByIdQuery request, CancellationToken cancellationToken)
    //{
    //    throw new NotImplementedException();
    //}
}

