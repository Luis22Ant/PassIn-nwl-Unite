using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Attendees.GetAllByEventsId;
using PassIn.Application.UseCases.Attendees.GetById;
using PassIn.Application.UseCases.Attendees.Login;
using PassIn.Application.UseCases.Attendees.Update;
using PassIn.Application.UseCases.Events.RegisterAttendee;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttendeesController : ControllerBase
{

    [HttpPost]
    [Route("{eventId}/register")]
    [ProducesResponseType(typeof(ResponseRegisterJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromRoute] Guid eventId, [FromBody] RequestRegisterEventJson request)
    {
        var useCase = new RegisterAttendeeOnEventUseCase();

        var response = useCase.Execute(eventId, request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [Route("{eventId}")]
    [ProducesResponseType(typeof(ResponseAllAttendeesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetAll([FromRoute] Guid eventId)
    {
        var useCase = new GetAllAttendeesByEventIdUseCase();

        var response = useCase.Execute(eventId);

        return Ok(response);
    }

    [HttpGet]
    [Route("{attendeeId}/searchAttendee")]
    [ProducesResponseType(typeof(ResponseAttendeeJson),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetById([FromRoute]Guid attendeeId)
    {
        var useCase = new GetByIdUseCases();

        var response = await useCase.Execute(attendeeId);

        return Ok(response);
    }

    [HttpPut]
    [Route("{attendeeId}")]
    [ProducesResponseType(typeof(ResponseAttendeeJson),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Update([FromRoute] Guid attendeeId, ResponseAttendeeJson request)
    {
        var useCase = new UpdateUseCases();

        var response = await useCase.Execute(attendeeId, request);

        return Ok(response);
    }

    [HttpPost]
    [Route("{email},{password}")]
    [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status401Unauthorized)]

    public async Task<IActionResult> Login([FromRoute] string email, [FromRoute] string password)
    {
        var useCase = new LoginUseCase();

        var response = await useCase.Execute(email, password);


        return Ok(response.Id);

    }


}
