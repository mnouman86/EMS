using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.CarDetail.Command.CreateCarDetailCommand
{
    internal class CreateCarDetailCommandHandler : IRequestHandler<CreateCarDetailCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserManager _userManager;
        private readonly IConfiguration configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCarDetailCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
                                                                    //private readonly IUnitOfWork _unitOfWork;
                                                                    //private readonly IAppUserManager _userManager;


        public CreateCarDetailCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, IMapper mapper, ILogger<CreateCarDetailCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            this.configuration = configuration;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            //_unitOfWork = unitOfWork;
            //_userManager = userManager;
        }

        public async ValueTask<OperationResult<ResponseEntity>> Handle(CreateCarDetailCommand request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {

                var user = await _userManager.GetUserByIdAsync(request.UserId);
                if (user == null)
                    return OperationResult<ResponseEntity>.FailureResult("User Not Found");

                //await _unitOfWork.URLRepository.AddAsync(new Domain.Entities.UserManagement.URL()
                //{ CreatedBy = user.Id, Path = request.Path, Title = request.Title, Description = request.Description/*, CreatedTime=DateTime.Now*/ });

                //await _unitOfWork.CommitAsync();
                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);

                //return OperationResult<ResponseEntity>.SuccessResult(result);
                var result = await _unitOfWork.CarDetailRepository.AddAsync(new Domain.Entities.CarDetail.CarDetail()
                { 
                    CreatedBy = user.Id,
                   BusinessId = request.BusinessId,
                    Model = request.Model,
                    Year = request.Year,
                    VehicleIdentificationNumber = request.VehicleIdentificationNumber,
                    PlateNumber = request.PlateNumber,
                    NoOfSeat = request.NoOfSeat,
                    RentPrice = request.RentPrice,
                    About = request.About,
                    CultureId=request.CultureId,
                    RefundPolicy = request.RefundPolicy,
                    NonRefundPolicy = request.NonRefundPolicy,
                    CancellationPolicy = request.CancellationPolicy,
                    ServiceCategoryId = request.ServiceCategoryId,
                    ServiceTypeEnumId = request.ServiceTypeEnumId,
                    VehicleTypeLookUpId = request.VehicleTypeLookUpId,
                    DrivingAvailabilityOptionLookUpId = request.DrivingAvailabilityOptionLookUpId,
                    PerHourPrice=request.PerHourPrice,
                    LanguageLookUpId=request.LanguageLookUpId,
                    IsFullyRefundable = request.IsFullyRefundable,
                    IsPartiallyRefundable = request.IsPartiallyRefundable
                });
                await _unitOfWork.CommitAsync();
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
                return OperationResult<ResponseEntity>.SuccessResult(result);
            }
        }
    }

}
