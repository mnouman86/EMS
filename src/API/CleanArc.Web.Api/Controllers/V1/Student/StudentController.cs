using Asp.Versioning;
using CleanArc.Application.Features.Student.Command.BulkImportStudentsCommand;
using CleanArc.Application.Features.Student.Command.ChangeStudentStatusCommand;
using CleanArc.Application.Features.Student.Command.DeleteStudentCommand;
using CleanArc.Application.Features.Student.Command.PromoteStudentsCommand;
using CleanArc.Application.Features.Student.Command.SubmitAdmissionCommand;
using CleanArc.Application.Features.Student.Command.UpdateStudentCommand;
using CleanArc.Application.Features.Student.Queries.GetAllStudents;
using CleanArc.Application.Features.Student.Queries.GetStudentById;
using CleanArc.Domain.Common;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.Student;

/// <summary>
/// Endpoints for the Students (Admissions) module — STU-01..STU-09.
/// The generic Create endpoint (SubmitAdmission) is overridden to allow
/// anonymous parent submissions; all other write endpoints require auth.
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Student")]
public class StudentController : _BaseController<
    SubmitAdmissionCommand,
    UpdateStudentCommand,
    DeleteStudentCommand,
    ResponseEntity,
    GetAllStudentsQuery,
    List<GetAllStudentsQueryResult>,
    GetStudentByIdQuery,
    GetStudentByIdQueryResult>
{
    private readonly ISender _sender;

    public StudentController(
        ISender sender,
        ILogger<_BaseController<
            SubmitAdmissionCommand,
            UpdateStudentCommand,
            DeleteStudentCommand,
            ResponseEntity,
            GetAllStudentsQuery,
            List<GetAllStudentsQueryResult>,
            GetStudentByIdQuery,
            GetStudentByIdQueryResult>> logger,
        IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {
        _sender = sender;
    }

    /// <summary>
    /// STU-01: Submit admission application. Public endpoint — parents can submit
    /// without an internal login. UserId is set from the authenticated user only
    /// when one exists (admin-on-behalf flow); otherwise it stays 0.
    /// </summary>
    [AllowAnonymous]
    [HttpPost("StudentCreate")]
    public override async Task<IActionResult> Create([FromBody] SubmitAdmissionCommand command)
    {
        if (User?.Identity?.IsAuthenticated == true)
            SetUserId(command);
        var result = await _sender.Send(command);
        return OperationResult(result);
    }

    /// <summary>STU-04: Change a student's lifecycle status (triggers Student ID on Admit).</summary>
    [Authorize]
    [HttpPost("StudentChangeStatus")]
    public async Task<IActionResult> ChangeStatus([FromBody] ChangeStudentStatusCommand command)
    {
        SetUserId(command);
        var result = await _sender.Send(command);
        return OperationResult(result);
    }

    /// <summary>STU-08: Bulk import students from Excel (rows pre-parsed to JSON).</summary>
    [Authorize]
    [HttpPost("StudentBulkImport")]
    public async Task<IActionResult> BulkImport([FromBody] BulkImportStudentsCommand command)
    {
        SetUserId(command);
        var result = await _sender.Send(command);
        return OperationResult(result);
    }

    /// <summary>STU-09: Promote a set of students from one class to another (or to Alumni).</summary>
    [Authorize]
    [HttpPost("StudentPromote")]
    public async Task<IActionResult> Promote([FromBody] PromoteStudentsCommand command)
    {
        SetUserId(command);
        var result = await _sender.Send(command);
        return OperationResult(result);
    }
}
