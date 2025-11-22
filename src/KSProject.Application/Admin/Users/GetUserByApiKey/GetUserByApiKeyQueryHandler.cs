using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Admin.Users.GetUserByApiKey;

public class GetUserByApiKeyQueryHandler :
    IQueryHandler<GetUserByApiKeyQuery, GetUserByApiKeyQueryResponse>
{
    private readonly IKSProjectUnitOfWork _uow;

    public GetUserByApiKeyQueryHandler(IKSProjectUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }

    public async Task<GetUserByApiKeyQueryResponse> Handle(GetUserByApiKeyQuery request,
        CancellationToken cancellationToken)
    {
        Domain.Aggregates.Users.User user = await _uow.Users.GetUserByApiKey(request.Payload.ApiKey);

        if (user is null)
        {
            throw new KSNotFoundException("User not found.");
        }

        return new GetUserByApiKeyQueryResponse(user.Id, user.UserName, user.Email, user.Active, user.SuperAdmin, user.ApiKeys.FirstOrDefault(x => x.Key == request.Payload.ApiKey).IsInternal,user.ApiKeys.FirstOrDefault(x => x.Key == request.Payload.ApiKey).Variant, user.ApiKeys.FirstOrDefault(x => x.Key == request.Payload.ApiKey).Scopes, user.ApiKeys.FirstOrDefault(x => x.Key == request.Payload.ApiKey).IsActive, user.ApiKeys.FirstOrDefault(x => x.Key == request.Payload.ApiKey).Domain);
    }
}
