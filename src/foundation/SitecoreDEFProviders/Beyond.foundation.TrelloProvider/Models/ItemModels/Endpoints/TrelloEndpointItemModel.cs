using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyond.feature.TrelloProvider.Models.ItemModels.Endpoints
{
	public class TrelloEndpointItemModel : ItemModel
	{
		public const string AppName = "App Name";
		public const string AppKey = "App Key";
		public const string AuthToken = "Auth Token";

		public const string BoardName = "Board Name";
		public const string ToDoListName = "To Do List Name";
		public const string DoneListName = "Done List Name";
	}
}
