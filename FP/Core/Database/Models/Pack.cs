﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FP.Core.Database.Models
{
	public class Pack
	{
		public int ID { get; set; }
		[ForeignKey("User")] public int UserId { get; set; }
		[ForeignKey("PackType")] public int PackTypeId { get; set; }
		public decimal DealSum { get; set; }
		public DateTime StartDate { get; set; } = DateTime.Now;
		public DateTime EndDate { get; set; }

		public User User { get; set; } = new();
		public PackType PackType { get; set; }
	}
}
