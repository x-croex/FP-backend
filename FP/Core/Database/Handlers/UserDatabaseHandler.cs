using FP.Core.Api.ApiDto;
using FP.Core.Database.Models;
using Loger;

namespace FP.Core.Database.Handlers;

public class UserDatabaseHandler
{
    public static LogService<UserDatabaseHandler> _loger = new();

    public static async Task<bool> CreateUser(UserDto userData)
    {
        await _loger.LogAction("Start to add user in database", Loger.Enums.LogType.Inforamation);
        await _loger.LogAction($"{userData}");
        bool isSuccess = true;

        using FpDbContext context = new();
        FpUser user = new()
        {
            Email = userData.Email,
            Name = userData.Name,
            Passwordhash = userData.Passwordhash
        };

        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            await _loger.LogAction("User created", Loger.Enums.LogType.Inforamation);
        }
        catch (Exception ex)
        {
            isSuccess = false;
            await _loger.LogAction("Cannot create user", Loger.Enums.LogType.Inforamation, exception: ex);
        }

        return isSuccess;
    }
}
