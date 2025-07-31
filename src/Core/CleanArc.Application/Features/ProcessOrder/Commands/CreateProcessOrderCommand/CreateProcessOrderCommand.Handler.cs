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

namespace CleanArc.Application.Features.ProcessOrder.Commands.CreateProcessOrderCommand;

internal class CreateProcessOrderCommandHandler: IRequestHandler<CreateProcessOrderCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly IConfiguration configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateProcessOrderCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
    //private readonly IUnitOfWork _unitOfWork;
    //private readonly IAppUserManager _userManager;


    public CreateProcessOrderCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, IMapper mapper, ILogger<CreateProcessOrderCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
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

    public async ValueTask<OperationResult<ResponseEntity>> Handle(CreateProcessOrderCommand request, CancellationToken cancellationToken)
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
            var result = await _unitOfWork.ProcessOrderRepository.AddAsync(new Domain.Entities.ProcessOrder.ProcessOrder()
            { CreatedBy = request.UserId,
                 CultureId = request.CultureId,
                OrderNumber = request.OrderNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                CardHolderName = request.CardHolderName,
                CardName = request.CardName,
                CardCVC = request.CardCVC,
                ExpirationMonth = request.ExpirationMonth,
                ExpirationYear = request.ExpirationYear,
                CountryLookUpId = request.CountryLookUpId,
                ZipCode = request.ZipCode,
                Amount = request.Amount,
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                NoOfAdults = request.NoOfAdults,
                NoOfChildren = request.NoOfChildren,
                NoOfRooms = request.NoOfRooms,
                OrderStatusEnumId = request.OrderStatusEnumId,
                GenericTitleId=request.GenericTitleId,
                ServiceTypeEnumId=request.ServiceTypeEnumId,
                ParticipantSize = request.ParticipantSize,
                Tax=request.Tax,
                PaymentStatus=request.PaymentStatus,
                Title=request.Title,
                SubTitle=request.SubTitle,
                City=request.City,
            });
            await _unitOfWork.CommitAsync();
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
            return OperationResult<ResponseEntity>.SuccessResult(result);
        }
    }
}
