using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure.Entities;

namespace PassIn.Infrastructure;

public class PassInDbContext : DbContext
{

    public DbSet<Events> Events { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=localhost\\SQLEXPRESS02;Database=PassInDb;UID=LUISPC\\Elton Oliveira;PWD='';Integrated Security=true;trustServerCertificate=true");
    }
}
