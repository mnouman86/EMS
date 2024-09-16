using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Hotel.Command.CreateHotelCommand;

internal class CreateHotelCommandHandler:IRequestHandler<CreateHotelCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
private readonly IAppUserManager _userManager;
private readonly IConfiguration configuration;
private readonly IMapper _mapper;
private readonly ILogger<CreateHotelCommandHandler> _logger;
private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
                                                            //private readonly IUnitOfWork _unitOfWork;
                                                            //private readonly IAppUserManager _userManager;


public CreateHotelCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, IMapper mapper, ILogger<CreateHotelCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
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

public async ValueTask<OperationResult<bool>> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
{
    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
    {

        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null)
            return OperationResult<bool>.FailureResult("User Not Found");
        await _unitOfWork.HotelRepository.AddAsync(new Domain.Entities.Hotel.Hotel()
        { CreatedBy = user.Id,  
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
            IsChain = request.IsChain,
            ServiceID = request.ServiceID,
            CheckInFrom = request.CheckInFrom,
            CheckInTo = request.CheckInTo,
            CheckOutFrom = request.CheckOutFrom,
            CheckOutTo = request.CheckOutTo,
        });
        await _unitOfWork.CommitAsync();
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
        return OperationResult<bool>.SuccessResult(true);
    }
}
}
