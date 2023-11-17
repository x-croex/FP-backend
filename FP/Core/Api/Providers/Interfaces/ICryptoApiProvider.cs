using FP.Core.Api.Responses;

namespace FP.Core.Api.Providers.Interfaces;

public interface ICryptoApiProvider
{
    public Task<CryptoCreatedTrc20Wallet?> CreateTrc20Wallet();

    public Task<decimal?> GetTrc20WalletBalance(string walletAddress);
}