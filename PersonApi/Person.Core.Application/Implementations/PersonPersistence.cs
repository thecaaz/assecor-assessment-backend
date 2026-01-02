using Person.Core.Application.Abstractions;
using Person.Core.Domain;

namespace Person.Infrastructure.Data.CSV;

public class PersonPersistence : IPersonPersistence
{
    private readonly IRepository<Core.Domain.Person> _repository;

    public PersonPersistence(IRepository<Core.Domain.Person> repository)
    {
        _repository = repository;
    }

    public async Task<int> AddAsync(Core.Domain.Person person)
    {
        return await _repository.InsertAsync(person);
    }

    public async Task<IEnumerable<Core.Domain.Person>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IEnumerable<Core.Domain.Person>> GetByColorAsync(Color color)
    {
        var people = await _repository.GetAllAsync();
        return people.Where(x => x.Color == color);
    }

    public async Task<Core.Domain.Person> GetByIdAsync(int id)
    {
        var person = await _repository.GetByIdOrDefaultAsync(id);

        if (person == null)
        {
            throw new KeyNotFoundException($"Person with Id '{id}' not found.");
        }

        return person!;
    }
}
