using KSFramework.KSMessaging.Abstraction;
namespace KSProject.Application.TestAggregate.GetAllTestAggregates;

public record GetAllTestAggregatesQuery(
    GetAllTestAggregatesRequest Payload
    ) : IQuery<List<GetAllTestAggregatesResponse>>;