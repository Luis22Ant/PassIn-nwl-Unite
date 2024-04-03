using Microsoft.IdentityModel.Tokens;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Checkins.DoCheckin;

public class DoAttendeeCheckInUseCase
{
    private readonly PassInDbContext _dbContext;


    public DoAttendeeCheckInUseCase()
    {
        _dbContext = new PassInDbContext();
    }
    public ResponseRegisterJson Execute(Guid attendeeId)
    {

        Validate(attendeeId);

        var entity = new CheckIn
        {
            Attendee_Id = attendeeId,
            Created_at = DateTime.UtcNow
        };

        _dbContext.CheckIns.Add(entity);
        _dbContext.SaveChanges();

        return new ResponseRegisterJson
        {
            Id = entity.Id
        };
 
    }

    private void Validate(Guid attendeeId)
    {
        var existAttendee = _dbContext.Attendee.Any(attendee => attendee.Id == attendeeId);

        if (!existAttendee)
            throw new NotFoundException("The attendee with this Id was not found.");

        var existCheckIn = _dbContext.CheckIns.Any(ch => ch.Attendee_Id == attendeeId);

        if(existCheckIn)
            throw new ConflictException("Participant can't do checking twice in the same event.");

    }
}
