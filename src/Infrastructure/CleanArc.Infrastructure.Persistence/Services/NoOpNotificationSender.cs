using CleanArc.Application.Contracts.Notifications;
using CleanArc.Domain.Entities.Notification;
using CleanArc.Infrastructure.Sql.SqlQueries;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CleanArc.Infrastructure.Persistence.Services
{
    /// <summary>
    /// Default <see cref="INotificationSender"/>. Persists a
    /// <see cref="NotificationLog"/> row and logs via ILogger, but does NOT
    /// actually dispatch to any provider. Swap out for Twilio / SMTP / WhatsApp
    /// implementations later by re-registering this interface in DI.
    /// </summary>
    public class NoOpNotificationSender : INotificationSender
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<NoOpNotificationSender> _logger;

        public NoOpNotificationSender(IConfiguration configuration, ILogger<NoOpNotificationSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<NotificationSendResult> SendAsync(NotificationMessage message, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("[Notification no-op] channel={Channel} to={Recipient} tpl={Template}",
                message.Channel, message.Recipient, message.TemplateKey);

            using IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DBConnection1"));
            conn.Open();
            var id = await conn.ExecuteScalarAsync<long>(
                NotificationQueries.Log_Notification,
                new
                {
                    Channel = (int)message.Channel,
                    message.Recipient,
                    message.Subject,
                    message.Body,
                    message.TemplateKey,
                    message.RelatedEntity,
                    Status = (int)NotificationStatus.Sent,    // no-op treats as sent
                    FailureReason = (string)null,
                    message.SentBy
                },
                commandType: CommandType.StoredProcedure);

            return new NotificationSendResult { Success = true, LogId = id };
        }

        public async Task<IReadOnlyList<NotificationSendResult>> SendBulkAsync(IEnumerable<NotificationMessage> messages, CancellationToken cancellationToken = default)
        {
            var results = new List<NotificationSendResult>();
            foreach (var m in messages)
                results.Add(await SendAsync(m, cancellationToken));
            return results;
        }
    }
}
