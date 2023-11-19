using FP.Core.Api.ApiDto;
using FP.Core.Api.Providers.Interfaces;
using FP.Core.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; 
using System.Text.Json;
using TronNet;

namespace FP.Core.Database.Handlers;

public class WalletDatabaseHandler
{
    private readonly ILogger<WalletDatabaseHandler> _logger;
    private readonly FpDbContext _dbContext;
    private readonly IServiceProvider _serviceProvider;
    private readonly ICryptoApiTRC20Provider _cryptoApiProvider;

    public WalletDatabaseHandler(FpDbContext dbContext, IServiceProvider service, ILogger<WalletDatabaseHandler> logger, ICryptoApiTRC20Provider cryptoApiProvider)
    {
        _dbContext = dbContext;
        _serviceProvider = service;
        _logger = logger;
        _cryptoApiProvider = cryptoApiProvider;
    }

    public async Task<Wallet> CreateWallet()
    {
        _logger.LogInformation("Start to add wallet in database");

        string status = "Ok";
        Wallet wallet = new();
        var key = TronECKey.GenerateKey(TronNetwork.MainNet);
        var address = key.GetPublicAddress();

        if (address != null && key != null)
        {
            wallet.WalletAddress = address;
            wallet.WalletSecretKey = key.GetPrivateKey();
        }

        try
        {
            var result = await _dbContext.Wallets.AnyAsync(u => u.WalletAddress == wallet.WalletAddress);
            if (!result)
            {
                await _dbContext.Wallets.AddAsync(wallet);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Wallet created");
            }
            else
            {
                _logger.LogInformation($"Cannot create user with email {wallet.WalletAddress}");
                status = "Invalid email";
            }
        }
        catch (Exception ex)
        {
            status = "Server error";
            _logger.LogInformation(ex, "Cannot create user");
        }
        status = JsonSerializer.Serialize(wallet);

        return wallet;
    }

   

}