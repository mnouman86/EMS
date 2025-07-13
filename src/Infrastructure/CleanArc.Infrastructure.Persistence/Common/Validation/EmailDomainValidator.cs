using CleanArc.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Common.Validation
{
    public class EmailDomainValidator : IEmailDomainValidator
    {
        private readonly HashSet<string> _disposableDomains;

        public EmailDomainValidator(string filePath)
        {
            if (File.Exists(filePath))
            {
                _disposableDomains = File.ReadAllLines(filePath)
                    .Select(line => line.Trim().ToLower())
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .ToHashSet();
            }
            else
            {
                _disposableDomains = new HashSet<string>();
            }
        }

        public bool IsDisposable(string email)
        {
            var domain = email.Split('@').Last().ToLower();
            return _disposableDomains.Contains(domain);
        }
    }
}
