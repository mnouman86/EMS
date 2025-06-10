using CleanArc.Application.Common;
using CleanArc.Application.Models.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Identity
{
    public interface IEmailVerificationService
    {
        Task<IdentityResult> GenerateAndSendCodeAsync(string email);
        Task<IdentityResult> VerifyCodeAsync(string email, string code);
        //Task<Result> GenerateAndSendCodeAsync(string email);
        //Task<Result> VerifyCodeAsync(string email, string code);
    }
}
