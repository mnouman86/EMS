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

namespace CleanArc.Application.Features.ActivityAddressMapping.Queries.GetActivityAddressMappingById
{
    internal class GetActivityAddressMappingByIdQueryHandler : IRequestHandler<GetActivityAddressMappingByIdQuery, OperationResult<GetActivityAddressMappingByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetActivityAddressMappingByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetActivityAddressMappingByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetActivityAddressMappingByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetActivityAddressMappingByIdQueryResult>> Handle(GetActivityAddressMappingByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var ActivityAddressMapping = await _unitOfWork.ActivityAddressMappingRepository.GetByIdAsync(request.Id);

                if (ActivityAddressMapping == null)
                {
                    return OperationResult<GetActivityAddressMappingByIdQueryResult>.NotFoundResult("ActivityAddressMapping not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetActivityAddressMappingByIdQueryResult>(ActivityAddressMapping);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetActivityAddressMappingByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetActivityAddressMappingByIdQueryResult>> Handle(GetActivityAddressMappingByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
