using TronNet.Contracts;
using TronNet;
using Microsoft.AspNetCore.Mvc;
using FP.Core.Api.ApiDto;
using FP.Core.Api.Helpers;
using FP.Core.Database.Models;
using FP.Core.Database.Handlers;
using FP.Core.Api.Providers.Providers.Networks.TRC20;
using FP.Core.Api.Providers.Interfaces;

namespace FP.Core.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class WalletController : ControllerBase
	{
		private readonly JwtService _jwtService;
		private readonly ICryptoApiTRC20Provider _cryptoTRC20Provider;
		private readonly ILogger<WalletController> _logger;
		private readonly UserDatabaseHandler _userDatabaseHandler;
		private readonly WalletDatabaseHandler _walletDatabaseHandler;

		public WalletController(JwtService jwtService, ILogger<WalletController> logger, UserDatabaseHandler userDatabaseHandler, ICryptoApiTRC20Provider cryptoApiTrc20Provider, WalletDatabaseHandler walletDatabaseHandler)
		{
			_cryptoTRC20Provider = cryptoApiTrc20Provider;
			_userDatabaseHandler = userDatabaseHandler;
			_jwtService = jwtService;
			_logger = logger;
			_walletDatabaseHandler = walletDatabaseHandler;
		}

		[HttpPost("transfer")]
		public async Task<IActionResult> TransferAsync(WalletDto toWallet, decimal amount)
		{
			var jwt = Request.Cookies["jwt"];
			if (jwt != null)
			{
				var token = _jwtService.Verify(jwt);
				var isSuccess = int.TryParse(token.Issuer, out int userId);
				if (isSuccess)
				{
					Wallet wallet = await _userDatabaseHandler.GetUserWalletById(userId);
					var fromWallet = new WalletDto { WalletAddress = wallet.WalletAddress, WalletSecretKey = wallet.WalletSecretKey };
					var result = await _cryptoTRC20Provider.TransferTrc20(fromWallet, toWallet, amount);

					if (result != null)
					{
						_logger.LogInformation("Transfer from {fromWallet} to {toWallet} amount {amount} ", fromWallet, toWallet, amount);
						return Ok(result);
					}
					else
					{
						_logger.LogInformation("Cannot create pack {result}", result);
						return BadRequest(result);
					}
				}
			}
				return Unauthorized();
		}
		[HttpPost("create")]
		public async Task<IActionResult> CreateWalletAsync()
		{
			var result = await _walletDatabaseHandler.CreateWallet();
			return Ok(result);
		}
	}
}
