using FP.Core.Api.ApiDto;
using FP.Core.Api.Controllers;
using FP.Core.Database.Models;
using Loger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FP.Core.Database.Handlers;

public class UserDatabaseHandler
{
	private readonly ILogger<UserDatabaseHandler> _logger;
	private readonly FpDbContext _dbContext;
	private readonly IServiceProvider _serviceProvider;
	private readonly WalletDatabaseHandler _walletDatabaseHandler;

	public UserDatabaseHandler(FpDbContext dbContext, WalletDatabaseHandler walletDatabaseHandler, IServiceProvider service, ILogger<UserDatabaseHandler> logger)
	{
		_dbContext = dbContext;
		_serviceProvider = service;
		_logger = logger;
		_walletDatabaseHandler = walletDatabaseHandler;
	}

	public async Task<string> CreateUser(UserDto userData)
	{
		_logger.LogInformation($"Start to add user in database {userData}");

		string status = "Ok";
		var hasher = _serviceProvider.GetRequiredService<IPasswordHasher<User>>();
		User user = new()
		{
			Email = userData.Email,
			Name = userData.Name,
			BalanceWallet = await _walletDatabaseHandler.CreateWallet(),
			TopUpWallet = await _walletDatabaseHandler.CreateWallet()
		};
		user.Passwordhash = hasher.HashPassword(user, userData.Passwordhash);

		try
		{
			var result = await _dbContext.Users.AnyAsync(u => u.Email == user.Email);
			if (!result)
			{
				await _dbContext.Users.AddAsync(user);
				await _dbContext.SaveChangesAsync();
				_logger.LogInformation("User created");
			}
			else
			{
				_logger.LogInformation("Cannot create user with email {Email}", user.Email);
				status = "Invalid email";
			}
		}
		catch (Exception ex)
		{
			status = "Server error";
			_logger.LogInformation(ex, "Cannot create user");
		}
		status = JsonSerializer.Serialize(user);
		return status;
	}

	public async Task<User?> LoginUser(UserDto userData)
	{
		_logger.LogInformation("Start to find user in database {userData}", userData);

		string status = "Ok";

		User user = new()
		{
			Email = userData.Email
		};
		var hasher = _serviceProvider.GetRequiredService<IPasswordHasher<User>>();
		try
		{
			var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
			if (result != null)
			{
				if (hasher.VerifyHashedPassword(result, result.Passwordhash, userData.Passwordhash) == PasswordVerificationResult.Success)
				{
					_logger.LogInformation("User found");
				}
				else
				{
					status = "Invalid password";
				}
			}
			else
			{
				status = "Invalid email";
			}
			_logger.LogInformation(status);
			return result;
		}
		catch (Exception ex)
		{
			_logger.LogInformation(ex, "Cannot create user");
			status = "Server error";
			return null;
		}

	}
	public async Task<Wallet?> GetUserWalletById(int userId)
	{
		_logger.LogInformation("Start to find user in database {userId}", userId);
		var user = await _dbContext.Users.Include<User, Wallet>(u => u.BalanceWallet).FirstOrDefaultAsync(u => userId == u.Id);
		return user.BalanceWallet;
	}


}
