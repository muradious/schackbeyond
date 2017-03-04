using Sitecore.DataExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyond.feature.TrelloProvider.Plugins
{
	public class TrelloEndpointSettings: IPlugin
	{
		public string AppName { get; set; }
		public string AppKey { get; set; }
		public string AuthToken { get; set; }

		public string BoardName { get; set; }
		public string ToDoListName { get; set; }
		public string DoneListName { get; set; }
	}
}
