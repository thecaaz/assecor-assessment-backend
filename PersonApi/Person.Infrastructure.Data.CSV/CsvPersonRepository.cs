using Microsoft.Extensions.Logging;
using Person.Core.Application.Abstractions;

namespace Person.Infrastructure.Data.CSV;

public class CsvPersonRepository : IRepository<Core.Domain.Person>
{
    private readonly ILogger<CsvPersonRepository> _logger;

    public const string CsvFileName = "sample-input.csv";
    public readonly string CsvFilePath;

    public CsvPersonRepository(ILogger<CsvPersonRepository> logger)
    {
        _logger = logger;
        var executingAssembly = System.Reflection.Assembly.GetExecutingAssembly().Location;
        var executingAssemblyDirectory = Path.GetDirectoryName(executingAssembly)!;
        CsvFilePath = Path.Combine(executingAssemblyDirectory, CsvFileName);
    }

    public async Task<int> InsertAsync(Core.Domain.Person person)
    {
        var people = await LoadFromCsvAsync();
        people.Add(person);
        await SaveToCsvAsync(people);
        return person.Id;
    }

    public async Task<IEnumerable<Core.Domain.Person>> GetAllAsync()
    {
        var people = await LoadFromCsvAsync();
        return people;
    }

    public async Task<Core.Domain.Person?> GetByIdOrDefaultAsync(int id)
    {
        var people = await LoadFromCsvAsync();
        var person = people.FirstOrDefault(p => p.Id == id);
        return person;
    }

    private async Task<List<Core.Domain.Person>> LoadFromCsvAsync()
    {
        var csvContent = await File.ReadAllTextAsync(CsvFilePath);
        var people = new List<Core.Domain.Person>();

        var lines = csvContent.Split(Environment.NewLine);
        var currentIndex = 0;
        foreach (var line in lines)
        {
            var person = ParseLineToPerson(line);

            if (person is null)
            {
                _logger.LogWarning("Skipping invalid line in CSV: {Line}", line);
                continue;
            }

            person.Id = ++currentIndex;
            people.Add(person);
        }
        return people;
    }

    private Core.Domain.Person? ParseLineToPerson(string line)
    {
        var values = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
        if (values.Length != 4) return null;

        var cityValue = values[2].Trim();
        var city = cityValue.Split([' '], 2);

        var person = new Core.Domain.Person
        {
            Name = values[1].Trim(),
            LastName = values[0].Trim(),
            ZipCode = city[0].Trim(),
            City = city[1].Trim(),
            Color = Enum.Parse<Core.Domain.Color>(values[3], true)
        };
        return person;
    }

    private Task SaveToCsvAsync(List<Core.Domain.Person> people)
    {
        foreach (var person in people)
        {
            if (person.Id == 0)
            {
                person.Id = people.IndexOf(person) + 1;
            }
        }

        people = people.OrderBy(p => p.Id).ToList();
        var lines = people.Select(p => $"{p.LastName}, {p.Name}, {p.ZipCode} {p.City}, {p.Color}");
        var csvContent = string.Join(Environment.NewLine, lines);
        return File.WriteAllTextAsync(CsvFilePath, csvContent);
    }
}
