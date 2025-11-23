using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace KSProject.Application.Admin.ApiKeys.GenerateApiKey;

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
        Domain.Aggregates.Users.User user = await _uow.Users.GetUserByIdIncludingApiKeysAsync(request.Payload.UserId, cancellationToken);
        
        if (user is null)
        {
            throw new KSNotFoundException("User not found.");
        }
        
        var newApiKey = ApiKey.Create(Guid.NewGuid(), user.Id, Guid.NewGuid().ToString().Replace("-", ""), request.Payload.Domain, request.Payload.Variant , true, DateTime.UtcNow.AddMonths(6), request.Payload.Scopes);
        
        newApiKey.User = user;
        
        user.AddApiKey(newApiKey);
        _uow.ChangeEntityState(newApiKey, EntityState.Added);
        
        await _uow.SaveChangesAsync(cancellationToken);

        return new(newApiKey.Id, newApiKey.Key, newApiKey.Scopes, newApiKey.ExpirationDate);
    }
}
