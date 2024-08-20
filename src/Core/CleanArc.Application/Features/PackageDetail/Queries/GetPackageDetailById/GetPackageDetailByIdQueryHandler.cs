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

namespace CleanArc.Application.Features.PackageDetail.Queries.GetPackageDetailById
{
    internal class GetPackageDetailByIdQueryHandler : IRequestHandler<GetPackageDetailByIdQuery, OperationResult<GetPackageDetailByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetPackageDetailByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetPackageDetailByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetPackageDetailByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetPackageDetailByIdQueryResult>> Handle(GetPackageDetailByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var PackageDetail = await _unitOfWork.PackageDetailRepository.GetByIdAsync(request.Id);

                if (PackageDetail == null)
                {
                    return OperationResult<GetPackageDetailByIdQueryResult>.NotFoundResult("PackageDetail not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetPackageDetailByIdQueryResult>(PackageDetail);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetPackageDetailByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetPackageDetailByIdQueryResult>> Handle(GetPackageDetailByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
