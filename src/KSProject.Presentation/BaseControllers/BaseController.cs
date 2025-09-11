using KSFramework.KSMessaging.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace KSProject.Presentation.BaseControllers;

[ApiController]
[Route(Routes.ROOT)]
[Produces("application/json")]
public abstract class BaseController : ControllerBase
{
    protected readonly ISender Sender;
    
    protected BaseController(ISender sender)
    {
        Sender = sender;
    }
    
    protected string GetUserIp()
    {
        return Request.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()!;
    }

}