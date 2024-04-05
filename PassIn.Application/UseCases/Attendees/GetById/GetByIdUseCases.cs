using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Attendees.GetById;

public class GetByIdUseCases
{

    private readonly PassInDbContext _dbContext;

    public GetByIdUseCases()
    {
        _dbContext = new PassInDbContext();
    }
    public async Task<ResponseAttendeeJson> Execute(Guid attendeeId)
    {
        var entity = await _dbContext.Attendee.FindAsync(attendeeId);

        if (entity == null)
            throw new NotFoundException("This attendee don't exist.");

        return new ResponseAttendeeJson
        {
            Id = entity.Id,
            Password = entity.Password,
            Email = entity.Email,
            Name = entity.Name,
            CreatedAt = entity.Created_At
        };
    }
}
