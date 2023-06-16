using System.Configuration;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage.Blobs;
using get_credentials.Options;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SomeServiceOptions>(builder.Configuration.GetSection("MyServiceConfig"));
// builder.Services.ConfigureOptions<SomeServiceOptionsSetup>();

// Add services to the container.
builder.Services.AddApplicationInsightsTelemetry();

// Configuration of Azure clients
builder.Services.AddAzureClients(factoryBuilder =>
{
    if (factoryBuilder == null) throw new ArgumentNullException(nameof(factoryBuilder));
    
    
    // Set credentials
    factoryBuilder.UseCredential(new DefaultAzureCredential( new DefaultAzureCredentialOptions
    {
        ManagedIdentityClientId = "9f49b200-a820-47bd-bf8f-709ecbc9b4c9",
    }));

    factoryBuilder.AddSecretClient(vaultUri: new Uri("https://romik-kv-snippets.vault.azure.net/"));

    var cfg = builder.Configuration.GetSection("Storage");
    
    factoryBuilder.AddBlobServiceClient(cfg);
    
    factoryBuilder.AddBlobServiceClient("sldkjflsdjfsd");

});

// builder.Services.AddSecretClient(configuration:)

builder.Services.Configure<SomeServiceOptions>(builder.Configuration.GetSection("MyServiceConfig"));


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

app.MapGet("/", () => "1.0.0");

app.Run();