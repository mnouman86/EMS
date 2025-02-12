using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.URL.Commands.DeleteURLCommand;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.URL.Queries.GetAllURLs
{
    internal class GetAllOrdersQueryHandler:IRequestHandler<GetAllURLsQuery,OperationResult<List<GetAllURLsQueryResult>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllOrdersQueryHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


        public GetAllOrdersQueryHandler(IUnitOfWork unitOfWork,IMapper mapper, IHttpContextAccessor httpContextAccessor,ILogger<GetAllOrdersQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger= logger;
        }

        public async ValueTask<OperationResult<List<GetAllURLsQueryResult>>> Handle(GetAllURLsQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var uRLs = 

                //var resultCheck = uRLs.Select(c => new GetAllURLsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
                //var result = _mapper.Map<List<GetAllURLsQueryResult>>(uRLs);
                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                //return OperationResult<List<GetAllURLsQueryResult>>.SuccessResult(result);

                var response = await _unitOfWork.URLRepository.GetAllAsync(request.searchRequest);

                if (response.Code != 200)
                {
                    return OperationResult<List<GetAllURLsQueryResult>>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<List<GetAllURLsQueryResult>>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<List<GetAllURLsQueryResult>>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }
    }
}
