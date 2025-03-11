using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Features.Activity.Queries.GetAllActivity;

namespace CleanArc.Application.Features.SearchFilterThingsToDo.Queries.GetAllSearchFilterThingsToDo
{
    internal class GetAllSearchFilterThingsToDoQueryHandler : IRequestHandler<GetAllSearchFilterThingsToDoQuery, OperationResult<List<GetAllSearchFilterThingsToDoQueryResult>>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllSearchFilterThingsToDoQueryHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


        public GetAllSearchFilterThingsToDoQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllSearchFilterThingsToDoQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }

        public async ValueTask<OperationResult<List<GetAllSearchFilterThingsToDoQueryResult>>> Handle(GetAllSearchFilterThingsToDoQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var Product = await _unitOfWork.SearchFilterThingsToDoRepository.GetAllWithParamAsync(request.searchRequest);

                ////var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
                //var result = _mapper.Map<List<GetAllSearchFilterThingsToDoQueryResult>>(Product);
                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                //return OperationResult<List<GetAllSearchFilterThingsToDoQueryResult>>.SuccessResult(result);

                var response = await _unitOfWork.SearchFilterThingsToDoRepository.GetAllWithParamAsync(request.searchRequest);

                //if (response.Code != 200)
                //{
                //    return OperationResult<List<GetAllSearchFilterThingsToDoQueryResult>>.FailureResult(
                //        response.Message,
                //    response.Code
                //    );
                //}

                var mappedResult = _mapper.Map<List<GetAllSearchFilterThingsToDoQueryResult>>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<List<GetAllSearchFilterThingsToDoQueryResult>>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }
    }

}
