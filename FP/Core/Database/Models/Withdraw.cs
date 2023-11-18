namespace FP.Core.Database.Models
{
	public class Withdraw
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public decimal Sum { get; set; }
		public DateTime CreationTime { get; set; }
		public DateTime RealizationTime { get; set; }
		public bool IsRealized { get; set; }
		public string WalletAddress { get; set; }
	}
}
