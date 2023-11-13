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

    [HttpPost("create")]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserDto userData)
    {
        _loger.LogAction("API-Request", new string[]
        {
            $"Post | Name=create | {userData}"
        });

        UserApiHandler handler = new();
        var result = await handler.CreateUser(userData);

        if(result == "Ok")
        {
            _loger.LogAction($"User created successfully");
            return Ok(result);
        }
        else
        {
            _loger.LogAction($"Cannot create user", new string[]
            {
                $"{result}"
            });
            return BadRequest(result);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync([FromBody] UserDto userData)
    {
        _loger.LogAction("API-Request", new string[]
        {
            $"Post | Name=login | {userData}"
        });

        UserApiHandler handler = new();
        var result = await handler.LoginUser(userData);

        if (result == "Ok")
        {
            _loger.LogAction($"User found successfully");
            return Ok(result);
        }
        else
        {
            _loger.LogAction($"Cannot find user", new string[]
            {
                $"Post | Name=login | {result}"
            });
            return BadRequest(result);
        }
    }
}