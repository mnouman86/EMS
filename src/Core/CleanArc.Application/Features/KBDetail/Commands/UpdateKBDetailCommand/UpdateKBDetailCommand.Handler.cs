using AutoMapper;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.KBDetail.Commands.CreateKBDetailCommand;
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

namespace CleanArc.Application.Features.KBDetail.Commands.UpdateKBDetailCommand;

internal class UpdateKBDetailCommandHandler:IRequestHandler<UpdateKBDetailCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly IConfiguration configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateKBDetailCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
    //private readonly IUnitOfWork _unitOfWork;
    //private readonly IAppUserManager _userManager;


    public UpdateKBDetailCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, IMapper mapper, ILogger<UpdateKBDetailCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
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
    public async ValueTask<OperationResult<ResponseEntity>> Handle(UpdateKBDetailCommand request, CancellationToken cancellationToken)
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
            var result = await _unitOfWork.KBDetailRepository.UpdateAsync(new Domain.Entities.KBDetail.KBDetail()
            { UpdatedBy = user.Id,ID= request.ID,
                //Title = request.Title,
                Title = request.Title,
                KeyDate = request.KeyDate,
                Cost = request.Cost,
                ServiceID = request.ServiceID,
                CoreAreaLookupID = request.CoreAreaLookupID,
                //RelatedUrlLinkLookupID = request.RelatedUrlLinkLookupID,
                RelatedAreasLookupIDs = request.RelatedAreasLookupIDs,
                Access = request.Access,
                Availablity = request.Availablity,
                WhenToVisitIDs = request.WhenToVisitIDs,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                CountryLookUpID = request.CountryLookUpID,
                CityLookUpID = request.CityLookUpID,
                StatelookUpID = request.StatelookUpID,
                PostalCode = request.PostalCode,
                Latitude = request.Latitude,
                Longitude = request.Longitude,

                Status = request.Status,
                ApprovedBy = request.ApprovedBy,
                Remarks = request.Remarks,
                IsContributed = request.IsContributed,
                ApprovedDate=request.ApprovedDate,
               ExistingContentID=request.ExistingContentID,
                CultureId = request.CultureId });
            await _unitOfWork.CommitAsync();
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
            return OperationResult<ResponseEntity>.SuccessResult(result);
        }
    }

}
