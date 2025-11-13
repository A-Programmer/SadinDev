using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Contracts;
using KSFramework.Exceptions;

namespace KSProject.Application.ApiKeys.RevokeApiKey;

public sealed class RevokeApiKeyCommandHandler :
    ICommandHandler<RevokeApiKeyCommand, RevokeApiKeyCommandResponse>
{
    private readonly IKSProjectUnitOfWork _uow;

    public RevokeApiKeyCommandHandler(IKSProjectUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }

    public async Task<RevokeApiKeyCommandResponse> Handle(RevokeApiKeyCommand request,
        CancellationToken cancellationToken)
    {
        User? user = await _uow.Users.GetUserByIdIncludingApiKeysAsync(request.Payload.UserId, cancellationToken);

        if (user is null)
        {
            throw new KSNotFoundException("User not found.");
        }

        user.RevokeApiKey(request.Payload.ApiKeyId);

        await _uow.SaveChangesAsync(cancellationToken);

        return new RevokeApiKeyCommandResponse(true);
    }
}
