using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.URL.Queries.GetURLById;
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
using CleanArc.Application.Features.RoomDetails.Queries.GetRoomDetailById;

namespace CleanArc.Application.Features.ProcessOrder.Queries.GetProcessOrderById
{
    internal class GetProcessOrderByIdQueryHandler : IRequestHandler<GetProcessOrderByIdQuery, OperationResult<GetProcessOrderByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetProcessOrderByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetProcessOrderByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetProcessOrderByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetProcessOrderByIdQueryResult>> Handle(GetProcessOrderByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var ProcessOrder = await _unitOfWork.ProcessOrderRepository.GetByIdAsync(request.Id);

                //if (ProcessOrder == null)
                //{
                //    return OperationResult<GetProcessOrderByIdQueryResult>.NotFoundResult("ProcessOrder not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetProcessOrderByIdQueryResult>(ProcessOrder);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetProcessOrderByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.ProcessOrderRepository.GetByIdAsync(request.Id);

                if (response.Code != 200)
                {
                    return OperationResult<GetProcessOrderByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetProcessOrderByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetProcessOrderByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

        //public ValueTask<OperationResult<GetProcessOrderByIdQueryResult>> Handle(GetProcessOrderByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
