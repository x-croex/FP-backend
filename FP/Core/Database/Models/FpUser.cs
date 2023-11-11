namespace FP.Core.Database.Models;

public class FpUser
{
    public int Id { get; set; }
    public int Rang { get; set; }
    public string Name { get; set; } = "";
    public string Passwordhash { get; set; } = "";
    public string Email { get; set; } = "";
    public float BalanceCrypto { get; set; }
    public float BalanceFiat { get; set; }
    public float BalanceInternal { get; set; }

    public override string ToString()
    {
        return $"ObjectId - {Id} | Type - FpUser";
    }
}
