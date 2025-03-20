using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
using CleanArc.Application.Models.Common;
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

namespace CleanArc.Application.Features.Amenities.Command.UpdateAmenitiesCommand;

internal class UpdateAmenityCommandHandler : IRequestHandler<UpdateAmenityCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly IConfiguration configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateAmenityCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
    //private readonly IUnitOfWork _unitOfWork;
    //private readonly IAppUserManager _userManager;


    public UpdateAmenityCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, IMapper mapper, ILogger<UpdateAmenityCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
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
    public async ValueTask<OperationResult<ResponseEntity>> Handle(UpdateAmenityCommand request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {

            var user = await _userManager.GetUserByIdAsync(request.UserId);
            if (user == null)
                return OperationResult<ResponseEntity>.FailureResult("User Not Found");

            var result = await _unitOfWork.AmenityRepository.UpdateAsync(new Domain.Entities.Amenity.Amenity()
            {
                UpdatedBy = user.Id,
                Id = request.Id,
                Description = request.Description,
                Name = request.Name,
                ServiceId = request.ServiceId,
                Icon = request.Icon,
                ServiceCategoryId = request.ServiceCategoryId,
                CultureId=request.CultureId
            });
            await _unitOfWork.CommitAsync();
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
            return OperationResult<ResponseEntity>.SuccessResult(result);
        }
    }

}

