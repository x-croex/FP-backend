using FP.Core.Api.Helpers;
using FP.Core.Database;
using FP.Core.Database.Handlers;
using FP.Core.Database.Models;
using FP.Core.Loger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FpDbContext>(o => o.UseNpgsql(builder.Configuration["ConnectionStrings:string"]));
builder.Services.AddCors();

builder.Services.AddScoped<UserDatabaseHandler>();
builder.Services.AddScoped<PackDatabaseHandler>();
builder.Services.AddScoped<JwtService>();

builder.Services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();

DataLogger.StartLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(options => options
    .WithOrigins("http://localhost:3000", "http://localhost:8080", "http://localhost:4200")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials());

app.MapControllers();

app.Run();
