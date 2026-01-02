using Person.Core.Application.Abstractions;
using Person.Infrastructure.Data.CSV;

namespace Person.Core.Application.Tests.PersonPersistenceTests;

public partial class PersonPersistenceTests
{
    private readonly IRepository<Domain.Person> _repository;

    private readonly PersonPersistence _personPersistence;

    public PersonPersistenceTests()
    {
        _repository = NSubstitute.Substitute.For<IRepository<Domain.Person>>();

        _personPersistence = new PersonPersistence(_repository);
    }
}
