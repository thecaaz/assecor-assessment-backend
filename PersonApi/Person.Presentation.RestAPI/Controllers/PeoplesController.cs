using Microsoft.AspNetCore.Mvc;
using Person.Core.Application.Abstractions;
using Person.Presentation.RestAPI.Models;

namespace Person.Presentation.RestAPI.Controllers
{
    [ApiController]
    [Route("persons")]
    public class PeoplesController : ControllerBase
    {
        private readonly IPersonPersistence _personPersistence;

        public PeoplesController(IPersonPersistence personPersistence)
        {
            _personPersistence = personPersistence;
        }

        [HttpGet]
        public async Task<IEnumerable<GetPersonResponse>> GetAll()
        {
            var people = await _personPersistence.GetAllAsync();
            var response = people.Select(MapToGetResponse);
            return response;
        }

        [HttpGet("{id}")]
        public async Task<GetPersonResponse> GetById(int id)
        {
            var people = await _personPersistence.GetByIdAsync(id);
            var response = MapToGetResponse(people);
            return response;
        }

        [HttpGet("color/{color}")]
        public async Task<IEnumerable<GetPersonResponse>> GetByColor(string color)
        {
            var colorEnum = ParseToColor(color);

            var people = await _personPersistence.GetByColorAsync(colorEnum);
            var response = people.Select(MapToGetResponse);
            return response;
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody] CreatePersonRequest createPersonRequest)
        {
            var colorEnum = ParseToColor(createPersonRequest.Color);
            var person = new Core.Domain.Person
            {
                Name = createPersonRequest.Name,
                LastName = createPersonRequest.LastName,
                ZipCode = createPersonRequest.ZipCode,
                City = createPersonRequest.City,
                Color = colorEnum
            };
            var personId = await _personPersistence.AddAsync(person);
            return CreatedAtAction(nameof(GetById), new { id = personId }, MapToGetResponse(person));
        }

        private static GetPersonResponse MapToGetResponse(Core.Domain.Person person) =>
            new GetPersonResponse
            {
                Id = person.Id,
                Name = person.Name,
                LastName = person.LastName,
                ZipCode = person.ZipCode,
                City = person.City,
                Color = person.Color.ToString()
            };

        private static Core.Domain.Color ParseToColor(string color)
        {
            Core.Domain.Color colorEnum;
            try
            {
                colorEnum = Enum.Parse<Core.Domain.Color>(color, true);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"'{color}' is not a valid color.");
            }

            return colorEnum;
        }
    }
}
