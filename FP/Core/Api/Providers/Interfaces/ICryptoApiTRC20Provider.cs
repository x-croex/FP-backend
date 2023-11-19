using FP.Core.Api.ApiDto;
using FP.Core.Api.Responses;

namespace FP.Core.Api.Providers.Interfaces;

public interface ICryptoApiTRC20Provider
{
    public Task<CryptoCreatedWallet?> CreateTrc20Wallet();

    public Task<decimal?> GetTrc20WalletBalance(string walletAddress);
    public Task<decimal?> TransferTrc20(WalletDto fromWallet, WalletDto toWallet, decimal amount);
}