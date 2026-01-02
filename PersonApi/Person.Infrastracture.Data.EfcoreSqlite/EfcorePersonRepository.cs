using Person.Core.Application.Abstractions;
using Person.Infrastracture.Data.EfcoreSqlite;

namespace Person.Infrastructure.Data.CSV;

public class EfcorePersonRepository : IRepository<Core.Domain.Person>
{
    private readonly PersonContext _personContext;

    public EfcorePersonRepository(PersonContext personContext)
    {
        _personContext = personContext;
    }

    public async Task<int> InsertAsync(Core.Domain.Person person)
    {
        _personContext.People.Add(person);
        await _personContext.SaveChangesAsync();
        return person.Id;
    }

    public Task<IEnumerable<Core.Domain.Person>> GetAllAsync()
    {
        return Task.FromResult(_personContext.People.AsEnumerable());
    }

    public Task<Core.Domain.Person?> GetByIdOrDefaultAsync(int id)
    {
        var person = _personContext.People.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(person);
    }
}
