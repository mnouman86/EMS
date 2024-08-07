using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
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

namespace CleanArc.Application.Features.BusinessProfile.Command.CreateBusinessProfileCommand
{
    internal class CreateBusinessProfileCommandHnadler : IRequestHandler<CreateBusinessProfileCommand, OperationResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserManager _userManager;
        private readonly IConfiguration configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBusinessProfileCommandHnadler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
                                                                    //private readonly IUnitOfWork _unitOfWork;
                                                                    //private readonly IAppUserManager _userManager;


        public CreateBusinessProfileCommandHnadler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, IMapper mapper, ILogger<CreateBusinessProfileCommandHnadler> logger, IHttpContextAccessor httpContextAccessor)
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

        public async ValueTask<OperationResult<bool>> Handle(CreateBusinessProfileCommand request, CancellationToken cancellationToken)
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
                await _unitOfWork.BusinessProfileRepository.AddAsync(new Domain.Entities.BusinessProfile.BusinessProfile()
                { CreatedBy = user.Id,
                    BusinessTypeID = request.BusinessTypeID,
                    FullLegalName = request.FullLegalName,
                    MobileNumber = request.MobileNumber,
                    PhoneNumber = request.PhoneNumber,
                    CountryID = request.CountryID,
                    CityID = request.CityID,
                    StateID = request.StateID,
                    EmailAddress = request.EmailAddress,
                    Address1 = request.Address1,
                    Address2 = request.Address2,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    ServiceID = request.ServiceID
                   

                });
                await _unitOfWork.CommitAsync();
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
                return OperationResult<bool>.SuccessResult(true);
            }
        }
    }

}
