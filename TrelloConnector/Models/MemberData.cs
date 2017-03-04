using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beyond.foundation.TrelloConnector.Interfaces;

namespace Beyond.foundation.TrelloConnector.Models
{
	public class MemberData : IMemberData
	{
		public string MemberID { get; set; }
		public string MemberName { get; set; }
	}
}
