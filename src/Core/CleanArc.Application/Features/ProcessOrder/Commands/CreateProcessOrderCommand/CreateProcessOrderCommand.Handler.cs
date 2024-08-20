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

namespace CleanArc.Application.Features.ProcessOrder.Commands.CreateProcessOrderCommand;

internal class CreateProcessOrderCommandHandler: IRequestHandler<CreateProcessOrderCommand, OperationResult<bool>>
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

    public async ValueTask<OperationResult<bool>> Handle(CreateProcessOrderCommand request, CancellationToken cancellationToken)
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
            await _unitOfWork.ProcessOrderRepository.AddAsync(new Domain.Entities.ProcessOrder.ProcessOrder()
            { CreatedBy = user.Id,
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
                CountryID = request.CountryID,
                ZipCode = request.ZipCode,
                CategoryID = request.CategoryID,
                ServiceID = request.ServiceID,
                SubServiceID = request.SubServiceID,
                Amount = request.Amount,
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                NoOfAdults = request.NoOfAdults,
                NoOfChildrens = request.NoOfChildrens,
                NoOfRooms = request.NoOfRooms,
                OrderStatus = request.OrderStatus,
                CreditDate = request.CreditDate,
                
            });
            await _unitOfWork.CommitAsync();
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
