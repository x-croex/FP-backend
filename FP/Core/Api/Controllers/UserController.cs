using FP.Core.Api.ApiDto;
using FP.Core.Api.Handlers;
using FP.Core.Database.Handlers;
using Loger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FP.Core.Api.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace FP.Core.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _loger;
    private readonly JwtService _jwtService;
    private readonly UserDatabaseHandler _databaseHandler;

	public UserController(UserDatabaseHandler databaseHandler, ILogger<UserController> logger, JwtService jwtService)
	{
		_databaseHandler = databaseHandler;
        _loger = logger;
        _jwtService = jwtService;
    }


    [HttpPost("create")]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserDto userData)
    {

        var result = await _databaseHandler.CreateUser(userData);

        if(result != null)
        {
            _loger.LogInformation("User created successfully {result}", result);
            return Ok(result);
        }
        else
        {
            _loger.LogInformation("Cannot create user {result}", result);
            return BadRequest(result);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync([FromBody] UserDto userData)
    {
        _loger.LogInformation("API-Request \n Post | Name=login | {userData}", userData);
        var result = await _databaseHandler.LoginUser(userData);

        if(result != null )
        {
            var jwtToken = _jwtService.Generate(result.Id);
            Response.Cookies.Append("jwt", jwtToken, new CookieOptions
            {
                HttpOnly = true
            });
        }

        if (result != null)
        {
            _loger.LogInformation("User found successfully {result}", result);
            return Ok(result);
        }
        else
        {
            _loger.LogInformation("Cannot find user {result}", result);
            return BadRequest(result);
        }
    }

    [HttpPost("logout")]
    public IActionResult LogoutUser()
    {
        Response.Cookies.Delete("jwt");

        return Ok("success");
    }
}