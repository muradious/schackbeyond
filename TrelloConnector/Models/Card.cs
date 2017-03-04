using System;
using Beyond.foundation.TrelloConnector.Interfaces;

namespace Beyond.foundation.TrelloConnector.Models
{
	public class Card : ICard
	{
		public string ID { get; set; }
		public string Name { get; set; }
		public string Desc { get; set; }
		public string Date { get; set; }
		public string Members { get; set; }
		public string URL { get; set; }
	}
}
