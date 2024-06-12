using Asp.Versioning;
using CleanArc.Application.Features.BusinessBankAccount.Command.BusinessBankAccountCommand;
using CleanArc.Application.Features.BusinessBankAccount.Command.DeleteBusinessBankAccountCommand;
using CleanArc.Application.Features.BusinessBankAccount.Command.UpdateBusinessBankAccountCommand;
using CleanArc.Application.Features.BusinessBankAccount.Query.GetAllBusinessBankAccount;
using CleanArc.Application.Features.BusinessBankAccount.Query.GetBusinessBankAccountById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.BusinessBankAccount
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/BusinessBankAccount")]
    public class BusinessBankAccountController : _BaseController<CreateBusinessBankAccountCommand, UpdateBusinessBankAccountCommand, DeleteBusinessBankAccountCommand, bool, GetAllBusinessBankAccountQuery,
    List<GetAllBusinessBankAccountQueryResult>, GetBusinessBankAccountByIdQuery, GetBusinessBankAccountByIdQueryResult>
    {
        
        public BusinessBankAccountController(ISender sender, ILogger<_BaseController<CreateBusinessBankAccountCommand, UpdateBusinessBankAccountCommand, DeleteBusinessBankAccountCommand, bool, GetAllBusinessBankAccountQuery,
   List<GetAllBusinessBankAccountQueryResult>, GetBusinessBankAccountByIdQuery, GetBusinessBankAccountByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}

