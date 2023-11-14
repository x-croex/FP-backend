using FP.Core.Api.ApiDto;
using FP.Core.Database.Models;
using Loger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FP.Core.Database.Handlers;

public class UserDatabaseHandler
{
    public static LogService<UserDatabaseHandler> _loger = new();
	private readonly FpDbContext _dbContext;
	private readonly IServiceProvider _serviceProvider;
	public UserDatabaseHandler(FpDbContext dbContext, IServiceProvider service)
	{
		_dbContext = dbContext;
		_serviceProvider = service;
	}

	public async Task<string> CreateUser(UserDto userData)
	{
		_loger.LogAction("Start to add user in database", new string[]
		{
		$"{userData}"
		});

		string status = "Ok";
		var hasher = _serviceProvider.GetRequiredService<IPasswordHasher<FpUser>>();
		FpUser user = new()
		{
			Email = userData.Email,
			Name = userData.Name,
		
			
		};
		user.Passwordhash = hasher.HashPassword(user, userData.Passwordhash);

		try
		{
			var result = await _dbContext.Users.AnyAsync(u => u.Email == user.Email);
			if (!result)
			{
				await _dbContext.Users.AddAsync(user);
				await _dbContext.SaveChangesAsync();
				_loger.LogAction("User created");
			}
			else
			{
				_loger.LogAction("Cannot create user", new string[]
				{
				$"User with email {userData.Email} exists"
				});
				status = "Invalid email";
			}
		}
		catch (Exception ex)
		{
			status = "Server error";
			_loger.LogAction("Cannot create user", exception: ex);
		}

		return status;
	}

	public async Task<string> LoginUser(UserDto userData)
	{
		_loger.LogAction("Start to find user in database", new string[]
		{
		$"{userData}"
		});

		string status = "Ok";

		FpUser user = new()
		{
			Email = userData.Email
		};
		var hasher = _serviceProvider.GetRequiredService<IPasswordHasher<FpUser>>();
		try
		{
			var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
			if (result != null)
			{
				if (hasher.VerifyHashedPassword(result, result.Passwordhash, userData.Passwordhash) == PasswordVerificationResult.Success)
				{
					_loger.LogAction("User found");
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
		}
		catch (Exception ex)
		{
			_loger.LogAction("Cannot find user", exception: ex);
			status = "Server error";
		}

		return status;
	}
}
