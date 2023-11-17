using System.ComponentModel.DataAnnotations.Schema;

namespace FP.Core.Database.Models;

public class Wallet
{
    public int Id { get; set; }
    public string WalletAddress { get; set; } = "";
    public string WalletSecretKey { get; set; } = "";
}
