using Azure;
using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.Delete;
using PassIn.Application.UseCases.Events.GetAll;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Application.UseCases.Events.RegisterAttendee;
using PassIn.Application.UseCases.Events.Update;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestEventJson request)
    {

        var useCase = new RegisterEventUseCase();

        var response = useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid id)
    {

        var useCase = new GetEventByIdUseCase();

        var response = useCase.Execute(id);

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseEventJson),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status404NotFound)]

    public IActionResult GetAll()
    {
        var useCase = new GetAllUserCase();
        var response = useCase.Execute();

        return Ok(response);
    }

    [HttpPut]
    [Route("{eventId}")]
    [ProducesResponseType(typeof(ResponseEventJson),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status404NotFound)]

    public IActionResult Update([FromRoute] Guid eventId, [FromBody] ResponseEventJson request)
    {
        var useCase = new UpdateUseCase();
        var response = useCase.Execute(eventId,request);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{idEvent}")]
    [ProducesResponseType(typeof(ResponseEventJson),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult Delete([FromRoute] Guid idEvent)
    {
        var useCases = new DeleteEventUseCases();
        useCases.Execute(idEvent);

        return Ok();
    }
}

