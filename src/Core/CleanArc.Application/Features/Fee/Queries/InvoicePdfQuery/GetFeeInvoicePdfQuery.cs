using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Pdf;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.Fee.Queries.ReceiptQuery;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Fee;
using Mediator;
using Microsoft.Extensions.Configuration;

namespace CleanArc.Application.Features.Fee.Queries.InvoicePdfQuery
{
    /* FEE-02: Render a monthly invoice as PDF. School name / address / phone /
       email are taken from IConfiguration → "SchoolInfo" so the deployment can
       swap them without a code change. */
    public record GetFeeInvoicePdfQuery(int InvoiceId) : IRequest<OperationResult<FeeReceiptDownloadResult>>;

    internal class GetFeeInvoicePdfQueryHandler : IRequestHandler<GetFeeInvoicePdfQuery, OperationResult<FeeReceiptDownloadResult>>
    {
        private readonly IUnitOfWork _u;
        private readonly IPdfRenderer<FeeInvoiceModel> _pdf;
        private readonly ITeacherScopeContext _scope;
        private readonly IConfiguration _config;
        public GetFeeInvoicePdfQueryHandler(IUnitOfWork u, IPdfRenderer<FeeInvoiceModel> pdf, ITeacherScopeContext scope, IConfiguration config)
        { _u = u; _pdf = pdf; _scope = scope; _config = config; }

        public async ValueTask<OperationResult<FeeReceiptDownloadResult>> Handle(GetFeeInvoicePdfQuery r, CancellationToken ct)
        {
            var res = await _u.FeeRepository.GetInvoiceForPdfAsync(r.InvoiceId);
            if (res.Code != 200 || res.Data == null)
                return OperationResult<FeeReceiptDownloadResult>.FailureResult(res.Message ?? "Not found", res.Code == 0 ? 404 : res.Code);

            var model = res.Data;
            // Pull school header from config; keep any hard-coded defaults as fallbacks
            // so a fresh deployment still renders something useful before it's configured.
            var schoolName = _config["SchoolInfo:Name"];
            if (!string.IsNullOrWhiteSpace(schoolName)) model.SchoolName = schoolName;

            var addrParts = new List<string?>
            {
                _config["SchoolInfo:Address"],
                _config["SchoolInfo:Phone"] is { Length: > 0 } phone ? $"Tel: {phone}" : null,
                _config["SchoolInfo:Email"] is { Length: > 0 } email ? email : null
            };
            var addr = string.Join(" · ", addrParts.Where(x => !string.IsNullOrWhiteSpace(x)));
            if (!string.IsNullOrWhiteSpace(addr)) model.SchoolAddress = addr;

            var bytes = _pdf.Render(model);
            var fileName = $"TSSS_Invoice_{model.InvoiceNo?.Replace('-', '_')}.pdf";
            return OperationResult<FeeReceiptDownloadResult>.SuccessResult(new FeeReceiptDownloadResult
            {
                Bytes = bytes,
                FileName = fileName
            });
        }
    }
}
