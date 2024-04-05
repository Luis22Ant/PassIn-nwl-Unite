using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Events.Update;

public class UpdateUseCase
{
    private readonly PassInDbContext _dbContext;

    public UpdateUseCase()
    {
        _dbContext = new PassInDbContext();
    }
    public ResponseEventJson Execute(Guid eventId, ResponseEventJson request)
    {
        Validate(request);
   
        var entity = _dbContext.Events.Find(eventId);

        if (entity == null)
            throw new NotFoundException("The event with this Id don't exist.");

        entity.Title = request.Title;
        entity.Details = request.Details;
        entity.Maximum_Attendees = request.MaximumAttendees;
        entity.Slug = request.Title.ToLower().Replace(" ","-");

        _dbContext.SaveChanges();

        return new ResponseEventJson
        {
            Id = eventId,
            Title = entity.Title,
            Details = entity.Details,
            MaximumAttendees = entity.Maximum_Attendees
        };
    }

    private void Validate(ResponseEventJson request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ErrorOnValidadeException("Title is invalid!");
        }
        if (string.IsNullOrWhiteSpace(request.Details))
        {
            throw new ErrorOnValidadeException("Details is invalid!");
        }
        if(request.MaximumAttendees <= 0)
        {
            throw new ErrorOnValidadeException("MaximumAttendees is invalid!");
        }
    }
}
