using Microsoft.Extensions.DependencyInjection;
using Person.Core.Application.Abstractions;
using Person.Infrastructure.Data.CSV;

namespace Person.Core.Application;

public static class DependencyInjection
{
    public static void AddPersonApplication(this IServiceCollection services)
    {
        services.AddTransient<IPersonPersistence, PersonPersistence>();
    }
}
