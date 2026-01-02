namespace Person.Core.Application.Abstractions;

public interface IPersonPersistence
{
    Task<Domain.Person> GetByIdAsync(int id);
    Task<IEnumerable<Domain.Person>> GetAllAsync();
    Task<IEnumerable<Domain.Person>> GetByColorAsync(Domain.Color color);
    Task<int> AddAsync(Domain.Person person);
}
