using Sitecore.DataExchange.Processors.PipelineSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.DataExchange.Contexts;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;

namespace Beyond.feature.TrelloProvider.Processors.PipelineSteps
{
	public class GetCardsFromTrelloSetProcessor : Sitecore.DataExchange.Processors.PipelineSteps.BaseReadDataStepProcessor
	{
		protected override bool IsEndpointValid(Endpoint endpoint, PipelineStep pipelineStep, PipelineContext pipelineContext)
		{
			return base.IsEndpointValid(endpoint, pipelineStep, pipelineContext);
		}
		protected override bool AreRequiredPluginsSet(PipelineStep pipelineStep, PipelineContext pipelineContext)
		{
			return base.AreRequiredPluginsSet(pipelineStep, pipelineContext);
		}
		protected override void ReadData(Endpoint endpoint, PipelineStep pipelineStep, PipelineContext pipelineContext)
		{
			try
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
				logger.Info($"{lines.Count} cards were read from Trello ()");
				//logger.Info("{0} rows were read from the file. (pipeline step: {1}, endpoint: {2})",
				//				lines.Count, pipelineStep.Name, endpoint.Name);
				//
				//add the plugin to the pipeline context
				pipelineContext.Plugins.Add(itemsSettings);
			}
			catch (Exception ex)
			{

			}
		}
	}

	public class GetCardsFromTrelloSetProcessor1 : Sitecore.DataExchange.Processors.Pipelines.PipelineProcessor
	{
		public override void Process(Pipeline pipeline, PipelineContext pipelineContext)
		{
			base.Process(pipeline, pipelineContext);
		}
	}
}
