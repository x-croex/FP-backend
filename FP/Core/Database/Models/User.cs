namespace FP.Core.Database.Models;

public class User
{
    public int Id { get; set; }
    public int Rang { get; set; }
    public string Name { get; set; } = "";
    public string Passwordhash { get; set; } = "";
    public string Email { get; set; } = "";
    public float BalanceCrypto { get; set; }
    public float BalanceFiat { get; set; }
    public float BalanceInternal { get; set; }

    public virtual List<Pack> Pack { get; set; }
}
