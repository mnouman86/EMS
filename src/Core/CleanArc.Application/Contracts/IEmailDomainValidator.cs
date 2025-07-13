using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts
{
    public interface IEmailDomainValidator
    {
        bool IsDisposable(string email);
    }
}
