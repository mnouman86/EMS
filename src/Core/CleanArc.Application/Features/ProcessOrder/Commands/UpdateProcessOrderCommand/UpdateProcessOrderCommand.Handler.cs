using AutoMapper;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.ProcessOrder.Commands.CreateProcessOrderCommand;
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

namespace CleanArc.Application.Features.ProcessOrder.Commands.UpdateProcessOrderCommand;

internal class UpdateProcessOrderCommandHandler:IRequestHandler<UpdateProcessOrderCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly IConfiguration configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateProcessOrderCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
    //private readonly IUnitOfWork _unitOfWork;
    //private readonly IAppUserManager _userManager;


    public UpdateProcessOrderCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, IMapper mapper, ILogger<UpdateProcessOrderCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
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
    public async ValueTask<OperationResult<ResponseEntity>> Handle(UpdateProcessOrderCommand request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {

            

            //await _unitOfWork.URLRepository.AddAsync(new Domain.Entities.UserManagement.URL()
            //{ CreatedBy = user.Id, Path = request.Path, Title = request.Title, Description = request.Description/*, CreatedTime=DateTime.Now*/ });

            //await _unitOfWork.CommitAsync();
            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);

            //return OperationResult<ResponseEntity>.SuccessResult(result);
            var result = await _unitOfWork.ProcessOrderRepository.UpdateAsync(new Domain.Entities.ProcessOrder.ProcessOrder()
            { UpdatedBy = request.UserId,
                Id= request.Id,
                CultureId = request.CultureId,
                OrderStatusEnumId = request.OrderStatusEnumId
            });
            await _unitOfWork.CommitAsync();
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
            return OperationResult<ResponseEntity>.SuccessResult(result);
        }
    }

}
