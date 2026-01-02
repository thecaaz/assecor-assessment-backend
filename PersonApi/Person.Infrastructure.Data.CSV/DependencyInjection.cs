using Microsoft.Extensions.DependencyInjection;
using Person.Core.Application.Abstractions;
using Person.Infrastructure.Data.CSV;

namespace CoopHub.Infrastructure.Data.EntityFramework;

public static class DependencyInjection
{
    public static void AddCsvPersonPersistence(this IServiceCollection services)
    {
        services.AddTransient<IRepository<Person.Core.Domain.Person>, CsvPersonRepository>();
    }
}
