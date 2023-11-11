using FP.Core.Api.ApiDto;
using FP.Core.Database.Handlers;
using Loger;

namespace FP.Core.Api.Handlers;

public class UserApiHandler
{
    private LogService<UserApiHandler> _loger;

    public UserApiHandler() => _loger = new();

    public async Task<bool> CreateUser(UserDto userData)
    {
        await _loger.LogAction("Start to create new user", Loger.Enums.LogType.Inforamation);
        await _loger.LogAction("Registration...");

        var isSuccess = true;
        var result = await UserDatabaseHandler.CreateUser(userData);

        if (result)
        {
            await _loger.LogAction("UserDatabaseHandler created new user", Loger.Enums.LogType.Inforamation);
        }
        else
        {
            isSuccess = false;
            await _loger.LogAction("UserDatabaseHandler cannot create user", Loger.Enums.LogType.Inforamation);
        }

        return isSuccess;
    }
}
