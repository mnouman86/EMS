using CleanArc.Application.Common;
using CleanArc.Application.Models.Fee;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Fee;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IFeeRepository
    {
        /* Configuration (FEE-01) */
        Task<ResponseEntity> UpsertFeeTypeAsync(UpsertFeeTypeDTO dto);
        Task<ResponseEntity> DeleteFeeTypeAsync(DeleteRequest req, int? updatedBy);
        Task<ListResponseWrapper<FeeType>> GetFeeTypesAsync(SearchRequest req);
        Task<ResponseEntity> UpsertFeeTypeAmountAsync(UpsertFeeTypeAmountDTO dto);
        Task<ListResponseWrapper<FeeStructureRow>> GetFeeStructureForClassAsync(int schoolClassId, int academicYearId);
        Task<ResponseEntity> ConfigureFeeCalendarAsync(ConfigureFeeCalendarDTO dto);

        /* Invoices (FEE-02) */
        Task<ListResponseWrapper<InvoicePreviewRow>> GenerateMonthlyInvoicesAsync(GenerateMonthlyInvoicesDTO dto);
        Task<ResponseEntity> CancelInvoiceAsync(CancelInvoiceDTO dto);

        /* Payments (FEE-03 / FEE-08 / FEE-11) */
        Task<ResponseEntity> RecordPaymentAsync(RecordPaymentDTO dto);
        Task<ResponseEntity> ClearChequeAsync(ClearChequeDTO dto);
        Task<ResponseEntity> ApplyAdvanceAsync(ApplyAdvanceDTO dto);
        Task<ResponseEntity> ReversePaymentAsync(ReversePaymentDTO dto);

        /* Ledger & receipts (FEE-04) */
        Task<ListResponseWrapper<StudentLedgerEntry>> GetStudentLedgerAsync(int studentId, int? academicYearId);
        Task<SingleResponseWrapper<FeePayment>> GetPaymentByIdAsync(int paymentId);
        Task<ListResponseWrapper<FeePaymentAllocation>> GetPaymentAllocationsAsync(int paymentId);

        /* Concession (FEE-09) */
        Task<ResponseEntity> GrantConcessionAsync(GrantConcessionDTO dto);
        Task<ResponseEntity> RevokeConcessionAsync(RevokeConcessionDTO dto);
        Task<ListResponseWrapper<FeeConcession>> GetConcessionsAsync(int? studentId);

        /* Reminders (FEE-10) — reads pending list with contact numbers */
        Task<ListResponseWrapper<PendingFeeRow>> GetReminderTargetsAsync(string studentIdsCsv);

        /* Arrears (FEE-12) */
        Task<ResponseEntity> CarryForwardArrearsAsync(CarryForwardArrearsDTO dto);
        Task<ResponseEntity> WriteOffArrearAsync(WriteOffArrearDTO dto);
        Task<ListResponseWrapper<FeeArrearRow>> GetArrearsAsync(int? studentId, bool includeWrittenOff);

        /* Dashboards / Reports (FEE-05 / FEE-06 / FEE-07 / FEE-14) */
        Task<SingleResponseWrapper<CollectionDashboardSummary>> GetCollectionSummaryAsync(System.DateTime fromDate, System.DateTime toDate, int? classId);
        Task<ListResponseWrapper<CollectionByClassRow>> GetCollectionByClassAsync(System.DateTime fromDate, System.DateTime toDate);
        Task<ListResponseWrapper<CollectionByDayRow>> GetCollectionByDayAsync(System.DateTime fromDate, System.DateTime toDate);
        Task<ListResponseWrapper<PendingFeeRow>> GetPendingFeeListAsync(int? classId, int? minOutstanding, int? minDaysOverdue);
        Task<ListResponseWrapper<PendingFeeRow>> GetMonthlyNonSubmittedAsync(int month, int year, int? classId);
        Task<ListResponseWrapper<FeeReportAgeingRow>> GetOutstandingAgeingAsync(int? classId);

        /* Advance & Parent (FEE-08 / FEE-13) */
        Task<SingleResponseWrapper<StudentAdvanceBalance>> GetAdvanceBalanceAsync(int studentId);
        Task<SingleResponseWrapper<ParentFeeSummary>> ParentSearchAsync(ParentFeeSearchDTO dto);
    }
}
