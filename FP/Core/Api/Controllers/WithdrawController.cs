using FP.Core.Api.ApiDto;
using FP.Core.Api.Helpers;
using FP.Core.Database.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace FP.Core.Api.Controllers
{
	public class WithdrawController : Controller
	{
		private readonly ILogger<PackController> _logger;
		private readonly JwtService _jwtService;
		private readonly WithdrawDatabaseHandler _databaseHandler;

		public WithdrawController(WithdrawDatabaseHandler databaseHandler, ILogger<PackController> logger, JwtService jwtService)
		{
			_databaseHandler = databaseHandler;
			_logger = logger;
			_jwtService = jwtService;
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreatePackAsync([FromBody] PackDto pack)
		{
			var jwt = Request.Cookies["jwt"];
			if (jwt != null)
			{
				var token = _jwtService.Verify(jwt);
				var isSuccess = int.TryParse(token.Issuer, out int userId);
				if (isSuccess)
				{
					var result = await _databaseHandler.CreateWithdraw();

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
	}
}
