using Sitecore.DataExchange.Processors.PipelineSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.DataExchange.Contexts;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using TrelloConnector;
using TrelloConnector.Models;
using Beyond.feature.TrelloProvider.Plugins;

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

			TrelloContext trello = new TrelloContext();
			var cards = trello.GetTrelloItems(new ApiModel { ApiKey = settings.AppKey, BoardName = settings.BoardName, Token = settings.AuthToken, ToDoListName = settings.ToDoListName });

			var itemsSettings = new IterableDataSettings(cards);

			logger.Info($"{cards.Count} cards were read from Trello ({settings.AppName}, {settings.AppKey}, {settings.BoardName}, {settings.ToDoListName})");

			//add the plugin to the pipeline context
			pipelineContext.Plugins.Add(itemsSettings);
		}
	}

	//public class GetCardsFromTrelloSetProcessor1 : Sitecore.DataExchange.Processors.Pipelines.PipelineProcessor
	//{
	//	public override void Process(Pipeline pipeline, PipelineContext pipelineContext)
	//	{
	//		base.Process(pipeline, pipelineContext);
	//	}
	//}
}
