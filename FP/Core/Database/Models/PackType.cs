﻿namespace FP.Core.Database.Models
{
	public class PackType
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public List<Pack> Packs { get; set; }
	}
}
