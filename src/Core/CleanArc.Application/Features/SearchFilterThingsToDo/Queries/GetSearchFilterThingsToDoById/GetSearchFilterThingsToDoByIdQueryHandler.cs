using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.URL.Queries.GetURLById;
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

namespace CleanArc.Application.Features.SearchFilterThingsToDo.Queries.GetSearchFilterThingsToDoById
{
    internal class GetSearchFilterThingsToDoByIdQueryHandler 
    //    : IRequestHandler<GetSearchFilterThingsToDoByIdQuery, OperationResult<GetSearchFilterThingsToDoByIdQueryResult>>
    {
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly ILogger<GetSearchFilterThingsToDoByIdQueryHandler> _logger;
    //    private readonly IMapper _mapper;
    //    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    //    public GetSearchFilterThingsToDoByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetSearchFilterThingsToDoByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    //    {
    //        _unitOfWork = unitOfWork;
    //        _mapper = mapper;
    //        _httpContextAccessor = httpContextAccessor;
    //        _logger = logger;
    //    }
    //    public async ValueTask<OperationResult<GetSearchFilterThingsToDoByIdQueryResult>> Handle(GetSearchFilterThingsToDoByIdQuery request, CancellationToken cancellationToken)
    //    {
    //        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
    //        {
    //            var SearchFilterThingsToDo = await _unitOfWork.SearchFilterThingsToDoRepository.GetByIdAsync(request.Id);

    //            if (SearchFilterThingsToDo == null)
    //            {
    //                return OperationResult<GetSearchFilterThingsToDoByIdQueryResult>.NotFoundResult("SearchFilterThingsToDo not found");
    //            }

    //            //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
    //            var result = _mapper.Map<GetSearchFilterThingsToDoByIdQueryResult>(SearchFilterThingsToDo);

    //            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

    //            return OperationResult<GetSearchFilterThingsToDoByIdQueryResult>.SuccessResult(result);
    //        }
    //    }

    //    //public ValueTask<OperationResult<GetSearchFilterThingsToDoByIdQueryResult>> Handle(GetSearchFilterThingsToDoByIdQuery request, CancellationToken cancellationToken)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}
    }
}
