using CleanArc.Application.Common;
using CleanArc.Application.Models.StartupData;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IStartupDataRepository
    {

        Task<SingleResponseWrapper<StartupDataDto>> GetStartupDataAsync();
    }
}
