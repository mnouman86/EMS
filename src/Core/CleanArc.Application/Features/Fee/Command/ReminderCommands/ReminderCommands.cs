using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Notifications;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Notification;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Fee.Command.ReminderCommands
{
    /* FEE-10: Send bulk fee reminders via WhatsApp/SMS/Both.
       Builds messages from the pending fee list and dispatches via INotificationSender. */
    public record SendFeeRemindersCommand(List<int>? StudentIds, string? Channel, string? TemplateKey, string? CustomBody)
        : IRequest<OperationResult<SendFeeRemindersResult>>, IValidatableModel<SendFeeRemindersCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<SendFeeRemindersCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<SendFeeRemindersCommand> v)
        {
            v.RuleFor(c => c.Channel).NotEmpty()
                .Must(x => x == null || new[] { "Whatsapp", "Sms", "Both" }.Contains(x))
                .WithMessage("Channel must be Whatsapp / Sms / Both");
            return v;
        }
    }

    public class SendFeeRemindersResult
    {
        public int TargetCount { get; set; }
        public int SentCount { get; set; }
        public int FailedCount { get; set; }
    }

    internal class SendFeeRemindersCommandHandler : IRequestHandler<SendFeeRemindersCommand, OperationResult<SendFeeRemindersResult>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m; private readonly INotificationSender _notifier;
        public SendFeeRemindersCommandHandler(IUnitOfWork u, IAppUserManager m, INotificationSender notifier)
        { _u = u; _m = m; _notifier = notifier; }

        public async ValueTask<OperationResult<SendFeeRemindersResult>> Handle(SendFeeRemindersCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<SendFeeRemindersResult>.FailureResult("User Not Found");

            var ids = r.StudentIds == null || r.StudentIds.Count == 0 ? string.Empty : string.Join(",", r.StudentIds);
            var targets = await _u.FeeRepository.GetReminderTargetsAsync(ids);

            var channels = r.Channel == "Both"
                ? new[] { NotificationChannel.Whatsapp, NotificationChannel.Sms }
                : new[] { r.Channel == "Sms" ? NotificationChannel.Sms : NotificationChannel.Whatsapp };

            var messages = new List<NotificationMessage>();
            foreach (var t in targets.Data)
            {
                var phone = t.ParentMobile ?? t.EmergencyContactPhone;
                if (string.IsNullOrWhiteSpace(phone)) continue;
                var body = !string.IsNullOrWhiteSpace(r.CustomBody)
                    ? r.CustomBody
                    : $"Assalamu Alaikum, this is a reminder that fee for {t.StudentFullName} ({t.ClassName}) is outstanding: Rs. {t.TotalOutstanding}. - TSSS";
                foreach (var ch in channels)
                {
                    messages.Add(new NotificationMessage
                    {
                        Channel = ch,
                        Recipient = phone,
                        Body = body,
                        TemplateKey = r.TemplateKey ?? "FeeReminder",
                        RelatedEntity = $"Student:{t.StudentId}",
                        SentBy = user.Id
                    });
                }
            }

            var sendResults = await _notifier.SendBulkAsync(messages, ct);
            var result = new SendFeeRemindersResult
            {
                TargetCount = targets.Data.Count,
                SentCount = sendResults.Count(x => x.Success),
                FailedCount = sendResults.Count(x => !x.Success)
            };
            return OperationResult<SendFeeRemindersResult>.SuccessResult(result);
        }
    }
}
