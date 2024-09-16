using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
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

namespace CleanArc.Application.Features.Hotel.Queries.GetHotelById
{
    internal class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery, OperationResult<GetHotelByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetHotelByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetHotelByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetHotelByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetHotelByIdQueryResult>> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var hotel = await _unitOfWork.HotelRepository.GetByIdAsync(request.Id);

                if (hotel == null)
                {
                    return OperationResult<GetHotelByIdQueryResult>.NotFoundResult("URL not found");
                }

                //var result = new GetHotelByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetHotelByIdQueryResult>(hotel);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetHotelByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetHotelByIdQueryResult>> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
