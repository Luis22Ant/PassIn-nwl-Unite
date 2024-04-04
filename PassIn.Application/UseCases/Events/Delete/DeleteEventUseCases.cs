using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.Delete;

public class DeleteEventUseCases
{
    private readonly PassInDbContext _dbContext;

    public DeleteEventUseCases()
    {
        _dbContext = new PassInDbContext();
    }
    public void Execute(Guid idEvent)
    {
        var entity = _dbContext.Events.Include(e => e.Attendees).FirstOrDefault(e => e.Id == idEvent);


        if (entity == null)
            throw new NotFoundException("This event don't exist.");

        if (entity.Attendees != null && entity.Attendees.Any())
            throw new ErrorOnValidadeException("You can't delete this event because contais attendees");

        _dbContext.Events.Remove(entity);
        _dbContext.SaveChanges();
    }
}
