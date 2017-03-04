using Beyond.feature.TrelloProvider.Models.ItemModels.Endpoints;
using Beyond.feature.TrelloProvider.Plugins;
using Sitecore.DataExchange.Converters.Endpoints;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyond.feature.TrelloProvider.Converters.Endpoints
{
	public class TrelloEndpointConverter: BaseEndpointConverter<ItemModel>
	{
		private static readonly Guid TemplateId = Guid.Parse("{8E09138D-8BB4-4511-9EE7-2E7F1B9A1DEC}");
		public TrelloEndpointConverter(IItemModelRepository repository) : base(repository)
        {
			//
			//identify the template an item must be based
			//on in order for the converter to be able to
			//convert the item
			this.SupportedTemplateIds.Add(TemplateId);
		}

		protected override void AddPlugins(ItemModel source, Endpoint endpoint)
		{
			//
			//create the plugin
			var settings = new TrelloEndpointSettings();
			//
			//populate the plugin using values from the item
			settings.AppName =
				base.GetStringValue(source, TrelloEndpointItemModel.AppName);
			settings.AppKey =
				base.GetStringValue(source, TrelloEndpointItemModel.AppKey);
			settings.AuthToken =
				base.GetStringValue(source, TrelloEndpointItemModel.AuthToken);

			settings.BoardName =
				base.GetStringValue(source, TrelloEndpointItemModel.BoardName);
			settings.ToDoListName =
				base.GetStringValue(source, TrelloEndpointItemModel.ToDoListName);
			settings.DoneListName =
				base.GetStringValue(source, TrelloEndpointItemModel.DoneListName);
			//
			//add the plugin to the endpoint
			endpoint.Plugins.Add(settings);
		}
	}
}
