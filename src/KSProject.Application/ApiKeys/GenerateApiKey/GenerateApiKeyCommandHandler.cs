using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Contracts;
using KSFramework.Exceptions;

namespace KSProject.Application.ApiKeys.GenerateApiKey;

public sealed class GenerateApiKeyCommandHandler :
    ICommandHandler<GenerateApiKeyCommand, GenerateApiKeyCommandResponse>
{
    private readonly IKSProjectUnitOfWork _uow;

    public GenerateApiKeyCommandHandler(IKSProjectUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }

    public async Task<GenerateApiKeyCommandResponse> Handle(GenerateApiKeyCommand request,
        CancellationToken cancellationToken)
    {
        User user = await _uow.Users.GetUserByIdIncludingApiKeysAsync(request.Payload.UserId, cancellationToken);
        
        if (user is null)
        {
            throw new KSNotFoundException("User not found.");
        }
        
        var newApiKey = ApiKey.Create(Guid.NewGuid(), user.Id, Guid.NewGuid().ToString().Replace("-", ""), true, DateTime.UtcNow.AddMonths(6), request.Payload.Scopes);
        
        newApiKey.User = user;
        
        user.AddApiKey(newApiKey);
        // var addedApiKey = await _uow.Users.GenerateApiKeyForUserAsync(request.Payload.UserId, request.Payload.Scopes, cancellationToken);
        
        await _uow.SaveChangesAsync(cancellationToken);

        // return new(addedApiKey.Id, addedApiKey.Key, addedApiKey.Scopes, addedApiKey.ExpirationDate);
        return new(newApiKey.Id, newApiKey.Key, newApiKey.Scopes, newApiKey.ExpirationDate);
    }
}
