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
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Activity.Commands.CreateActivityCommand;

internal class CreateActivityCommandHandler: IRequestHandler<CreateActivityCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly IConfiguration configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateActivityCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
    //private readonly IUnitOfWork _unitOfWork;
    //private readonly IAppUserManager _userManager;


    public CreateActivityCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, IMapper mapper, ILogger<CreateActivityCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
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

    public async ValueTask<OperationResult<bool>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
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
            await _unitOfWork.ActivityRepository.AddAsync(new Domain.Entities.Activity.Activity()
            { CreatedBy = user.Id, Description = request.Description,
                Title=request.Title,
                LanguageID = request.LanguageID,
                ServiceID = request.ServiceID,
                BusinessID = request.BusinessID,
                MinAge = request.MinAge,
                MaxAge = request.MaxAge,
                ActivityTypeID = request.ActivityTypeID,
                ActivityNatureID = request.ActivityNatureID,
                MinGroupSize = request.MinGroupSize,
                MaxGroupSize = request.MaxGroupSize,
                PrivateParticipantID = request.PrivateParticipantID,
                WhoCannotParticipate = request.WhoCannotParticipate,
                WhoCanParticipate = request.WhoCanParticipate,
                ManageActivityID = request.ManageActivityID,
                Days = request.Days,
                Hours = request.Hours,
                AddressID = request.AddressID,
                IsTransportation = request.IsTransportation,
                TransportationID = request.TransportationID,
                ActivityIncludeID = request.ActivityIncludeID,
                IsDisability = request.IsDisability,
                DisabilitiesID = request.DisabilitiesID,
                Recommendation = request.Recommendation,
                AllowedItems = request.AllowedItems,
                CurrencyID = request.CurrencyID,
                PerGroupPrice = request.PerGroupPrice,
                PerPersonPrice = request.PerPersonPrice,
                SeasonID = request.SeasonID,


            });
            await _unitOfWork.CommitAsync();
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
