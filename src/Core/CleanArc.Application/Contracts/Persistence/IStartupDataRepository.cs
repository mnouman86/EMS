using CleanArc.Application.Common;
using CleanArc.Application.Models.StartupData;
using CleanArc.Domain.Entities.OTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IStartupDataRepository
    {

        Task<SingleResponseWrapper<StartupDataDto>> GetStartupDataAsync();
    }
}
