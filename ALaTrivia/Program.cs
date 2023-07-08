using ALaTrivia.Data;
using ALaTrivia.Models;
using ALaTrivia.Models.Mongo;
using ALaTrivia.Repository;
using OpenAI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddLogging();
var envApiKey = Environment.GetEnvironmentVariable("OpenAiApiKey");
Console.WriteLine($"OpenAiApiKey = {envApiKey}");
builder.Services.AddOpenAIService(settings => { settings.ApiKey = envApiKey ?? "";});
var databaseSettings = new TriviaDatabaseSettings
{
    CollectionName = Environment.GetEnvironmentVariable("COLLECTION_NAME"),
    ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING"),
    DatabaseName = Environment.GetEnvironmentVariable("DATABASE_NAME")
};
Console.WriteLine($"database Settings: {databaseSettings.DatabaseName} {databaseSettings.CollectionName} {databaseSettings.ConnectionString}");
builder.Services.AddSingleton(databaseSettings);
builder.Services.AddScoped<ITriviaQuestionContext, TriviaQuestionContext>();
builder.Services.AddScoped<ITriviaQuestionRepository, TriviaQuestionRepository>();
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

app.Run();