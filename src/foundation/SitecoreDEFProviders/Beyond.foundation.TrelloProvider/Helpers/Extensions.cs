using Beyond.feature.TrelloProvider.Plugins;
using Sitecore.DataExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyond.feature.TrelloProvider.Helpers
{
	public static class Extensions
	{
		public static TrelloEndpointSettings GetTextFileSettings(this Endpoint endpoint)
		{
			return endpoint.GetPlugin<TrelloEndpointSettings>();
		}

		public static bool HasTextFileSettings(this Endpoint endpoint)
		{
			return (GetTextFileSettings(endpoint) != null);
		}
	}
}
