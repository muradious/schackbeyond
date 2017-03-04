using Sitecore.DataExchange.Processors.PipelineSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.DataExchange.Contexts;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;

namespace Beyond.foundation.TrelloProvider.Processors.PipelineSteps
{
	public class GetListsFromTrelloSetProcessor : BaseReadDataStepProcessor
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
			//
			//get the file path from the plugin on the endpoint
			//var settings = endpoint.GetTextFileSettings();
			//if (settings == null)
			//{
			//	return;
			//}

			//TrelloConnector.TrelloHelper.
			var lines = new List<string>();

			var itemsSettings = new IterableDataSettings(lines);
			logger.Info($"{lines.Count} lists were read from Trello ()");
			//logger.Info("{0} rows were read from the file. (pipeline step: {1}, endpoint: {2})",
			//				lines.Count, pipelineStep.Name, endpoint.Name);
			//
			//add the plugin to the pipeline context
			pipelineContext.Plugins.Add(itemsSettings);
		}
	}
}
