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

namespace CleanArc.Application.Features.RoomDiscountByUserType.Queries.GetAllRoomDiscountByUserTypes
{
    internal class GetAllRoomDiscountByUserTypesQueryHandler : IRequestHandler<GetAllRoomDiscountByUserTypesQuery, OperationResult<List<GetAllRoomDiscountByUserTypesQueryResult>>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllRoomDiscountByUserTypesQueryHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


        public GetAllRoomDiscountByUserTypesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllRoomDiscountByUserTypesQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }

        public async ValueTask<OperationResult<List<GetAllRoomDiscountByUserTypesQueryResult>>> Handle(GetAllRoomDiscountByUserTypesQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var roomDiscountByUserType = await _unitOfWork.RoomDiscountByUserTypeRepository.GetAllAsync(request.searchRequest);

                ////var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
                //var result = _mapper.Map<List<GetAllRoomDiscountByUserTypesQueryResult>>(roomDiscountByUserType);
                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                //return OperationResult<List<GetAllRoomDiscountByUserTypesQueryResult>>.SuccessResult(result);

                var response = await _unitOfWork.RoomDiscountByUserTypeRepository.GetAllAsync(request.searchRequest);

                if (response.Code != 200)
                {
                    return OperationResult<List<GetAllRoomDiscountByUserTypesQueryResult>>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<List<GetAllRoomDiscountByUserTypesQueryResult>>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<List<GetAllRoomDiscountByUserTypesQueryResult>>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message,
                    response.TotalCount
                );
            }
        }
    }

}
