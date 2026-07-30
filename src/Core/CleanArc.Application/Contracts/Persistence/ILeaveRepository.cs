using CleanArc.Application.Common;
using CleanArc.Application.Models.Leave;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Leave;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface ILeaveRepository
    {
        /* Types */
        Task<ResponseEntity> UpsertLeaveTypeAsync(UpsertLeaveTypeDTO dto, int actorUserId);
        Task<ListResponseWrapper<LeaveType>> GetAllLeaveTypesAsync(bool activeOnly);
        Task<ResponseEntity> DeleteLeaveTypeAsync(int leaveTypeId, int actorUserId);

        /* Policy */
        Task<ResponseEntity> UpsertLeavePolicyAsync(UpsertLeavePolicyDTO dto, int actorUserId);
        Task<ListResponseWrapper<LeavePolicyRow>> GetAllLeavePoliciesAsync(int? academicYearId, int? employeeId);

        /* Routing */
        Task<ResponseEntity> UpsertLeaveRouteAsync(UpsertLeaveRouteDTO dto, int actorUserId);
        Task<ListResponseWrapper<LeaveApprovalRouteRow>> GetAllLeaveRoutesAsync();

        /* Balance */
        Task<ListResponseWrapper<LeaveBalanceRow>> GetLeaveBalanceAsync(int userId, int? academicYearId);

        /* Application */
        Task<ResponseEntity> SubmitLeaveApplicationAsync(SubmitLeaveApplicationDTO dto);
        Task<decimal> PreviewWorkingDaysAsync(DateTime startDate, DateTime endDate, bool halfDayFrom, bool halfDayTo);
        Task<ResponseEntity> DecideLeaveApplicationAsync(int leaveApplicationId, int deciderUserId, string decision, string? reason);
        Task<ResponseEntity> CancelLeaveApplicationAsync(int leaveApplicationId, int actorUserId, string? reason);
        Task<ListResponseWrapper<LeaveApplicationRow>> GetLeaveApplicationsAsync(GetLeaveApplicationsFilter filter, int callerUserId);

        /* Dashboard */
        Task<SingleResponseWrapper<LeaveDashboardBundle>> GetDashboardAsync();
    }
}
