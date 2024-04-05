using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System.Net.Mail;

namespace PassIn.Application.UseCases.Attendees.Login;

public class LoginUseCase
{
    private readonly PassInDbContext _dbContext;

    public LoginUseCase()
    {
        _dbContext = new PassInDbContext();
    }
    public async Task<ResponseAttendeeJson> Execute(string email,string password)
    {

        Validate(email, password);
        var login = await _dbContext.Attendee
            .Where(attendee => attendee.Email == email && attendee.Password == password)
            .FirstOrDefaultAsync();

        if (login == null)
            throw new UnauthorizedException("Access denied!");



        return new ResponseAttendeeJson
        {
            Id = login.Id
        };
    }

    private void Validate(string email,string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ErrorOnValidadeException("Password is invalid!");

        if (!MailIsValid(email))
            throw new ErrorOnValidadeException("Email is invalid!");
    }

    public bool MailIsValid(string email)
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
