using KSFramework.KSMessaging.Abstraction;
using KSProject.Application.TestAggregate.CreateTestAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSProject.Presentation.Controllers;

public sealed class TestAggregatesController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    [Route(Routes.TestAggregates.CreateTestAggregate)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CreateTestAggregateResponse),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<CreateTestAggregateResponse>> TestAggregateAsync(
        [FromForm] CreateTestAggregateRequest request,
        CancellationToken cancellationToken = default)
    {
        CreateTestAggregateCommand command = new()
        {
            Title = request.Title,
            Content = request.Content
        };

        CreateTestAggregateResponse result = await Sender.Send(command,
            cancellationToken);

        return Ok(result);
    }
}