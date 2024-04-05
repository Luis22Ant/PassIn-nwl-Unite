using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace PassIn.Application.UseCases.Attendees.Update;

public class UpdateUseCases
{
    private readonly PassInDbContext _dbContext;

    public UpdateUseCases()
    {
        _dbContext = new PassInDbContext();
    }
    public async Task<ResponseAttendeeJson> Execute(Guid attendeeId, ResponseAttendeeJson request)
    {
        var entity = await _dbContext.Attendee.FindAsync(attendeeId);

        if (entity == null)
            throw new NotFoundException("Don't exist attendee with this Id.");

        Validate(request);

        entity.Email = request.Email;
        entity.Name = request.Name;
        entity.Password = request.Password;

        _dbContext.Attendee.Update(entity);
        await _dbContext.SaveChangesAsync();
        return new ResponseAttendeeJson
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            Password = entity.Password    
        };
    }

    private void Validate(ResponseAttendeeJson request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ErrorOnValidadeException("Name is invalid!");
        }

        if (!EmailIsValid(request.Email))
        {
            throw new ErrorOnValidadeException("Email is invalid!");
        }
        if (string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ErrorOnValidadeException("Password is invalid!");
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
