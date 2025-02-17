using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.SharedKernel.Extensions;
using CleanArc.Application.Features.PackageDetail.Queries.GetPackageDetailById;

namespace CleanArc.Application.Features.MappingRoomImage.Queries.GetMappingRoomImageById
{
    internal class GetMappingRoomImageByIdQueryHandler : IRequestHandler<GetMappingRoomImageByIdQuery, OperationResult<GetMappingRoomImageByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetMappingRoomImageByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetMappingRoomImageByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetMappingRoomImageByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetMappingRoomImageByIdQueryResult>> Handle(GetMappingRoomImageByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var ageType = await _unitOfWork.AgeTypeRepository.GetByIdAsync(request.Id);

                //if (ageType == null)
                //{
                //    return OperationResult<GetMappingRoomImageByIdQueryResult>.NotFoundResult("ageType not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetMappingRoomImageByIdQueryResult>(ageType);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetMappingRoomImageByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.MappingRoomImageRepository.GetByIdAsync(request.Id);

                if (response.Code != 200)
                {
                    return OperationResult<GetMappingRoomImageByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetMappingRoomImageByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetMappingRoomImageByIdQueryResult>.SuccessResult(
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
}


