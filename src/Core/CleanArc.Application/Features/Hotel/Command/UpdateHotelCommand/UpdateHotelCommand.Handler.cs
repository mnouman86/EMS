using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;

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

            //return OperationResult<ResponseEntity>.SuccessResult(result);
            var result = await _unitOfWork.HotelRepository.UpdateAsync(new Domain.Entities.Hotel.Hotel()
            { UpdatedBy = user.Id, ID = request.ID, 
                Name = request.Name,
                CountryID = request.CountryID,
                StateID = request.StateID,
                CityID = request.CityID,
                ZipCode = request.ZipCode,
                Address1 = request.Address1,
                Address2 = request.Address2,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                MobileNumber = request.MobileNumber,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                FocalPersonName = request.FocalPersonName,
                //  Description = request.IsChain,
                IsChanelManager = request.IsChanelManager,
                IsRating = request.IsRating,
                ServiceID = request.ServiceID,
                IsChain = request.IsChain,
                CheckInFrom = request.CheckInFrom,
                CheckInTo = request.CheckInTo,
                CheckOutFrom = request.CheckOutFrom,
                CheckOutTo = request.CheckOutTo,
                //IsDeleted = request.IsDeleted,
                //IsActive = request.IsActive,
            });
            await _unitOfWork.CommitAsync();
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
            return OperationResult<ResponseEntity>.SuccessResult(result);
        }
    }

}
