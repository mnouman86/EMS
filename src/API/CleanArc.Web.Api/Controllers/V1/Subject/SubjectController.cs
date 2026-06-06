using Asp.Versioning;
using CleanArc.Application.Features.Subject.Command.CreateSubjectCommand;
using CleanArc.Application.Features.Subject.Command.DeleteSubjectCommand;
using CleanArc.Application.Features.Subject.Command.MapSubjectsToClassCommand;
using CleanArc.Application.Features.Subject.Command.UpdateSubjectCommand;
using CleanArc.Application.Features.Subject.Queries.GetAllSubjects;
using CleanArc.Application.Features.Subject.Queries.GetSubjectById;
using CleanArc.Application.Features.Subject.Queries.GetSubjectsByClass;
using CleanArc.Domain.Common;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.Subject;

/// <summary>
/// Endpoints for the Subjects module — SUB-01..SUB-05.
/// Inherits generic CRUD from the base controller and adds two custom endpoints:
///   - SubjectMapToClass (SUB-05)
///   - SubjectGetByClass (helper to render the mapping screen)
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Subject")]
public class SubjectController : _BaseController<
    CreateSubjectCommand,
    UpdateSubjectCommand,
    DeleteSubjectCommand,
    ResponseEntity,
    GetAllSubjectsQuery,
    List<GetAllSubjectsQueryResult>,
    GetSubjectByIdQuery,
    GetSubjectByIdQueryResult>
{
    private readonly ISender _sender;

    public SubjectController(
        ISender sender,
        ILogger<_BaseController<
            CreateSubjectCommand,
            UpdateSubjectCommand,
            DeleteSubjectCommand,
            ResponseEntity,
            GetAllSubjectsQuery,
            List<GetAllSubjectsQueryResult>,
            GetSubjectByIdQuery,
            GetSubjectByIdQueryResult>> logger,
        IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {
        _sender = sender;
    }

    /// <summary>
    /// SUB-05: Replace the subjects mapped to a class. Pass an empty list to clear all mappings.
    /// </summary>
    [Authorize]
    [HttpPost("SubjectMapToClass")]
    public async Task<IActionResult> MapToClass([FromBody] MapSubjectsToClassCommand command)
    {
        SetUserId(command);
        var result = await _sender.Send(command);
        return OperationResult(result);
    }

    /// <summary>
    /// SUB-05 helper: returns the subjects currently mapped to a given class.
    /// </summary>
    [HttpPost("SubjectGetByClass")]
    public async Task<IActionResult> GetByClass([FromBody] GetSubjectsByClassQuery query)
    {
        var result = await _sender.Send(query);
        return OperationResult(result);
    }
}
