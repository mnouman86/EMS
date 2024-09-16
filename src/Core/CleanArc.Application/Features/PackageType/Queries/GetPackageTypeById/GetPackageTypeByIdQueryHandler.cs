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

namespace CleanArc.Application.Features.PackageType.Queries.GetPackageTypeById
{
    internal class GetPackageTypeByIdQueryHandler : IRequestHandler<GetPackageTypeByIdQuery, OperationResult<GetPackageTypeByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetPackageTypeByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetPackageTypeByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetPackageTypeByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetPackageTypeByIdQueryResult>> Handle(GetPackageTypeByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var PackageType = await _unitOfWork.PackageTypeRepository.GetByIdAsync(request.Id);

                if (PackageType == null)
                {
                    return OperationResult<GetPackageTypeByIdQueryResult>.NotFoundResult("PackageType not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetPackageTypeByIdQueryResult>(PackageType);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetPackageTypeByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetPackageTypeByIdQueryResult>> Handle(GetPackageTypeByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
