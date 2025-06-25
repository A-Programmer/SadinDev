using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Users.CheckUserExistence;

public sealed class CheckUserExistenceQueryHandler : IQueryHandler<CheckUserExistenceQuery, CheckUserExistenceResponse>
{
    private readonly IKSProjectUnitOfWork _uow;

    public CheckUserExistenceQueryHandler(IKSProjectUnitOfWork uow) => _uow = uow;
    
    public async Task<CheckUserExistenceResponse> Handle(CheckUserExistenceQuery request, CancellationToken cancellationToken)
    {
        bool userByUserName = false;
        bool userByEmail = false;
        bool userByPhoneNumber = false;

        User? user = await _uow.Users.FindUserByUserNameAsync(request.Payload.UserNameOrEmailOrPhoneNumber, cancellationToken);
        if (user is not null)
            return new CheckUserExistenceResponse()
            {
                Id = user.Id
            };
         
        user = await _uow.Users.FindUserByEmailAsync(request.Payload.UserNameOrEmailOrPhoneNumber, cancellationToken);
        if (user is not null)
            return new CheckUserExistenceResponse()
            {
                Id = user.Id
            };
        
        user = await _uow.Users.FindUserByPhoneNumber(request.Payload.UserNameOrEmailOrPhoneNumber, cancellationToken);
        if (user is not null)
            return new CheckUserExistenceResponse()
            {
                Id = user.Id
            };
        

        return new CheckUserExistenceResponse()
        {
            Id = null
        };
    }
}