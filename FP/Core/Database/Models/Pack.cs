namespace FP.Core.Database.Models
{
	public class Pack
	{
		public int ID { get; set; }
		public int UserId { get; set; }
		public decimal DealSum { get; set; }
		public DateTime StartDate { get; set; } = DateTime.Now;
		public DateTime EndDate { get; set; }

		public User User { get; set; }
	}
}
