using AIssist.Application.Api.Services;
using AIssist.Application.Api.Services.Interfaces;
using AIssist.Infrastructure.Ioc;
using Supabase;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDependencyInjection();
builder.Services.AddScoped<IAuthService, AuthService>();

var url = builder.Configuration["supabase:url"];
var key = builder.Configuration["supabase:key"];
var options = new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true,
};

builder.Services.AddSingleton(provider => new Client(url, key, options));

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

