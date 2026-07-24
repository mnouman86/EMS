using Asp.Versioning;
using CleanArc.Application.Features.Employee.Command.AssignTeacherSubjectsCommand;
using CleanArc.Application.Features.Employee.Command.CreateEmployeeCommand;
using CleanArc.Application.Features.Employee.Command.DeleteEmployeeCommand;
using CleanArc.Application.Features.Employee.Command.EmployeeAdvanceCommands;
using CleanArc.Application.Features.Employee.Command.MarkEmployeeLeftCommand;
using CleanArc.Application.Features.Employee.Command.UpdateEmployeeCommand;
using CleanArc.Application.Features.Employee.Command.UploadEmployeeDocumentCommand;
using CleanArc.Application.Features.Employee.Command.UpsertEmployeeSalaryCommand;
using CleanArc.Application.Features.Employee.Queries.GetAllEmployees;
using CleanArc.Application.Features.Employee.Queries.GetEmployeeAdvances;
using CleanArc.Application.Features.Employee.Queries.GetEmployeeById;
using CleanArc.Application.Features.Employee.Queries.GetEmployeeDocuments;
using CleanArc.Application.Features.Employee.Queries.GetEmployeeSalary;
using CleanArc.Application.Features.Employee.Queries.GetTeacherAssignments;
using CleanArc.Domain.Common;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.Employee;

/// <summary>
/// Endpoints for the Employees / Teachers module — EMP-01..EMP-08.
/// Inherits generic CRUD from the base controller and adds dedicated
/// endpoints for MarkLeft (EMP-07), AssignTeacherSubjects (EMP-06),
/// UploadDocument (EMP-08), plus list helpers for documents and assignments.
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Employee")]
public class EmployeeController : _BaseController<
    CreateEmployeeCommand,
    UpdateEmployeeCommand,
    DeleteEmployeeCommand,
    ResponseEntity,
    GetAllEmployeesQuery,
    List<GetAllEmployeesQueryResult>,
    GetEmployeeByIdQuery,
    GetEmployeeByIdQueryResult>
{
    private readonly ISender _sender;

    public EmployeeController(
        ISender sender,
        ILogger<_BaseController<
            CreateEmployeeCommand,
            UpdateEmployeeCommand,
            DeleteEmployeeCommand,
            ResponseEntity,
            GetAllEmployeesQuery,
            List<GetAllEmployeesQueryResult>,
            GetEmployeeByIdQuery,
            GetEmployeeByIdQueryResult>> logger,
        IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {
        _sender = sender;
    }

    /// <summary>EMP-07: Mark employee as Left (soft, captures LastWorkingDay + Reason).</summary>
    [Authorize]
    [HttpPost("EmployeeMarkLeft")]
    public async Task<IActionResult> MarkLeft([FromBody] MarkEmployeeLeftCommand command)
    {
        SetUserId(command);
        var result = await _sender.Send(command);
        return OperationResult(result);
    }

    /// <summary>EMP-06: Replace the (class, subject) pairs assigned to a teacher.</summary>
    [Authorize]
    [HttpPost("EmployeeAssignTeacherSubjects")]
    public async Task<IActionResult> AssignSubjects([FromBody] AssignTeacherSubjectsCommand command)
    {
        SetUserId(command);
        var result = await _sender.Send(command);
        return OperationResult(result);
    }

    /// <summary>EMP-06 helper: returns the assignments of a teacher.</summary>
    [HttpPost("EmployeeGetTeacherAssignments")]
    public async Task<IActionResult> GetAssignments([FromBody] GetTeacherAssignmentsQuery query)
    {
        var result = await _sender.Send(query);
        return OperationResult(result);
    }

    /// <summary>
    /// EMP-08: Persist metadata for an uploaded document (photo / CNIC / degree / etc.).
    /// File bytes themselves should be uploaded via the FileUpload module first;
    /// pass the resulting FilePath here. Setting IsPhoto=true also stamps Employee.PhotoPath.
    /// </summary>
    [Authorize]
    [HttpPost("EmployeeUploadDocument")]
    public async Task<IActionResult> UploadDocument([FromBody] UploadEmployeeDocumentCommand command)
    {
        SetUserId(command);
        var result = await _sender.Send(command);
        return OperationResult(result);
    }

    /// <summary>EMP-08 helper: list all documents (incl. photo) for an employee.</summary>
    [HttpPost("EmployeeGetDocuments")]
    public async Task<IActionResult> GetDocuments([FromBody] GetEmployeeDocumentsQuery query)
    {
        var result = await _sender.Send(query);
        return OperationResult(result);
    }

    /// <summary>
    /// Streams a previously-uploaded employee document to the caller.  The path
    /// stored in dbo.EmployeeDocument.FilePath is relative to the API's working
    /// directory (Resources/Images2/...).  We restrict downloads to files that
    /// resolve INSIDE the uploads root so a caller can't sneak in a
    /// "..\..\appsettings.json" style path.
    /// </summary>
    [Authorize, HttpGet("EmployeeDownloadDocument")]
    public IActionResult DownloadDocument([FromQuery] string path, [FromQuery] string? name = null)
    {
        if (string.IsNullOrWhiteSpace(path)) return NotFound();

        // Uploads land under CWD/Resources — restrict access to that subtree.
        var uploadsRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Resources"));
        var full = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), path.TrimStart('/', '\\')));
        if (!full.StartsWith(uploadsRoot, StringComparison.OrdinalIgnoreCase) || !System.IO.File.Exists(full))
            return NotFound();

        var bytes = System.IO.File.ReadAllBytes(full);
        // Best-effort MIME from extension; browser handles the rest for images/pdfs.
        var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(full, out var contentType)) contentType = "application/octet-stream";

        return File(bytes, contentType, name ?? Path.GetFileName(full));
    }

    /* ---------- Foundational extensions for Modules 6–9 ---------- */

    /// <summary>Upsert the employee's salary structure (versioned by EffectiveFrom).</summary>
    [Authorize, HttpPost("EmployeeUpsertSalary")]
    public async Task<IActionResult> UpsertSalary([FromBody] UpsertEmployeeSalaryCommand command)
    {
        SetUserId(command);
        var result = await _sender.Send(command);
        return OperationResult(result);
    }

    /// <summary>Current active salary structure for an employee.</summary>
    [Authorize, HttpPost("EmployeeGetCurrentSalary")]
    public async Task<IActionResult> GetCurrentSalary([FromBody] GetCurrentEmployeeSalaryQuery query) => OperationResult(await _sender.Send(query));

    /// <summary>Full salary version history for an employee.</summary>
    [Authorize, HttpPost("EmployeeGetSalaryHistory")]
    public async Task<IActionResult> GetSalaryHistory([FromBody] GetEmployeeSalaryHistoryQuery query) => OperationResult(await _sender.Send(query));

    /// <summary>Issue a salary advance (deducted on next payroll via EXP-05).</summary>
    [Authorize, HttpPost("EmployeeIssueAdvance")]
    public async Task<IActionResult> IssueAdvance([FromBody] IssueEmployeeAdvanceCommand command)
    {
        SetUserId(command);
        var result = await _sender.Send(command);
        return OperationResult(result);
    }

    /// <summary>Adjust an existing advance — positive amount = repayment, negative = increase.</summary>
    [Authorize, HttpPost("EmployeeAdjustAdvance")]
    public async Task<IActionResult> AdjustAdvance([FromBody] AdjustEmployeeAdvanceCommand command)
    {
        SetUserId(command);
        var result = await _sender.Send(command);
        return OperationResult(result);
    }

    /// <summary>List all advances for an employee (Open + Settled + WrittenOff).</summary>
    [Authorize, HttpPost("EmployeeGetAdvances")]
    public async Task<IActionResult> GetAdvances([FromBody] GetEmployeeAdvancesQuery query) => OperationResult(await _sender.Send(query));
}
