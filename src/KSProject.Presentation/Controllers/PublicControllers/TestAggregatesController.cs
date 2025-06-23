using KSFramework.KSMessaging.Abstraction;
using KSFramework.Pagination;
using KSProject.Application.TestAggregate.CreateTestAggregate;
using KSProject.Application.TestAggregate.DeleteTestAggregate;
using KSProject.Application.TestAggregate.GetAllTestAggregates;
using KSProject.Application.TestAggregate.GetPagedTestAggregates;
using KSProject.Application.TestAggregate.GetTestAggregateById;
using KSProject.Application.TestAggregate.UpdateTestAggregate;
using KSProject.Presentation.BaseControllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSProject.Presentation.Controllers.PublicControllers;

public sealed class TestAggregatesController(ISender sender) : BaseController(sender)
{
    [HttpGet]
    [Route(Routes.TestAggregates.GetAllTestAggregates)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<GetAllTestAggregatesResponse>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<List<GetAllTestAggregatesResponse>>> GetAsync(
        [FromQuery] GetAllTestAggregatesRequest request,
        CancellationToken cancellationToken)
    {
        GetAllTestAggregatesQuery query = new GetAllTestAggregatesQuery(request);
        List<GetAllTestAggregatesResponse> result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }
    
    [HttpGet]
    [Route(Routes.TestAggregates.GetPagedTestAggregates)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PaginatedList<GetPagedTestAggregateResponse>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedList<GetPagedTestAggregateResponse>>> GetPagedAsync(
        [FromQuery] GetPagedTestAggregateRequest request,
        CancellationToken cancellationToken)
    {
        GetPagedTestAggregateQuery query = new(request);
        
        PaginatedList<GetPagedTestAggregateResponse> result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }
    
    
    [HttpGet]
    [Route(Routes.TestAggregates.GetTestAggregateById)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GetTestAggregateByIdResponse),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<GetTestAggregateByIdResponse>> GetAsync(
        [FromRoute] GetTestAggregateByIdRequest request,
        CancellationToken cancellationToken)
    {
        GetTestAggregateByIdQuery query = new GetTestAggregateByIdQuery(request);
        GetTestAggregateByIdResponse result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }
    
    [HttpPost]
    [Route(Routes.TestAggregates.CreateTestAggregate)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CreateTestAggregateResponse),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<CreateTestAggregateResponse>> PostAsync(
        [FromBody] CreateTestAggregateRequest request,
        CancellationToken cancellationToken = default)
    {
        CreateTestAggregateCommand command = new(request);

        CreateTestAggregateResponse result = await Sender.Send(command,
            cancellationToken);

        return Ok(result);
    }
    
    [HttpPut]
    [Route(Routes.TestAggregates.UpdateTestAggregate)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UpdateTestAggregateResponse),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<UpdateTestAggregateResponse>> PutAsync(
        Guid id,
        [FromBody] UpdateTestAggregateRequest request,
        CancellationToken cancellationToken = default)
    {
        if (id != request.Id)
            return BadRequest("Id is not correct");
        var commandRequest = new UpdateTestAggregateRequest()
        {
            Id = id,
            Title = request.Title,
            Content = request.Content
        };
        UpdateTestAggregateCommand command = new(commandRequest);

        UpdateTestAggregateResponse result = await Sender.Send(command,
            cancellationToken);

        return Ok(result);
    }
    
    [HttpDelete]
    [Route(Routes.TestAggregates.DeleteTestAggregate)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(DeleteTestAggregateResponse),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<DeleteTestAggregateResponse>> DeleteAsync(
        [FromBody] DeleteTestAggregateRequest request,
        CancellationToken cancellationToken = default)
    {
        DeleteTestAggregateCommand command = new(request);

        DeleteTestAggregateResponse result = await Sender.Send(command,
            cancellationToken);

        return Ok(result);
    }
}