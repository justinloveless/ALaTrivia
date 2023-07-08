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
builder.Services.AddOpenAIService(settings => { settings.ApiKey = envApiKey ?? "";});
var databaseSettings = new TriviaDatabaseSettings();
builder.Configuration.GetSection("TriviaDatabaseSettings").Bind(databaseSettings);
builder.Services.AddSingleton(databaseSettings);
builder.Services.AddScoped<IDbContext, DbContext>();
builder.Services.AddScoped<ITriviaQuestionRepository, TriviaQuestionRepository>();
builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
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