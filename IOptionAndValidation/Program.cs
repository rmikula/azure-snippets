using FluentValidation;
using IOption;
using IOption.Validators;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton);

// Add Option object and validate it on start !!!
// Simple way
/*
builder.Services.AddOptions<ExampleOption>()
    .BindConfiguration(ExampleOption.Section)
    .Validate(x =>
    {
        if (x.Retries is <= 0 or >= 9)
        {
            Console.Error.WriteLine($"Retries is {x.Retries} must be between 0 and 9");
            return false;
        }

        if (x.LogLevel <= LogLevel.Error)
        {
            Console.Error.WriteLine($"Loglevel is {x.LogLevel} has to be greater then {LogLevel.Error}");
            return false;
        }

        return true;
    })
    .ValidateOnStart();
*/


// Validate annotations
/*
builder.Services.AddOptions<Example2Option>()
    .BindConfiguration(Example2Option.Section)
    .ValidateDataAnnotations()
    .ValidateOnStart();
*/


// Nick Chapsas way
builder.Services.AddOptions<Example3Option>()
    .BindConfiguration(Example3Option.Section)
    .ValidateFluently()
    .ValidateOnStart();




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("hello", (IOptions<ExampleOption> option) => option.Value);

app.Run();