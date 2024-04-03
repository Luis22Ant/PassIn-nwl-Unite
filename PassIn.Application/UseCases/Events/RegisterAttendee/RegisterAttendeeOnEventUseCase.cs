using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System.Net.Mail;

namespace PassIn.Application.UseCases.Events.RegisterAttendee;

public class RegisterAttendeeOnEventUseCase
{
    private readonly PassInDbContext _dbContext;
    public RegisterAttendeeOnEventUseCase()
    {
        _dbContext = new PassInDbContext();
    }
    public ResponseRegisterJson Execute(Guid eventId, RequestRegisterEventJson request)
    {     
        Validate(eventId, request);

        var entity = new Infrastructure.Entities.Attendee
        {
            Name = request.Name,
            Email = request.Email,
            Event_Id = eventId,
            Created_At = DateTime.UtcNow
        };

        _dbContext.Attendee.Add(entity);
        _dbContext.SaveChanges();

        return new ResponseRegisterJson
        {
            Id = entity.Id
        };

    }

    private void Validate(Guid eventId, RequestRegisterEventJson request)
    {
        var eventEntity = _dbContext.Events.Find(eventId);

        if (eventEntity is null)
        {
            throw new NotFoundException("An event with this id does not exist.");
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ErrorOnValidadeException("The name is invalid");
        }
        if (!EmailIsValid(request.Email))
        {
            throw new ErrorOnValidadeException("The email is invalid");
        }

        var attendeeAlreadyRegistered = _dbContext
            .Attendee
            .Any(attendee => attendee.Email.Equals(request.Email) && attendee.Event_Id == eventId);

        if (attendeeAlreadyRegistered)
        {
            throw new ConflictException("You cant't register twice on the same event!");
        }

       var attendeesForThisEvent = _dbContext.Attendee.Count(ateendee => ateendee.Event_Id == eventId);
        if(attendeesForThisEvent == eventEntity.Maximum_Attendees )
        {
            throw new ErrorOnValidadeException("There is no room for this event!");
        }
    }

    private bool EmailIsValid(string email)
    {
        try
        {
            new MailAddress(email);

            return true;
        }
        catch
        {
            return false;
        }

    }
}
