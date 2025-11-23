using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Admin.ApiKeys.GetUserApiKeys;

public sealed class GetUserApiKeysQueryHandler :
    IQueryHandler<GetUserApiKeysQuery, GetUserApiKeysQueryResponse>
{
    private readonly IKSProjectUnitOfWork _uow;

    public GetUserApiKeysQueryHandler(IKSProjectUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }

    public async Task<GetUserApiKeysQueryResponse> Handle(GetUserApiKeysQuery request,
        CancellationToken cancellationToken)
    {
        var apiKeys = await _uow.Users.GetApiKeysByUserIdAsync(request.Payload.UserId, cancellationToken);

        var apiKeyDtos = apiKeys
            .Select(ak => new ApiKeyDto(ak.Id, ak.Key, ak.IsActive, ak.ExpirationDate, ak.Scopes))
            .ToList();

        return new GetUserApiKeysQueryResponse(apiKeyDtos);
    }
}

// DTO برای response list (اختیاری, برای ساده‌سازی)
public record ApiKeyDto(Guid Id, string Key, bool IsActive, DateTimeOffset? ExpirationDate, string Scopes);
