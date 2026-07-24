using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Pdf;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Fee;
using Mediator;
using Microsoft.Extensions.Configuration;

namespace CleanArc.Application.Features.Fee.Queries.ReceiptQuery
{
    /* FEE-04: Build receipt PDF for a payment. Returns bytes + filename suggestion.
       Re-print = IsDuplicate=true → DUPLICATE watermark rendered. */
    public record GetFeeReceiptQuery(int PaymentId, bool IsDuplicate)
        : IRequest<OperationResult<FeeReceiptDownloadResult>>;

    public class FeeReceiptDownloadResult
    {
        public byte[] Bytes { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; } = "application/pdf";
    }

    internal class GetFeeReceiptQueryHandler : IRequestHandler<GetFeeReceiptQuery, OperationResult<FeeReceiptDownloadResult>>
    {
        private readonly IUnitOfWork _u;
        private readonly IPdfRenderer<FeeReceiptModel> _pdf;
        private readonly ITeacherScopeContext _scope;
        private readonly IConfiguration _config;
        public GetFeeReceiptQueryHandler(IUnitOfWork u, IPdfRenderer<FeeReceiptModel> pdf, ITeacherScopeContext scope, IConfiguration config)
        { _u = u; _pdf = pdf; _scope = scope; _config = config; }

        public async ValueTask<OperationResult<FeeReceiptDownloadResult>> Handle(GetFeeReceiptQuery r, CancellationToken ct)
        {
            var payment = await _u.FeeRepository.GetPaymentByIdAsync(r.PaymentId);
            if (payment.Code != 200 || payment.Data == null)
                return OperationResult<FeeReceiptDownloadResult>.FailureResult("Payment not found", 404);

            if (_scope.IsTeacherScoped && !await _scope.OwnsStudentAsync(payment.Data.StudentId, TeacherScopeKind.ClassTeacher))
                return OperationResult<FeeReceiptDownloadResult>.FailureResult("Not authorized for this student.", 403);

            var allocations = await _u.FeeRepository.GetPaymentAllocationsAsync(r.PaymentId);

            // Look up student for header info via repo (this stays in handler — repo only
            // exposes raw payment + allocations; richer joining is left to the SP if needed).
            var model = new FeeReceiptModel
            {
                ReceiptNo = payment.Data.ReceiptNo,
                ReceiptDate = payment.Data.PaymentDate,
                PaymentMode = payment.Data.PaymentMode,
                ReferenceNo = payment.Data.ReferenceNo,
                AmountPaid = payment.Data.TotalAmount,
                IsDuplicate = r.IsDuplicate,
                ChequeClearanceDate = payment.Data.ClearanceDate,
                // Lines + student header fields populated by the SP-returned join in
                // usp_Get_FeePaymentById (extend it to include those when needed).
                // For now we keep the model loose so the renderer doesn't crash on nulls.
                Lines = new List<FeeReceiptLine>()
            };

            // School header from configuration.
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
            var fileName = $"TSSS_Receipt_{payment.Data.ReceiptNo?.Replace('-', '_')}.pdf";
            return OperationResult<FeeReceiptDownloadResult>.SuccessResult(new FeeReceiptDownloadResult
            {
                Bytes = bytes,
                FileName = fileName
            });
        }
    }
}
