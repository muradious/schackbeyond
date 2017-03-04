using Sitecore.DataExchange.Converters.DataAccess.ValueAccessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
using Sitecore.DataExchange.DataAccess;
using Beyond.foundation.TrelloProvider.Models.ItemModels.DataAccess;

namespace Beyond.foundation.TrelloProvider.Converters.DataAccess.ValueAccessors
{
	//public class CardIDValueAccessorConverter : ValueAccessorConverter
	//{
	//	private static readonly Guid TemplateId = Guid.Parse("{AF34BAE3-8AFA-401A-8151-9EDD41DC1006}");
	//	public CardIDValueAccessorConverter(IItemModelRepository repository) : base(repository)
 //       {
	//		this.SupportedTemplateIds.Add(TemplateId);
	//	}

	//	public override IValueAccessor Convert(ItemModel source)
	//	{
	//		var accessor = base.Convert(source);
	//		if (accessor == null)
	//		{
	//			return null;
	//		}
	//		var IdField = base.GetStringValue(source, ListValueAccessorItemModel.IDField);
	//		if (string.IsNullOrEmpty(IdField))
	//		{
	//			return null;
	//		}

	//		if (accessor.ValueReader == null)
	//		{
	//			accessor.ValueReader = new Sitecore.DataExchange.DataAccess.Readers.PropertyValueReader(IdField);
	//		}
	//		if (accessor.ValueWriter == null)
	//		{
	//			accessor.ValueWriter = new Sitecore.DataExchange.DataAccess.Writers.PropertyValueWriter(IdField);
	//		}
	//		return accessor;
	//	}

	//}
}
