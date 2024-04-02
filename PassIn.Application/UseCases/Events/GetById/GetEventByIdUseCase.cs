using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.GetById;

public class GetEventByIdUseCase
{
    public ResponseEventJson Execute(Guid id)
    {
        var dbContext = new PassInDbContext();

        var entity = dbContext.Events.FirstOrDefault(ev => ev.Id == id);

        if (entity is null)
            throw new PassInException("An event with this id dont exist");

        return new ResponseEventJson
        {
            Id = entity.Id,
            Details = entity.Details,
            MaximumAttendees = entity.Maximum_Attendees,
            Title = entity.Title
        };


    }
}
