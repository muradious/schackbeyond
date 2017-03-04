using Sitecore.DataExchange.Converters.PipelineSteps;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Repositories;
using Sitecore.DataExchange.Plugins;
using Beyond.foundation.TrelloProvider.Models.ItemModels;

namespace Beyond.foundation.TrelloProvider.Converters.PipelineSteps
{
	public class GetCardsFromTrelloStepConverter : BasePipelineStepConverter<ItemModel>
	{
		private static readonly Guid TemplateId = Guid.Parse("{DC27AC8C-AA22-4419-8141-02C32BBF4ACA}");
		public GetCardsFromTrelloStepConverter(IItemModelRepository repository) : base(repository)
        {
			this.SupportedTemplateIds.Add(TemplateId);
		}

		protected override void AddPlugins(ItemModel source, PipelineStep pipelineStep)
		{
			AddEndpointSettings(source, pipelineStep);
		}

		private void AddEndpointSettings(ItemModel source, PipelineStep pipelineStep)
		{
			var settings = new EndpointSettings();
			var endpointFrom = base.ConvertReferenceToModel<Endpoint>(source, GetCardsFromTrelloStepItemModel.EndpointFrom);
			if (endpointFrom != null)
			{
				settings.EndpointFrom = endpointFrom;
			}
			pipelineStep.Plugins.Add(settings);
		}
	}
}
