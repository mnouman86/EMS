using AutoMapper;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.PostPaymentStatus.Commands.CreatePostPaymentStatusCommand;
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

namespace CleanArc.Application.Features.PostPaymentStatus.Commands.UpdatePostPaymentStatusCommand;

internal class UpdatePostPaymentStatusCommandHandler:IRequestHandler<UpdatePostPaymentStatusCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly IConfiguration configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdatePostPaymentStatusCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
    //private readonly IUnitOfWork _unitOfWork;
    //private readonly IAppUserManager _userManager;


    public UpdatePostPaymentStatusCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, IMapper mapper, ILogger<UpdatePostPaymentStatusCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
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
    public async ValueTask<OperationResult<ResponseEntity>> Handle(UpdatePostPaymentStatusCommand request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {

            

            //await _unitOfWork.URLRepository.AddAsync(new Domain.Entities.UserManagement.URL()
            //{ CreatedBy = user.Id, Path = request.Path, Title = request.Title, Description = request.Description/*, CreatedTime=DateTime.Now*/ });

            //await _unitOfWork.CommitAsync();
            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);

            //return OperationResult<ResponseEntity>.SuccessResult(result);
            var result = await _unitOfWork.PostPaymentStatusRepository.UpdateAsync(new Domain.Entities.PostPaymentStatus.PostPaymentStatus()
            { UpdatedBy = request.UserId,
                Id= request.Id,
                Bill_Status = request.Bill_Status,
                Initiator = request.Initiator,
                Instrument_Institution = request.Instrument_Institution,
                Instrument_Number = request.Instrument_Number,
                Instrument_Type = request.Instrument_Type,
                Message = request.Message,
                Paid_Amount = request.Paid_Amount,
                Payment_Channel = request.Payment_Channel,
                Payment_Link = request.Payment_Link,
                Reference_Number = request.Reference_Number,
                Status = request.Status,
                Transaction_Date_Time = request.Transaction_Date_Time,
                Transaction_Ref_Id = request.Transaction_Ref_Id,
                CultureId = request.CultureId,
                OrderNumber = request.OrderNumber,PSID = request.PSID,
                Applicable_Soc = request.Applicable_Soc,
                Biller_Actual_Settlement_Date_Time = request.Biller_Actual_Settlement_Date_Time,
                Biller_Settlement_Amount = request.Biller_Settlement_Amount,
                Biller_Settlement_Date = request.Biller_Settlement_Date,
                Biller_Settlement_Ref_Id = request.Biller_Settlement_Ref_Id,
                Biller_Settlement_Status = request.Biller_Settlement_Status,
                Business_Crn = request.Business_Crn,
                Fee = request.Fee,
                Fee_Charging_Type = request.Fee_Charging_Type,
                Qr = request.Qr,
                Settlement_Institution = request.Settlement_Institution,
                Tax_On_Fee = request.Tax_On_Fee
            });
            await _unitOfWork.CommitAsync();
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
            return OperationResult<ResponseEntity>.SuccessResult(result);
        }
    }

}
