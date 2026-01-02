namespace Person.Presentation.RestAPI.Models;

public class CreatePersonRequest
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
}
