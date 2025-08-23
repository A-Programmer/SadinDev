using KSFramework.KSDomain;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Infrastructure.Data;
using KSProject.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KSProject.Domain.Aggregates.Users.Events;

namespace KSProject.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly KSProjectDbContext _dbContext;
    private readonly IMediator _publisher;
    private readonly ILogger<ProcessOutboxMessagesJob> _logger;

    public ProcessOutboxMessagesJob(KSProjectDbContext dbContext, IMediator publisher, ILogger<ProcessOutboxMessagesJob> logger)
    {
        _dbContext = dbContext;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("ProcessOutboxMessagesJob started at {Time}", DateTime.UtcNow);

        if (context.CancellationToken.IsCancellationRequested)
        {
            _logger.LogWarning("Cancellation requested for ProcessOutboxMessagesJob");
            return;
        }

        List<OutboxMessage> messages = await _dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        _logger.LogInformation("Found {Count} unprocessed OutboxMessages", messages.Count);
        _logger.LogInformation("Messages: {Messages}", JsonConvert.SerializeObject(messages));

        foreach (OutboxMessage outboxMessage in messages)
        {
            _logger.LogInformation("Processing OutboxMessage Id: {Id}, Content: {Content}", outboxMessage.Id, outboxMessage.Content);

            try
            {
                IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

                if (domainEvent is null)
                {
                    _logger.LogWarning("Failed to deserialize OutboxMessage Id: {Id}", outboxMessage.Id);
                    continue;
                }

                _logger.LogInformation("Deserialized DomainEvent: {Type}, Is UserUpdatedDomainEvent: {IsCorrectType}", 
                    domainEvent.GetType().Name, domainEvent is UserUpdatedDomainEvent);

                // پیدا کردن متد Publish با امضای دقیق
                var publishMethod = typeof(IMediator).GetMethods()
                    .Where(m => m.Name == nameof(IMediator.Publish) && 
                                m.IsGenericMethod && 
                                m.GetParameters().Length == 2 &&
                                m.GetParameters()[0].ParameterType.IsGenericParameter &&
                                m.GetParameters()[1].ParameterType == typeof(CancellationToken))
                    .SingleOrDefault()
                    ?.MakeGenericMethod(domainEvent.GetType());

                if (publishMethod == null)
                {
                    _logger.LogError("Publish method not found for type: {Type}", domainEvent.GetType().Name);
                    continue;
                }

                await (Task)publishMethod.Invoke(_publisher, new object[] { domainEvent, context.CancellationToken });

                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing OutboxMessage Id: {Id}", outboxMessage.Id);
                continue;
            }
        }

        await _dbContext.SaveChangesAsync(context.CancellationToken);
    }
}