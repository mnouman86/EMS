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
using System.Reflection.Emit;

namespace CleanArc.Application.Features.PostPaymentStatus.Commands.CreatePostPaymentStatusCommand;

internal class CreatePostPaymentStatusCommandHandler: IRequestHandler<CreatePostPaymentStatusCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly IConfiguration configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<CreatePostPaymentStatusCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
    //private readonly IUnitOfWork _unitOfWork;
    //private readonly IAppUserManager _userManager;


    public CreatePostPaymentStatusCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, IMapper mapper, ILogger<CreatePostPaymentStatusCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
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

    public async ValueTask<OperationResult<ResponseEntity>> Handle(CreatePostPaymentStatusCommand request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {

            //var user = await _userManager.GetUserByIdAsync(request.UserId);
            //if (user == null)
            //    return OperationResult<ResponseEntity>.FailureResult("User Not Found");

            //await _unitOfWork.URLRepository.AddAsync(new Domain.Entities.UserManagement.URL()
            //{ CreatedBy = user.Id, Path = request.Path, Title = request.Title, Description = request.Description/*, CreatedTime=DateTime.Now*/ });

            //await _unitOfWork.CommitAsync();
            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);

            //return OperationResult<ResponseEntity>.SuccessResult(result);
            var result = await _unitOfWork.PostPaymentStatusRepository.AddAsync(new Domain.Entities.PostPaymentStatus.PostPaymentStatus()
            {
                BillStatus = request.BillStatus,
                Initiator = request.Initiator,
                InstrumentInstitution = request.InstrumentInstitution,
                InstrumentNumber = request.InstrumentNumber,
                InstrumentType = request.InstrumentType,
                Message = request.Message,
                PaidAmount = request.PaidAmount,
                PaymentChannel = request.PaymentChannel,
                PaymentLink = request.PaymentLink,
                ReferenceNumber = request.ReferenceNumber,
                Status = request.Status,
                TransactionDateTime = request.TransactionDateTime,
                TransactionRefId = request.TransactionRefId,

                // Keep your existing fields
                CreatedBy = request.UserId,
                CultureId = request.CultureId,
                OrderNumber = request.OrderNumber,
                PSID=request.PSID,
                ApplicableSoc = request.ApplicableSoc,
                BillerActualSettlementDateTime = request.BillerActualSettlementDateTime,
                BillerSettlementAmount = request.BillerSettlementAmount,
                BillerSettlementDate = request.BillerSettlementDate,
                BillerSettlementRefId = request.BillerSettlementRefId,
                BillerSettlementStatus = request.BillerSettlementStatus,
                BusinessCrn = request.BusinessCrn,
                Fee = request.Fee,
                FeeChargingType = request.FeeChargingType,
                Qr = request.Qr,
                SettlementInstitution = request.SettlementInstitution,
                TaxOnFee = request.TaxOnFee
            });
            await _unitOfWork.CommitAsync();
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
            return OperationResult<ResponseEntity>.SuccessResult(result);
        }
    }
}
