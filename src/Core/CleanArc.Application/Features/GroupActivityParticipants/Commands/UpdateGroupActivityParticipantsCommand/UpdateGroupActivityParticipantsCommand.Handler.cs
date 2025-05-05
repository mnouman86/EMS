using AutoMapper;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.GroupActivityParticipants.Commands.CreateGroupActivityParticipantsCommand;
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

namespace CleanArc.Application.Features.GroupActivityParticipants.Commands.UpdateGroupActivityParticipantsCommand;

internal class UpdateGroupActivityParticipantsCommandHandler:IRequestHandler<UpdateGroupActivityParticipantsCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly IConfiguration configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateGroupActivityParticipantsCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
    //private readonly IUnitOfWork _unitOfWork;
    //private readonly IAppUserManager _userManager;


    public UpdateGroupActivityParticipantsCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, IMapper mapper, ILogger<UpdateGroupActivityParticipantsCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
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
    public async ValueTask<OperationResult<ResponseEntity>> Handle(UpdateGroupActivityParticipantsCommand request, CancellationToken cancellationToken)
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
            var result = await _unitOfWork.GroupActivityParticipantsRepository.UpdateAsync(new Domain.Entities.GroupActivityParticipants.GroupActivityParticipants()
            { UpdatedBy = request.UserId,Id= request.Id,
                // GenericTitleID = request.GenericTitleID,
                GenericTitleId = request.GenericTitleId,
                MobileNumber = request.MobileNumber,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Lead = request.Lead,
                CultureId = request.CultureId,
            GroupSize=request.GroupSize,
            guid=request.guid});
            await _unitOfWork.CommitAsync();
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
            return OperationResult<ResponseEntity>.SuccessResult(result);
        }
    }

}
