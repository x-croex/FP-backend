using FP.Core.Api.Helpers;
using FP.Core.Api.Providers.Interfaces;
using FP.Core.Api.Providers.Providers;
using FP.Core.Database;
using FP.Core.Database.Handlers;
using FP.Core.Database.Models;
using FP.Core.Loger;
using Google.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using TronNet;
using TronNet.Accounts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FpDbContext>(o => o.UseNpgsql(builder.Configuration["ConnectionStrings:string"]));
builder.Services.AddCors();

builder.Services.AddScoped<UserDatabaseHandler>();
builder.Services.AddScoped<PackDatabaseHandler>();
builder.Services.AddScoped<WalletDatabaseHandler>();

builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<ICryptoApiProvider, CryptoApiProvider>();
builder.Services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddTransient<IPasswordHasher<Wallet>, PasswordHasher<Wallet>>();
builder.Services.AddTronNet(x =>
{
	x.Network = TronNetwork.MainNet;
	x.Channel = new GrpcChannelOption { Host = "grpc.shasta.trongrid.io", Port = 50051 };
	x.SolidityChannel = new GrpcChannelOption { Host = "grpc.shasta.trongrid.io", Port = 50052 };
	x.ApiKey = "c71f2212-2803-4128-a265-719a3425d882";
});

builder.Services.AddHttpClient("Crypto", client => 
{ 
    client.BaseAddress = new Uri(builder.Configuration["CryptoApiUri"]!); 
});
builder.Services.AddHttpClient("Fiat", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Fiat:BaseApiUri"]!);
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", builder.Configuration["Fiat:ApiKey"]!);
});

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
