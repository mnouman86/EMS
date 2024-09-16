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

namespace CleanArc.Application.Features.DisabilityOption.Queries.GetDisabilityOptionById
{
    internal class GetDisabilityOptionByIdQueryHandler : IRequestHandler<GetDisabilityOptionByIdQuery, OperationResult<GetDisabilityOptionByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetDisabilityOptionByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetDisabilityOptionByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetDisabilityOptionByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetDisabilityOptionByIdQueryResult>> Handle(GetDisabilityOptionByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var DisabilityOption = await _unitOfWork.DisabilityOptionRepository.GetByIdAsync(request.Id);

                if (DisabilityOption == null)
                {
                    return OperationResult<GetDisabilityOptionByIdQueryResult>.NotFoundResult("DisabilityOption not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetDisabilityOptionByIdQueryResult>(DisabilityOption);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetDisabilityOptionByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetDisabilityOptionByIdQueryResult>> Handle(GetDisabilityOptionByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
