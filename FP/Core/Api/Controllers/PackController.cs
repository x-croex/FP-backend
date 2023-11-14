using FP.Core.Api.ApiDto;
using FP.Core.Api.Handlers;
using FP.Core.Database.Handlers;
using Loger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FP.Core.Database.Models;

namespace FP.Core.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PackController : ControllerBase
{
	private readonly ILogger<PackController> _logger;
	private readonly PackDatabaseHandler _databaseHandler;

	public PackController(PackDatabaseHandler databaseHandler, ILogger<PackController> logger)
	{
		_databaseHandler = databaseHandler;
		_logger = logger;
	}


	[HttpPost("create")]
	public async Task<IActionResult> CreatePackAsync([FromBody] PackDto pack)
	{
		var result = await _databaseHandler.CreatePack(pack);

		if (result != null)
		{
			_logger.LogInformation("Pack created successfully {result}", result.ID);
			return Ok(result);
		}
		else
		{
			_logger.LogInformation("Cannot create user {result}", result);
			return BadRequest(result);
		}
	}

	[HttpGet("get/{id}")]
	public async Task<IActionResult> GetPackAsync(int id)
	{
		_logger.LogInformation("API-Request \n Post | Name=login | {id}", id);

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