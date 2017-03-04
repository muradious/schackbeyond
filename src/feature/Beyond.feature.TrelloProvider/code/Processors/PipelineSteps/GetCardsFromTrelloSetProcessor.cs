using System;
using Sitecore.DataExchange.Contexts;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Beyond.feature.TrelloProvider.Plugins;
using Beyond.foundation.TrelloConnector.Models;
using Beyond.foundation.TrelloConnector;

namespace Beyond.feature.TrelloProvider.Processors.PipelineSteps
{
	public class GetCardsFromTrelloSetProcessor : Sitecore.DataExchange.Processors.PipelineSteps.BaseReadDataStepProcessor
	{
		protected override void ReadData(Endpoint endpoint, PipelineStep pipelineStep, PipelineContext pipelineContext)
		{
			if (endpoint == null)
			{
				throw new ArgumentNullException(nameof(endpoint));
			}
			if (pipelineStep == null)
			{
				throw new ArgumentNullException(nameof(pipelineStep));
			}
			if (pipelineContext == null)
			{
				throw new ArgumentNullException(nameof(pipelineContext));
			}

			var logger = pipelineContext.PipelineBatchContext.Logger;
			var settings = endpoint.GetPlugin<TrelloEndpointSettings>();
			if (settings == null)
			{
				return;
			}

			//Prepare the Trello API context
			var apiModel = new ApiModel { ApiKey = settings.AppKey, BoardName = settings.BoardName, Token = settings.AuthToken, ToDoListName = settings.ToDoListName };
			TrelloContext trello = new TrelloContext(apiModel);

			var cards = trello.GetTrelloItems();

			logger.Info($"{cards.Count} cards were read from Trello ({settings.AppName}, {settings.AppKey}, {settings.BoardName}, {settings.ToDoListName})");

			//Send the cards to the pipelineContext
			var itemsSettings = new IterableDataSettings(cards);
			pipelineContext.Plugins.Add(itemsSettings);
		}
	}
}
