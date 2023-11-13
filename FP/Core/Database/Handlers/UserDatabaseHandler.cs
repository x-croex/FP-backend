using FP.Core.Api.ApiDto;
using FP.Core.Database.Models;
using Loger;
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
                    $"User with email {userData.Email} exist"
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
            Email = userData.Email,
            Name = userData.Name,
            Passwordhash = userData.Passwordhash
        };

        try
        {
            var result = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (result != null)
                if (result.Passwordhash == user.Passwordhash)
                    _loger.LogAction("User found");
                else
                    status = "Invalid password";
            else
                status = "Invalid email";
        }
        catch (Exception ex)
        {
            _loger.LogAction("Cannot find user", exception: ex);
            status = "Server error";
        }

        return status;
    }
}
