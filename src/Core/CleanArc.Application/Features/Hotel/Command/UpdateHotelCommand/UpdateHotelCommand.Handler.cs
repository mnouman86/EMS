using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; 
using CleanArc.Domain.Common;
using System.Globalization;

namespace CleanArc.Application.Features.Hotel.Command.UpdateHotelCommand;

internal class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly IConfiguration configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateHotelCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
    //private readonly IUnitOfWork _unitOfWork;
    //private readonly IAppUserManager _userManager;


    public UpdateHotelCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, IMapper mapper, ILogger<UpdateHotelCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
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
    public async ValueTask<OperationResult<ResponseEntity>> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
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
            DateTime.TryParseExact(request.CheckInFrom, "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime checkInFrom);
            DateTime.TryParseExact(request.CheckInTo, "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime checkInTo);
            DateTime.TryParseExact(request.CheckOutFrom, "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime checkOutFrom);
            DateTime.TryParseExact(request.CheckOutTo, "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime checkOutTo);
            //return OperationResult<ResponseEntity>.SuccessResult(result);
            var result = await _unitOfWork.HotelRepository.UpdateAsync(new Domain.Entities.Hotel.Hotel()
            { UpdatedBy = user.Id, Id= request.Id, 
                Name = request.Name,
                CountryLookUpId = request.CountryLookUpId,
                StateLookUpId = request.StateLookUpId,
                CityLookUpId = request.CityLookUpId,
                BusinessId = request.BusinessId,
                PostalCode = request.PostalCode,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                MobileNumber = request.MobileNumber,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                FocalPersonName = request.FocalPersonName,
                //  Description = request.IsChain,
                IsChanelManager = request.IsChanelManager,
                IsRating = request.IsRating,
                ServiceId = request.ServiceId,
                ServiceCategoryId = request.ServiceCategoryId,
                IsChain = request.IsChain,
                CheckInFrom = checkInFrom,
                CheckInTo = checkInTo,
                CheckOutFrom = checkOutFrom,
                CheckOutTo = checkOutTo,
                About = request.About,
                RefundPolicy = request.RefundPolicy,
                NonRefundPolicy = request.NonRefundPolicy,
                CancellationPolicy = request.CancellationPolicy,
                CultureId=request.CultureId,
                //IsDeleted = request.IsDeleted,
                //IsActive = request.IsActive,
            });
            await _unitOfWork.CommitAsync();
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
            return OperationResult<ResponseEntity>.SuccessResult(result);
        }
    }

}
