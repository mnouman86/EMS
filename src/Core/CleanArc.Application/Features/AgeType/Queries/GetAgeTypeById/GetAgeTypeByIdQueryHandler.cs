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

namespace CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById
{
    internal class GetAgeTypeByIdQueryHandler : IRequestHandler<GetAgeTypeByIdQuery, OperationResult<GetAgeTypeByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAgeTypeByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetAgeTypeByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAgeTypeByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetAgeTypeByIdQueryResult>> Handle(GetAgeTypeByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var ageType = await _unitOfWork.AgeTypeRepository.GetByIdAsync(request.Id);

                if (ageType == null)
                {
                    return OperationResult<GetAgeTypeByIdQueryResult>.NotFoundResult("ageType not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetAgeTypeByIdQueryResult>(ageType);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetAgeTypeByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetAgeTypeByIdQueryResult>> Handle(GetAgeTypeByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
