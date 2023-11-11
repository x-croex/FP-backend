using FP.Core.Api.ApiDto;
using FP.Core.Api.Handlers;
using Loger;
using Microsoft.AspNetCore.Mvc;

namespace FP.Core.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private LogService<UserController> _loger;

    public UserController() => _loger = new();

    [HttpPost(Name = "CreateUser")]
    public async Task CreateUserAsync([FromBody] UserDto userData)
    {
        await _loger.LogAction("API-Request", Loger.Enums.LogType.Inforamation);
        await _loger.LogAction("Post. Name=CreateUser");
        UserApiHandler handler = new();
        var result = await handler.CreateUser(userData);

        if(result)
        {
            Ok("User created successfully");
            await _loger.LogAction("User created successfully", Loger.Enums.LogType.Inforamation);
        }
        else
        {
            BadRequest("Cannot create user");
            await _loger.LogAction("Cannot create user", Loger.Enums.LogType.Inforamation);
        }
    }
}