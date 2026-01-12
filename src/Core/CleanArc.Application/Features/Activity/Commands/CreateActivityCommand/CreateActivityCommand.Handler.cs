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
using System.Globalization;

namespace CleanArc.Application.Features.Activity.Commands.CreateActivityCommand;

internal class CreateActivityCommandHandler: IRequestHandler<CreateActivityCommand, OperationResult<ResponseEntity>>
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

    public async ValueTask<OperationResult<ResponseEntity>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            DateTime.TryParseExact(request.StartTime, TimeFormats.formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startTime);
            DateTime.TryParseExact(request.EndTime, TimeFormats.formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endTime);
            var user = await _userManager.GetUserByIdAsync(request.UserId);
            if (user == null)
                return OperationResult<ResponseEntity>.FailureResult("User Not Found");

            //await _unitOfWork.URLRepository.AddAsync(new Domain.Entities.UserManagement.URL()
            //{ CreatedBy = user.Id, Path = request.Path, Title = request.Title, Description = request.Description/*, CreatedTime=DateTime.Now*/ });

            //await _unitOfWork.CommitAsync();
            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);

            //return OperationResult<ResponseEntity>.SuccessResult(result);
           var result= await _unitOfWork.ActivityRepository.AddAsync(new Domain.Entities.Activity.Activity()
            {
                CultureId = request.CultureId,
                BusinessId = request.BusinessId,
                Title=request.Title,
                Status=request.Status,
                LanguageLookUpId = request.LanguageLookUpId,
                ServiceCategoryId = request.ServiceCategoryId,
                SubServiceCategoryId = request.SubServiceCategoryId,
                OtherSubService=request.OtherSubService,
                MinAge = request.MinAge,
                MaxAge = request.MaxAge,
                ActivityTypeLookUpId = request.ActivityTypeLookUpId,
                ActivityNatureLookUpId = request.ActivityNatureLookUpId,
              //  MinGroupSize = request.MinGroupSize,
                MaxGroupSize = request.MaxGroupSize,
                //IsPrivateActivity = request.IsPrivateActivity,
                //PrivateParticipantLookUpID = request.PrivateParticipantLookUpID,
                WhoCannotParticipate = request.WhoCannotParticipate,
                WhoCanParticipate = request.WhoCanParticipate,
                ActivitySupervisorLookUpId = request.ActivitySupervisorLookUpId,
                OtherManageActivity=request.OtherManageActivity,
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
                CurrencyLookUpId = request.CurrencyLookUpId,
               // PerPersonPrice=request.PerPersonPrice,
                CreatedBy=user.Id,
                SeasonLookUpId = request.SeasonLookUpId,
                IncludeOptionLookUpId = request.IncludeOptionLookUpId,
                DisabilityOptionLookUpId= request.DisabilityOptionLookUpId,
                EndDate = request.EndDate,
                StartDate = request.StartDate,
                StartTime = startTime,
                EndTime = endTime,
                IsFullyRefundable = request.IsFullyRefundable,
                IsPartiallyRefundable= request.IsPartiallyRefundable,
                RefundPolicy=request.RefundPolicy,
                NonRefundPolicy=request.NonRefundPolicy,
                CancellationPolicy=request.CancellationPolicy,
                // PerGroupPrice = request.PerGroupPrice,
                //SeasonID = request.SeasonID,


            });
            await _unitOfWork.CommitAsync();
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
            return OperationResult<ResponseEntity>.SuccessResult(result);
        }
    }
}
