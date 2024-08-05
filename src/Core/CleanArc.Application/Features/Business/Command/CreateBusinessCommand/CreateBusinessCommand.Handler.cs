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

namespace CleanArc.Application.Features.Business.Command.CreateBusinessCommand
{
    internal class CreateBusinessCommandHnadler : IRequestHandler<CreateBusinessCommand, OperationResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserManager _userManager;
        private readonly IConfiguration configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBusinessCommandHnadler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
                                                                    //private readonly IUnitOfWork _unitOfWork;
                                                                    //private readonly IAppUserManager _userManager;


        public CreateBusinessCommandHnadler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, IMapper mapper, ILogger<CreateBusinessCommandHnadler> logger, IHttpContextAccessor httpContextAccessor)
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

        public async ValueTask<OperationResult<bool>> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
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
                await _unitOfWork.BusinessRepository.AddAsync(new Domain.Entities.Business.Business()
                { CreatedBy = user.Id,
                  BusinessTypeID=request.BusinessTypeID,
                   Name = request.Name,
                   Address1 = request.Address1,
                   Address2 = request.Address2,
                   Email = request.Email,
                   PhoneNumber = request.PhoneNumber,
                   MobileNumber = request.MobileNumber,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude, 
                   CountryID = request.CountryID,
                   StateID = request.StateID,
                   CityID = request.CityID,
                   TaxIdentificationNumber = request.TaxIdentificationNumber,
                  License= request.License,
                    ProofOfInsurance= request.ProofOfInsurance,
                    //BankAccountDetailID= request.BankAccountDetailID,
                    IsCancelation= request.IsCancelation,
                    IsRefundable= request.IsRefundable,
                    
                });
                await _unitOfWork.CommitAsync();
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
                return OperationResult<bool>.SuccessResult(true);
            }
        }
    }

}
