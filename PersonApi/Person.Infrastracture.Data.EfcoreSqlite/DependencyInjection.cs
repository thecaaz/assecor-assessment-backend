using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Person.Core.Application.Abstractions;
using Person.Infrastructure.Data.CSV;

namespace Person.Infrastracture.Data.EfcoreSqlite;

public static class DependencyInjection
{
    public static void AddSqlitePersonPersistence(this IServiceCollection services)
    {
        services.AddSqlite<PersonContext>($"Data Source={PersonContext.DbName}");
        services.AddTransient<IRepository<Core.Domain.Person>, EfcorePersonRepository>();

        var context = new PersonContext();
        context.Database.Migrate();
    }
}
