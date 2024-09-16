using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.URL.Commands.AddURLCommand;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Entities.User;
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

namespace CleanArc.Application.Features.SearchFilterThingsToDo.Commands.CreateSearchFilterThingsToDoCommand;

internal class CreateSearchFilterThingsToDoCommandHandler: IRequestHandler<CreateSearchFilterThingsToDoCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly IConfiguration configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSearchFilterThingsToDoCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
    //private readonly IUnitOfWork _unitOfWork;
    //private readonly IAppUserManager _userManager;


    public CreateSearchFilterThingsToDoCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, IMapper mapper, ILogger<CreateSearchFilterThingsToDoCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
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

    public async ValueTask<OperationResult<bool>> Handle(CreateSearchFilterThingsToDoCommand request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {

            var user = await _userManager.GetUserByIdAsync(request.UserId);
            if (user == null)
                return OperationResult<bool>.FailureResult("User Not Found");

            //await _unitOfWork.URLRepository.AddAsync(new Domain.Entities.UserManagement.URL()
            //{ CreatedBy = user.Id, Path = request.Path, Title = request.Title, Description = request.Description/*, CreatedTime=DateTime.Now*/ });

            //await _unitOfWork.CommitAsync();
            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);

            //return OperationResult<bool>.SuccessResult(true);
            //await _unitOfWork.SearchFilterThingsToDoRepository.AddAsync(new Domain.Entities.SearchFilterThingsToDo.SearchFilterThingsToDo()
            //{
            //    CultureId = request.CultureId,
            //    Title=request.Title,
            //    LanguageLookUpID = request.LanguageLookUpID,
            //    ServiceLookUpID = request.ServiceLookUpID,
            //    SubServiceLookUpID = request.SubServiceLookUpID,
            //    OtherSubService=request.OtherSubService,
            //    MinAge = request.MinAge,
            //    MaxAge = request.MaxAge,
            //    ActivityTypeLookUpID = request.ActivityTypeLookUpID,
            //    ActivityNatureLookUpID = request.ActivityNatureLookUpID,
            //  //  MinGroupSize = request.MinGroupSize,
            //    MaxGroupSize = request.MaxGroupSize,
            //    //IsPrivateActivity = request.IsPrivateActivity,
            //    //PrivateParticipantLookUpID = request.PrivateParticipantLookUpID,
            //    WhoCannotParticipate = request.WhoCannotParticipate,
            //    WhoCanParticipate = request.WhoCanParticipate,
            //    ManageActivityLookUpID = request.ManageActivityLookUpID,
            //    OtherManageActivity=request.OtherManageActivity,
            //    Days = request.Days,
            //    Hours = request.Hours,
            //    Description = request.Description,
            //    //AddressID = request.AddressID,
            //    IsTransportation = request.IsTransportation,
            //    TransportationLookUpID = request.TransportationLookUpID,
            //    //ActivityIncludeID = request.ActivityIncludeID,
            //    IsDisability = request.IsDisability,
            //    //DisabilitiesID = request.DisabilitiesID,
            //    NotAllowedItems = request.NotAllowedItems,
            //    AllowedItems = request.AllowedItems,
            //    CurrencyLookUpID = request.CurrencyLookUpID,
            //   // PerPersonPrice=request.PerPersonPrice,
            //    CreatedBy=user.Id,
            //    SeasonLookUpID = request.SeasonLookUpID,
            //    IncludeOptionLookUpID = request.IncludeOptionLookUpID,
            //    DisabilityOptionLookUpID= request.DisabilityOptionLookUpID,
            //    EndDate = request.EndDate,
            //    StartDate = request.StartDate,
            //    StartTime = request.StartTime,
            //    EndTime = request.EndTime
            //    // PerGroupPrice = request.PerGroupPrice,
            //    //SeasonID = request.SeasonID,


           // });
            await _unitOfWork.CommitAsync();
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
