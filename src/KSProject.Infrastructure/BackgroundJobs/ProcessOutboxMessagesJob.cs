using KSFramework.KSDomain;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Users.Events;
using KSProject.Infrastructure.Data;
using KSProject.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

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

		//// Test manual event
		var testEvent = new UserUpdatedDomainEvent
		{
			Id = Guid.NewGuid(),
			Email = "test@example.com",
			OccurredOn = DateTime.UtcNow
		};
		//_logger.LogInformation("Publishing test UserUpdatedDomainEvent");
		//await _publisher.Publish(testEvent, context.CancellationToken);

		List<OutboxMessage> messages = await _dbContext
			.Set<OutboxMessage>()
			.Where(m => m.ProcessedOnUtc == null)
			.Take(20)
			.ToListAsync(context.CancellationToken);

		_logger.LogInformation("Found {Count} unprocessed OutboxMessages", messages.Count);

		foreach (OutboxMessage outboxMessage in messages)
		{
			_logger.LogInformation("\n\n\n\n\nProcessing OutboxMessage Id: {Id}, Content: {Content}", outboxMessage.Id, outboxMessage.Content);

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

				await _publisher.Publish(domainEvent, context.CancellationToken);

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
