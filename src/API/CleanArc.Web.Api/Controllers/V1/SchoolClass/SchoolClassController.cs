using Asp.Versioning;
using CleanArc.Application.Features.SchoolClass.Command.AssignClassTeacherCommand;
using CleanArc.Application.Features.SchoolClass.Command.CreateSchoolClassCommand;
using CleanArc.Application.Features.SchoolClass.Command.DeleteSchoolClassCommand;
using CleanArc.Application.Features.SchoolClass.Command.UpdateSchoolClassCommand;
using CleanArc.Application.Features.SchoolClass.Queries.GetAllSchoolClasses;
using CleanArc.Application.Features.SchoolClass.Queries.GetSchoolClassById;
using CleanArc.Domain.Common;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.SchoolClass;

/// <summary>
/// Endpoints for the Classes (Levels) module — CLS-01..CLS-05.
/// Inherits generic CRUD endpoints from the base controller
/// and adds an explicit AssignClassTeacher endpoint (CLS-05).
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/SchoolClass")]
public class SchoolClassController : _BaseController<
    CreateSchoolClassCommand,
    UpdateSchoolClassCommand,
    DeleteSchoolClassCommand,
    ResponseEntity,
    GetAllSchoolClassesQuery,
    List<GetAllSchoolClassesQueryResult>,
    GetSchoolClassByIdQuery,
    GetSchoolClassByIdQueryResult>
{
    private readonly ISender _sender;

    public SchoolClassController(
        ISender sender,
        ILogger<_BaseController<
            CreateSchoolClassCommand,
            UpdateSchoolClassCommand,
            DeleteSchoolClassCommand,
            ResponseEntity,
            GetAllSchoolClassesQuery,
            List<GetAllSchoolClassesQueryResult>,
            GetSchoolClassByIdQuery,
            GetSchoolClassByIdQueryResult>> logger,
        IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {
        _sender = sender;
    }

    /// <summary>
    /// CLS-05: Assigns (or clears) the Class Teacher for a class.
    /// Pass ClassTeacherId = null to clear the assignment.
    /// </summary>
    [Authorize]
    [HttpPost("SchoolClassAssignTeacher")]
    public async Task<IActionResult> AssignTeacher([FromBody] AssignClassTeacherCommand command)
    {
        SetUserId(command);
        var result = await _sender.Send(command);
        return OperationResult(result);
    }
}
