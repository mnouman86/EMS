using CleanArc.Application.Common;
using CleanArc.Application.Models.AcademicYear;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.AcademicYear;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IAcademicYearRepository
    {
        Task<ResponseEntity> CreateAsync(CreateAcademicYearDTO dto);
        Task<ResponseEntity> UpdateAsync(UpdateAcademicYearDTO dto);
        Task<ResponseEntity> SetCurrentAsync(SetCurrentAcademicYearDTO dto);
        Task<ResponseEntity> DeleteAsync(DeleteRequest deleteRequest, int? updatedBy);
        Task<ListResponseWrapper<AcademicYear>> GetAllAsync(SearchRequest request);
        Task<SingleResponseWrapper<AcademicYear>> GetByIdAsync(SearchRequestById request);
        Task<SingleResponseWrapper<AcademicYear>> GetCurrentAsync();
    }
}
