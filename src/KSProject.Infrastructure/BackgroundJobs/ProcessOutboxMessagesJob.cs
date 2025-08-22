using KSFramework.KSDomain;
using KSFramework.KSMessaging.Abstraction;
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

		List<OutboxMessage> messages = await _dbContext
			.Set<OutboxMessage>()
			.Where(m => m.ProcessedOnUtc == null)
			.Take(20)
			.ToListAsync(context.CancellationToken);

		_logger.LogInformation("Found {Count} unprocessed OutboxMessages", messages.Count);

		foreach (OutboxMessage outboxMessage in messages)
		{
			_logger.LogDebug("Processing OutboxMessage Id: {Id}, Content: {Content}", outboxMessage.Id, outboxMessage.Content);

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

				_logger.LogInformation("Deserialized DomainEvent: {Type}", domainEvent.GetType().Name);

				await _publisher.Publish(domainEvent, context.CancellationToken);

				outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error processing OutboxMessage Id: {Id}", outboxMessage.Id);
				continue;
			}
		}

		await _dbContext.SaveChangesAsync();
	}
}
