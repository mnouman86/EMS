using AutoMapper;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.Activity.Commands.CreateActivityCommand;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediator;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;

namespace CleanArc.Application.Features.Activity.Commands.UpdateActivityCommand;

internal class UpdateActivityCommandHandler:IRequestHandler<UpdateActivityCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly IConfiguration configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateActivityCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
    //private readonly IUnitOfWork _unitOfWork;
    //private readonly IAppUserManager _userManager;


    public UpdateActivityCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, IMapper mapper, ILogger<UpdateActivityCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
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
    public async ValueTask<OperationResult<bool>> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
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
            await _unitOfWork.ActivityRepository.UpdateAsync(new Domain.Entities.Activity.Activity()
            { UpdatedBy = user.Id,ID= request.ID,
                CultureId=request.CultureId,
                Title = request.Title,
                LanguageLookUpID = request.LanguageLookUpID,
                ServiceLookUpID = request.ServiceLookUpID,
                SubServiceLookUpID = request.SubServiceLookUpID,
                MinAge = request.MinAge,
                MaxAge = request.MaxAge,
                ActivityTypeLookUpID = request.ActivityTypeLookUpID,
                ActivityNatureLookUpID = request.ActivityNatureLookUpID,
                //MinGroupSize = request.MinGroupSize,
                MaxGroupSize = request.MaxGroupSize,
                //IsPrivateActivity = request.IsPrivateActivity,
                WhoCannotParticipate = request.WhoCannotParticipate,
                WhoCanParticipate = request.WhoCanParticipate,
                ManageActivityLookUpID = request.ManageActivityLookUpID,
                Days = request.Days,
                Hours = request.Hours,
                Description = request.Description,
                //AddressID = request.AddressID,
                IsTransportation = request.IsTransportation,
                TransportationLookUpID = request.TransportationLookUpID,
                //ActivityIncludeID = request.ActivityIncludeID,
                IsDisability = request.IsDisability,
                //DisabilitiesID = request.DisabilitiesID,
                NotAllowedItems = request.NotAllowedItems,
                AllowedItems = request.AllowedItems,
                CurrencyLookUpID = request.CurrencyLookUpID,
                SeasonLookUpID = request.SeasonLookUpID,
                //PerGroupPrice = request.PerGroupPrice,
               // PerPersonPrice = request.PerPersonPrice,
                OtherManageActivity = request.OtherManageActivity,
                OtherSubService = request.OtherSubService,
                IncludeOptionLookUpID = request.IncludeOptionLookUpID,
                EndDate = request.EndDate,
                StartDate = request.StartDate,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                DisabilityOptionLookUpID = request.DisabilityOptionLookUpID,
            });
            await _unitOfWork.CommitAsync();
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
            return OperationResult<bool>.SuccessResult(true);
        }
    }

}
