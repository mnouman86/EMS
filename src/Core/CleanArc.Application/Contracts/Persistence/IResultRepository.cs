using CleanArc.Application.Common;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.Result;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Result;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IResultRepository
    {
        // RES-01
        Task<ResponseEntity> CreateSessionAsync(CreateResultSessionDTO dto);
        Task<ListResponseWrapper<ResultSession>> GetSessionsAsync(SearchRequest request);

        // RES-04
        Task<ResponseEntity> ConfigureGradeBandsAsync(ConfigureGradeBandsDTO dto);
        Task<ListResponseWrapper<GradeBand>> GetGradeBandsAsync();

        // RES-02 + RES-03
        Task<ResponseEntity> EnterMarksAsync(EnterMarksDTO dto);
        Task<ListResponseWrapper<StudentSubjectMark>> GetMarksEntryGridAsync(MarksEntryGridRequest request);

        // RES-05
        Task<ListResponseWrapper<PreLockMissing>> GetPreLockReportAsync(LockResultsDTO request);
        Task<ResponseEntity> LockClassResultsAsync(LockResultsDTO dto);
        Task<ResponseEntity> UnlockClassResultsAsync(LockResultsDTO dto);

        // RES-06
        Task<ListResponseWrapper<ClassSheetRow>> GetClassResultSheetAsync(ClassSheetRequest request);

        // RES-07
        Task<SingleResponseWrapper<StudentResultCard>> ParentSearchAsync(ParentSearchDTO dto);

        // RES-08
        Task<SingleResponseWrapper<StudentResultCard>> GetStudentResultCardAsync(StudentCardRequest request);
        Task<ListResponseWrapper<StudentSubjectMark>> GetStudentResultMarksAsync(StudentCardRequest request);
    }
}
