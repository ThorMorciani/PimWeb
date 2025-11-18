using AIssist.Application.Api.Services;
using AIssist.Application.Api.Services.Interfaces;
using AIssist.Application.Services;
using AIssist.Application.Services.Interfaces;
using AIssist.Infrastructure.Data;
using AIssist.Infrastructure.Ioc;
using Microsoft.EntityFrameworkCore;
using Google.GenAI;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(connectionString);
builder.Services.AddDependencyInjection();
builder.Services.AddScoped<IAuthService, AuthService>();

// Configurações Gemini
var apiKey = builder.Configuration["Gemini:ApiKey"];
builder.Services.AddSingleton(sp => new Client(apiKey: apiKey));
builder.Services.AddScoped<IAService, AIService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAngular");
app.UseAuthorization();

app.MapControllers();

app.Run();
