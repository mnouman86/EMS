using AutoMapper;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.SearchFilterThingsToDo.Commands.CreateSearchFilterThingsToDoCommand;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediator;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;

namespace CleanArc.Application.Features.SearchFilterThingsToDo.Commands.UpdateSearchFilterThingsToDoCommand;

internal class UpdateSearchFilterThingsToDoCommandHandler:IRequestHandler<UpdateSearchFilterThingsToDoCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly IConfiguration configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateSearchFilterThingsToDoCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
    //private readonly IUnitOfWork _unitOfWork;
    //private readonly IAppUserManager _userManager;


    public UpdateSearchFilterThingsToDoCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, IMapper mapper, ILogger<UpdateSearchFilterThingsToDoCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
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
    public async ValueTask<OperationResult<ResponseEntity>> Handle(UpdateSearchFilterThingsToDoCommand request, CancellationToken cancellationToken)
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
            var result = await _unitOfWork.ActivityRepository.UpdateAsync(new Domain.Entities.Activity.Activity()
            { UpdatedBy = user.Id,Id= request.Id,
                CultureId=request.CultureId,
                Title = request.Name,
                LanguageLookUpId = request.LanguageLookUpId,
                ServiceCategoryLookUpId = request.ServiceLookUpId,
                SubServiceCategoryLookUpId = request.SubServiceLookUpId,
                MinAge = request.MinAge,
                MaxAge = request.MaxAge,
                ActivityTypeLookUpId = request.ActivityTypeLookUpId,
                ActivityNatureLookUpId = request.ActivityNatureLookUpId,
                //MinGroupSize = request.MinGroupSize,
                MaxGroupSize = request.MaxGroupSize,
                //IsPrivateActivity = request.IsPrivateActivity,
                WhoCannotParticipate = request.WhoCannotParticipate,
                WhoCanParticipate = request.WhoCanParticipate,
                ManageActivityLookUpId = request.ManageActivityLookUpId,
                Days = request.Days,
                Hours = request.Hours,
                Description = request.Description,
                //AddressID = request.AddressID,
                IsTransportation = request.IsTransportation,
                TransportationLookUpId = request.TransportationLookUpId,
                //ActivityIncludeID = request.ActivityIncludeID,
                IsDisability = request.IsDisability,
                //DisabilitiesID = request.DisabilitiesID,
                NotAllowedItems = request.NotAllowedItems,
                AllowedItems = request.AllowedItems,
                CurrencyLookUpID = request.CurrencyLookUpID,
                SeasonLookUpID = request.SeasonLookUpId,
                //PerGroupPrice = request.PerGroupPrice,
                //PerPersonPrice = request.PerPersonPrice,
                OtherManageActivity = request.OtherManageActivity,
                OtherSubService = request.OtherSubService,
                IncludeOptionLookUpId = request.IncludeOptionLookUpId,
                EndDate = request.EndDate,
                StartDate = request.StartDate,
                //StartTime = request.StartTime,
                //EndTime = request.EndTime,
                DisabilityOptionLookUpId = request.DisabilityOptionLookUpId,
            });
            await _unitOfWork.CommitAsync();
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
            return OperationResult<ResponseEntity>.SuccessResult(result);
        }
    }

}
