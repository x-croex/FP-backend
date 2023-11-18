using FP.Core.Api.Providers.Interfaces;
using FP.Core.Api.Responses;
using System.Net;

namespace FP.Core.Api.Providers.Providers;

public class CryptoApiProvider : ICryptoApiProvider
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CryptoApiProvider(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<CryptoCreatedTrc20Wallet?> CreateTrc20Wallet()
    {
        var httpClient = _httpClientFactory.CreateClient("Crypto");
        var response = await httpClient.PostAsync("create_wallet", null);
        if (response.StatusCode != HttpStatusCode.OK) return null;
        return await response.Content.ReadFromJsonAsync<CryptoCreatedTrc20Wallet>();
    }

    public async Task<decimal?> GetTrc20WalletBalance(string walletAddress)
    {
        var httpClient = _httpClientFactory.CreateClient("Crypto");
        var response = await httpClient.GetAsync($"wallet_balance/{walletAddress}");
        if (response.StatusCode != HttpStatusCode.OK) return null;

        var updated = await response.Content.ReadFromJsonAsync<CryptoTrc20WalletBalance>();
        return updated?.Balance;
    }

}
