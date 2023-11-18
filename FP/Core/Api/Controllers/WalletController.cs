using TronNet.Contracts;
using TronNet;
using Microsoft.AspNetCore.Mvc;

namespace FP.Core.Api.Controllers
{
	public class WalletController: ControllerBase
	{
		private readonly IContractClientFactory _contractClientFactory;
		private readonly ITronClient _tronClient;

		public WalletController(IContractClientFactory contractClientFactory, ITronClient tronClient)
		{
			_contractClientFactory = contractClientFactory;
			_tronClient = tronClient;
		}

		[HttpPost("test")]
		public async Task TransferAsync()
		{
			var walletClient = _tronClient.GetWallet();
			return;
		}
	}
}
