using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.User
{
    public class EmailVerificationCode
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Code { get; set; }
        public DateTime Expiration { get; set; }
        public int Attempts { get; set; }
        public int RequestCount { get; set; }
        public DateTime LastRequestTime { get; set; }
        public bool IsVerified { get; set; }
    }
}
