using System.Text;
using HealthCheck.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddCheck<ReadinessHealthCheck>(name: "ReadinessHealthCheckName", failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "RoMik" })
    .AddCheck<DatabaseHealthCheck>("DatabaseHealthCheckName", HealthStatus.Unhealthy, tags: new[] { "RoMik2" });

/// I can add configuration for HealthCheckMiddleware here or
/// or as parameter when register middleware. See below.
builder.Services.AddOptions<HealthCheckOptions>()
    .Configure(opt =>
    {
        opt.Predicate = prd => prd.Tags.Equals("RoMik");
        opt.AllowCachingResponses = false;
        /*
        opt.ResponseWriter = (httpContext, result) =>
        {
            httpContext.Response.ContentType = "text/plain";
            var task = httpContext.Response.Body.WriteAsync(Encoding.UTF8.GetBytes("Hello world !!!!")).AsTask();

            return task;
        };
        */
    });


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


#region Configuration HealthCheckMiddleware with parameter

app.Map(new PathString("/health/ready"),
    configuration: applicationBuilder =>
    {
        var opt = Options.Create(new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains("RoMik"),
            AllowCachingResponses = false
        });
        applicationBuilder.UseMiddleware<HealthCheckMiddleware>();
        // applicationBuilder.UseMiddleware<HealthCheckMiddleware>(opt);
    });

app.Map(new PathString("/health/v2/ready"),
    configuration: applicationBuilder =>
    {
        var opt = Options.Create(new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains("RoMik2"),
            AllowCachingResponses = false
        });
        // applicationBuilder.UseMiddleware<HealthCheckMiddleware>();
        applicationBuilder.UseMiddleware<HealthCheckMiddleware>(opt);
    });

#endregion

app.Run();