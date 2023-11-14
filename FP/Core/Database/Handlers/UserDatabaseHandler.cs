using FP.Core.Api.ApiDto;
using FP.Core.Database.Models;
using Loger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FP.Core.Database.Handlers;

public class UserDatabaseHandler
{
    public static LogService<UserDatabaseHandler> _loger = new();

	public static async Task<string> CreateUser(UserDto userData)
	{
		_loger.LogAction("Start to add user in database", new string[]
		{
		$"{userData}"
		});

		string status = "Ok";
		PasswordHasher<UserDto> hasher = new PasswordHasher<UserDto>();
		userData.Passwordhash = hasher.HashPassword(userData, userData.Passwordhash);
		using FpDbContext context = new();
		FpUser user = new()
		{
			Email = userData.Email,
			Name = userData.Name,
		
			Passwordhash = userData.Passwordhash
		};

		try
		{
			var result = await context.Users.AnyAsync(u => u.Email == user.Email);
			if (!result)
			{
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();
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

	public static async Task<string> LoginUser(UserDto userData)
	{
		_loger.LogAction("Start to find user in database", new string[]
		{
		$"{userData}"
		});

		string status = "Ok";

		using FpDbContext context = new();
		FpUser user = new()
		{
			Email = userData.Email
		};

		try
		{
			var result = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
			if (result != null)
			{
				if (BCrypt.Net.BCrypt.Verify(userData.Passwordhash, result.Passwordhash))
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
