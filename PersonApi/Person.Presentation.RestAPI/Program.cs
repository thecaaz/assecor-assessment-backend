using CoopHub.Infrastructure.Data.EntityFramework;
using Person.Core.Application;
using Person.Infrastracture.Data.EfcoreSqlite;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<PersonExceptionHandler>();
builder.Services.AddOpenApi();

builder.Services.AddPersonApplication();
//builder.Services.AddCsvPersonPersistence();
builder.Services.AddSqlitePersonPersistence();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseExceptionHandler();

app.Run();
