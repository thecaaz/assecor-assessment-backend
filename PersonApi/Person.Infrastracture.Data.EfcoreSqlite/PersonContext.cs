using Microsoft.EntityFrameworkCore;

namespace Person.Infrastracture.Data.EfcoreSqlite;

public class PersonContext : DbContext
{
    public DbSet<Core.Domain.Person> People { get; set; }

    public static string DbName = "people.db";

    public PersonContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbName}");
}
