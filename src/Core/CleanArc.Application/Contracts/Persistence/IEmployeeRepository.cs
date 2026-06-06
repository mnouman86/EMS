using CleanArc.Application.Common;
using CleanArc.Application.Models.Employee;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Employee;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<ResponseEntity> MarkAsLeftAsync(MarkEmployeeLeftDTO dto);
        Task<ResponseEntity> AssignTeacherSubjectsAsync(AssignTeacherSubjectsDTO dto);
        Task<ListResponseWrapper<TeacherAssignment>> GetTeacherAssignmentsAsync(SearchRequestById request);
        Task<ResponseEntity> AddDocumentAsync(UploadEmployeeDocumentDTO dto);
        Task<ListResponseWrapper<EmployeeDocument>> GetDocumentsAsync(SearchRequestById request);

        // Foundational extensions for Modules 6–9 (Payroll / Finance)
        Task<ResponseEntity> UpsertSalaryAsync(UpsertEmployeeSalaryDTO dto);
        Task<SingleResponseWrapper<EmployeeSalary>> GetCurrentSalaryAsync(SearchRequestById request);
        Task<ListResponseWrapper<EmployeeSalary>> GetSalaryHistoryAsync(SearchRequestById request);

        Task<ResponseEntity> IssueAdvanceAsync(IssueEmployeeAdvanceDTO dto);
        Task<ResponseEntity> AdjustAdvanceAsync(AdjustEmployeeAdvanceDTO dto);
        Task<ListResponseWrapper<EmployeeAdvance>> GetAdvancesAsync(SearchRequestById request);
    }
}
