using FP.Core.Api.ApiDto;
using FP.Core.Api.Handlers;
using FP.Core.Database.Handlers;
using Loger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FP.Core.Database.Models;
using FP.Core.Api.Helpers;

namespace FP.Core.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PackController : ControllerBase
{
	private readonly ILogger<PackController> _logger;
    private readonly JwtService _jwtService;
    private readonly PackDatabaseHandler _databaseHandler;

	public PackController(PackDatabaseHandler databaseHandler, ILogger<PackController> logger, JwtService jwtService)
	{
		_databaseHandler = databaseHandler;
		_logger = logger;
        _jwtService = jwtService;
    }


	[HttpGet("create")]
	public async Task<IActionResult> CreatePackAsync([FromBody] PackDto pack)
	{
		var jwt = Request.Cookies["jwt"];
		if(jwt != null)
		{
			var token = _jwtService.Verify(jwt);
			var isSuccess  = int.TryParse(token.Issuer, out int userId);
			if (isSuccess)
			{
				var result = await _databaseHandler.CreatePack(pack, userId);

				if (result != null)
				{
					_logger.LogInformation("Pack created successfully {result}", result.Id);
					return Ok(result);
				}
				else
				{
					_logger.LogInformation("Cannot create pack {result}", result);
					return BadRequest(result);
				}
			}
		}

        _logger.LogInformation($"Invalid JWT");
        return Unauthorized();
    }

	[HttpGet("get/{id}")]
	public async Task<IActionResult> GetPackAsync(int id)
	{
		_logger.LogInformation("API-Request \n Post | Name=getPack | {id}", id);

        var jwt = Request.Cookies["jwt"];
		if (jwt != null)
		{
			var token = _jwtService.Verify(jwt);
            var isSuccess = int.TryParse(token.Issuer, out int userId);

			if (isSuccess)
			{
				var pack = await _databaseHandler.GetPackById(id);

				if (pack != null)
				{
					_logger.LogInformation("User found successfully {result}", pack);
					return Ok(pack);
				}
				else
				{
					_logger.LogInformation("Cannot find user {result}", pack);
					return BadRequest(pack);
				}
			}
		}

        _logger.LogInformation($"Invalid JWT");
        return Unauthorized();
    }
}