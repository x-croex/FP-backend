using FP.Core.Api.Handlers;
using FP.Core.Database;
using FP.Core.Database.Handlers;
using FP.Core.Database.Models;
using FP.Core.Global;
using FP.Core.Loger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FpDbContext>(o => o.UseNpgsql(builder.Configuration["ConnectionStrings:string"]));
builder.Services.AddScoped<UserDatabaseHandler>();
builder.Services.AddScoped<PackDatabaseHandler>();
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

app.MapControllers();

app.Run();
