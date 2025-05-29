using Asp.Versioning;
using CleanArc.Application.Features.Manufacturer.Command.CreateManufacturerCommand;
using CleanArc.Application.Features.Manufacturer.Command.DeleteManufacturerCommand;
using CleanArc.Application.Features.Manufacturer.Command.UpdateManufacturerCommand;
using CleanArc.Application.Features.Manufacturer.Queries.GetAllManufacturer;
using CleanArc.Application.Features.Manufacturer.Queries.GetManufacturerById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; 
using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.Manufacturer;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Manufacturer")]
public class ManufacturerController : _BaseController<CreateManufacturerCommand, UpdateManufacturerCommand, DeleteManufacturerCommand, ResponseEntity, GetAllManufacturerQuery,
    List<GetAllManufacturerQueryResult>, GetManufacturerByIdQuery, GetManufacturerByIdQueryResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManufacturerController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public ManufacturerController(ISender sender, ILogger<_BaseController<CreateManufacturerCommand, UpdateManufacturerCommand, DeleteManufacturerCommand, ResponseEntity, GetAllManufacturerQuery,
List<GetAllManufacturerQueryResult>, GetManufacturerByIdQuery, GetManufacturerByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

