using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Security;
using CleanArc.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArc.Web.Api.Authorization;

/// <summary>
/// Central registry mapping protected (mutating / print / approve) endpoints to the
/// feature + action they require. Keyed by "ControllerName.ActionMethodName".
///
/// Design choice (safe + reversible):
///   - Only MUTATING endpoints are listed. Read endpoints (GetAll/GetById/dashboards/
///     reports) are intentionally NOT gated here so cross-module reference dropdowns
///     and menu-gated reads keep working; reads are controlled on the frontend (sidebar
///     + route guard) per the user story ("menu visible => can view").
///   - Endpoints not in this map, and any [AllowAnonymous] endpoint (e.g. the public
///     StudentCreate admission + parent searches), are left untouched — public pages
///     never break.
///   - Disable the whole feature by removing the single global filter registration.
/// </summary>
public static class PermissionMap
{
    public static readonly IReadOnlyDictionary<string, (string Feature, PermissionAction Action)> Rules =
        new Dictionary<string, (string, PermissionAction)>
        {
            // Academic Years
            ["AcademicYear.Create"]     = ("AcademicYears", PermissionAction.Create),
            ["AcademicYear.Update"]     = ("AcademicYears", PermissionAction.Update),
            ["AcademicYear.SetCurrent"] = ("AcademicYears", PermissionAction.Update),
            ["AcademicYear.Delete"]     = ("AcademicYears", PermissionAction.Delete),

            // Classes
            ["SchoolClass.Create"]        = ("Classes", PermissionAction.Create),
            ["SchoolClass.Update"]        = ("Classes", PermissionAction.Update),
            ["SchoolClass.Delete"]        = ("Classes", PermissionAction.Delete),
            ["SchoolClass.AssignTeacher"] = ("Classes", PermissionAction.Update),

            // Subjects
            ["Subject.Create"]     = ("Subjects", PermissionAction.Create),
            ["Subject.Update"]     = ("Subjects", PermissionAction.Update),
            ["Subject.Delete"]     = ("Subjects", PermissionAction.Delete),
            ["Subject.MapToClass"] = ("Subjects", PermissionAction.Update),

            // Employees
            ["Employee.Create"]         = ("Employees", PermissionAction.Create),
            ["Employee.Update"]         = ("Employees", PermissionAction.Update),
            ["Employee.Delete"]         = ("Employees", PermissionAction.Delete),
            ["Employee.MarkLeft"]       = ("Employees", PermissionAction.Update),
            ["Employee.AssignSubjects"] = ("Employees", PermissionAction.Update),
            ["Employee.UploadDocument"] = ("Employees", PermissionAction.Create),
            ["Employee.UpsertSalary"]   = ("Employees", PermissionAction.Update),
            ["Employee.IssueAdvance"]   = ("Employees", PermissionAction.Create),
            ["Employee.AdjustAdvance"]  = ("Employees", PermissionAction.Update),

            // Students (Create is [AllowAnonymous] => not listed)
            ["Student.Update"]       = ("Students", PermissionAction.Update),
            ["Student.Delete"]       = ("Students", PermissionAction.Delete),
            ["Student.ChangeStatus"] = ("Students", PermissionAction.Update),
            ["Student.BulkImport"]   = ("Students", PermissionAction.Create),
            ["Student.Promote"]      = ("Students", PermissionAction.Update),

            // Results (ParentSearch is [AllowAnonymous])
            ["Result.CreateSession"]  = ("Results", PermissionAction.Create),
            ["Result.ConfigureBands"] = ("Results", PermissionAction.Update),
            ["Result.EnterMarks"]     = ("Results", PermissionAction.Update),
            ["Result.Lock"]           = ("Results", PermissionAction.Approve),
            ["Result.Unlock"]         = ("Results", PermissionAction.Approve),

            // Fees (ParentSearch is [AllowAnonymous])
            ["Fee.UpsertType"]       = ("Fees", PermissionAction.Update),
            ["Fee.DeleteType"]       = ("Fees", PermissionAction.Delete),
            ["Fee.UpsertAmount"]     = ("Fees", PermissionAction.Update),
            ["Fee.ConfigCalendar"]   = ("Fees", PermissionAction.Update),
            ["Fee.Generate"]         = ("Fees", PermissionAction.Create),
            ["Fee.CancelInvoice"]    = ("Fees", PermissionAction.Delete),
            ["Fee.RecordPayment"]    = ("Fees", PermissionAction.Create),
            ["Fee.ClearCheque"]      = ("Fees", PermissionAction.Update),
            ["Fee.ApplyAdvance"]     = ("Fees", PermissionAction.Update),
            ["Fee.ReversePayment"]   = ("Fees", PermissionAction.Delete),
            ["Fee.GetReceiptPdf"]    = ("Fees", PermissionAction.Print),
            ["Fee.GrantConcession"]  = ("Fees", PermissionAction.Approve),
            ["Fee.RevokeConcession"] = ("Fees", PermissionAction.Approve),
            ["Fee.SendReminders"]    = ("Fees", PermissionAction.Update),
            ["Fee.CarryForward"]     = ("Fees", PermissionAction.Update),
            ["Fee.WriteOff"]         = ("Fees", PermissionAction.Approve),

            // Inventory
            ["Inventory.UpsertCategory"]   = ("Inventory", PermissionAction.Update),
            ["Inventory.UpsertItem"]       = ("Inventory", PermissionAction.Update),
            ["Inventory.DeleteItem"]       = ("Inventory", PermissionAction.Delete),
            ["Inventory.RecordPurchase"]   = ("Inventory", PermissionAction.Create),
            ["Inventory.IssueItems"]       = ("Inventory", PermissionAction.Create),
            ["Inventory.RecordReturn"]     = ("Inventory", PermissionAction.Create),
            ["Inventory.RecordAdjustment"] = ("Inventory", PermissionAction.Update),
            ["Inventory.SnoozeAlert"]      = ("Inventory", PermissionAction.Update),

            // Expenses
            ["Expense.UpsertCategory"]   = ("Expenses", PermissionAction.Update),
            ["Expense.DeleteCategory"]   = ("Expenses", PermissionAction.Delete),
            ["Expense.UpsertTemplate"]   = ("Expenses", PermissionAction.Update),
            ["Expense.GenerateRecurring"]= ("Expenses", PermissionAction.Create),
            ["Expense.Record"]           = ("Expenses", PermissionAction.Create),
            ["Expense.Delete"]           = ("Expenses", PermissionAction.Delete),
            ["Expense.StartPayroll"]     = ("Expenses", PermissionAction.Create),
            ["Expense.AdjustEntry"]      = ("Expenses", PermissionAction.Update),
            ["Expense.ConfirmPayroll"]   = ("Expenses", PermissionAction.Approve),
            ["Expense.SlipPdf"]          = ("Expenses", PermissionAction.Print),

            // Finance
            ["Finance.SetOpening"] = ("Finance", PermissionAction.Update),
            ["Finance.PnLPdf"]     = ("Finance", PermissionAction.Print),

            // Attendance (read endpoints intentionally left open per the menu-gated read policy)
            ["Attendance.BulkSave"] = ("Attendance", PermissionAction.Update),
        };
}

/// <summary>
/// Global authorization filter that enforces the <see cref="PermissionMap"/>. Admin
/// is a super-admin (bypass). Unmapped or [AllowAnonymous] endpoints are allowed.
/// </summary>
public sealed class PermissionAuthorizationFilter : IAsyncAuthorizationFilter
{
    private readonly IUserPermissionProvider _provider;
    public PermissionAuthorizationFilter(IUserPermissionProvider provider) { _provider = provider; }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        // Respect [AllowAnonymous] (public admission, parent searches, etc.).
        if (context.ActionDescriptor.EndpointMetadata.Any(m => m is IAllowAnonymous))
            return;

        if (context.ActionDescriptor is not ControllerActionDescriptor cad)
            return;

        if (!PermissionMap.Rules.TryGetValue($"{cad.ControllerName}.{cad.ActionName}", out var rule))
            return; // not a protected endpoint — leave to its own [Authorize]/anonymous setting

        var user = context.HttpContext.User;
        if (user?.Identity?.IsAuthenticated != true)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (user.IsInRole(Roles.Admin))
            return; // super-admin

        var idStr = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? user.Identity?.Name;
        if (!int.TryParse(idStr, out var userId))
        {
            context.Result = Forbidden();
            return;
        }

        var set = await _provider.GetForUserAsync(userId);
        if (!set.Has(rule.Feature, rule.Action))
            context.Result = Forbidden();
    }

    private static IActionResult Forbidden() =>
        new ObjectResult(new { Message = "You do not have permission to perform this action.", StatusCode = 403, IsSuccess = false })
        { StatusCode = 403 };
}
