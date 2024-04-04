using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.GetAll;

public class GetAllUserCase
{

    private PassInDbContext _dbContext;

    public GetAllUserCase()
    {
        _dbContext = new PassInDbContext();
    }
    public ResponseAllEvents Execute()
    {
        var eventos = _dbContext.Events.Select(evento => new ResponseEventJson
        {
            Id = evento.Id,
            Title = evento.Title,
            Details = evento.Details,
            MaximumAttendees = evento.Maximum_Attendees
        }).ToList();

        return new ResponseAllEvents
        {
            Events = eventos
        };
    }
}
