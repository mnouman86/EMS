using Asp.Versioning;
using CleanArc.Application.Features.Bank.Command.CreateBankCommand;
using CleanArc.Application.Features.Bank.Command.DeleteBankCommand;
using CleanArc.Application.Features.Bank.Command.UpdateBankCommand;
using CleanArc.Application.Features.Bank.Queries.GetAllBank;
using CleanArc.Application.Features.Bank.Queries.GetBankById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.Bank
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/BankController")]
    public class BankController:_BaseController<CreateBankCommand, UpdateBankCommand, DeleteBankCommand, bool, GetAllBankQuery,
    List<GetAllBankQueryResult>, GetBankByIdQuery, GetBankByIdQueryResult>
    {
       
        public BankController(ISender sender, ILogger<_BaseController<CreateBankCommand, UpdateBankCommand, DeleteBankCommand, bool, GetAllBankQuery,
   List<GetAllBankQueryResult>, GetBankByIdQuery, GetBankByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}
}


