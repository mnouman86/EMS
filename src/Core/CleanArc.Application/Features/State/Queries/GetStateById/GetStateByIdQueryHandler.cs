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

namespace CleanArc.Application.Features.State.Queries.GetStateById;

internal class GetStateByIdQueryHandler : IRequestHandler<GetStateByIdQuery, OperationResult<GetStateByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetStateByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetStateByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetStateByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetStateByIdQueryResult>> Handle(GetStateByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            var state = await _unitOfWork.StateRepository.GetByIdAsync(request.Id);

            if (state == null)
            {
                return OperationResult<GetStateByIdQueryResult>.NotFoundResult("state not found");
            }

            //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            var result = _mapper.Map<GetStateByIdQueryResult>(state);

            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            return OperationResult<GetStateByIdQueryResult>.SuccessResult(result);
        }
    }

    //public ValueTask<OperationResult<GetAgeTypeByIdQueryResult>> Handle(GetAgeTypeByIdQuery request, CancellationToken cancellationToken)
    //{
    //    throw new NotImplementedException();
    //}
}



