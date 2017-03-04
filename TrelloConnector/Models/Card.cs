﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloConnector.Interfaces;

namespace TrelloConnector.Models
{
	public class Card : ICard
	{
		public string ID { get; set; }
		public string Name { get; set; }
		public string Desc { get; set; }
		public string Members { get; set; }
	}
}
