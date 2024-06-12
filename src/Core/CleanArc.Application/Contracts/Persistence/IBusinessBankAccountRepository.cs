using CleanArc.Domain.Entities.BusinessBankAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IBusinessBankAccountRepository:IRepository<BusinessBankAccount>
    {
    }
}
