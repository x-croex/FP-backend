using EllipticCurve;
using FP.Core.Api.ApiDto;
using FP.Core.Api.Providers.Interfaces;
using FP.Core.Api.Responses;
using FP.Core.Database.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Text;
using System.Text.Json;
using static FP.Core.Api.Contracts.Requests.TranserRequest;

namespace FP.Core.Api.Providers.Providers.Networks.TRC20
{
	public class CryptoApiTRC20Provider : ICryptoApiTRC20Provider
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web);
		private readonly IServiceProvider _serviceProvider;

		public CryptoApiTRC20Provider(IHttpClientFactory httpClientFactory, IServiceProvider serviceProvider)
		{
			_httpClientFactory = httpClientFactory;
			_serviceProvider = serviceProvider;
		}
		public async Task<CryptoCreatedWallet?> CreateTrc20Wallet()
		{
			var httpClient = _httpClientFactory.CreateClient("Crypto");
			using var response = await httpClient.PostAsync("create_trc20_wallet", null);
			if (response.StatusCode != HttpStatusCode.OK) return null;
			return await response.Content.ReadFromJsonAsync<CryptoCreatedWallet>();
		}

		public async Task<decimal?> GetTrc20WalletBalance(string walletAddress)
		{
			var httpClient = _httpClientFactory.CreateClient("Crypto");
			using var response = await httpClient.GetAsync($"trc20_balance/{walletAddress}");
			if (response.StatusCode != HttpStatusCode.OK) return null;
			var updated = await response.Content.ReadFromJsonAsync<CryptoWalletBalance>();
			return updated?.Balance;
		}
		public async Task<decimal?> TransferTrc20(WalletDto fromWallet, WalletDto toWallet, decimal amount)
		{
			var hasher = _serviceProvider.GetRequiredService<IPasswordHasher<WalletDto>>();
			try
			{
				var httpClient = _httpClientFactory.CreateClient("Crypto");

				var transferRequest = new
				{
					from = new { address = fromWallet.WalletAddress, privateKey = fromWallet.WalletSecretKey },
					to = new { address = toWallet.WalletAddress, privateKey = toWallet.WalletSecretKey },
					amount
				};


				using var content = new StringContent(JsonSerializer.Serialize(transferRequest, Options));
				using var response = await httpClient.PostAsync("transfer_trc20", content);

				if (response.StatusCode != HttpStatusCode.OK)
				{
					// Handle the error if needed
					return null;
				}

				var updated = await response.Content.ReadFromJsonAsync<CryptoWalletBalance>();

				return updated?.Balance;
			}
			catch (Exception ex)
			{
				// Handle exceptions appropriately, log, or rethrow if necessary
				return null;
			}
		}

	}
}
