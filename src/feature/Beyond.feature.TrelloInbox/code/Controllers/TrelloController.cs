using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sitecore.Configuration;
using Sitecore.Data;
using Beyond.feature.TrelloInbox.Constants;
using Beyond.foundation.TrelloConnector.Models;
using Beyond.foundation.TrelloConnector;

namespace Beyond.feature.TrelloInbox.Controllers.TrelloInbox
{
	public class TrelloController : Controller
	{
		/// <summary>
		/// Get current user inbox sorted 
		/// </summary>
		/// <param name="sortfield">Represents the sort field name</param>
		/// <param name="sortorder">Represents the sort order string</param>
		/// <returns></returns>
		public ActionResult getUserTrelloInbox(string sortfield, string sortorder)
		{
			try
			{
				List<Card> result = GetCurrentUserInbox();

				#region Sorting Logic 
				switch (sortfield)
				{
					case "Id":
						{
							result = sortorder == "ASC" ? result.OrderBy(t => t.ID).ToList() : result.OrderByDescending(t => t.ID).ToList();
							break;
						}
					case "name":
						{
							result = sortorder == "ASC" ? result.OrderBy(t => t.Name).ToList() : result.OrderByDescending(t => t.Name).ToList();
							break;
						}
					case "description":
						{
							result = sortorder == "ASC" ? result.OrderBy(t => t.Desc).ToList() : result.OrderByDescending(t => t.Desc).ToList();
							break;
						}
					case "updateddate":
						{
							result = sortorder == "ASC" ? result.OrderBy(t => t.Date).ToList() : result.OrderByDescending(t => t.Date).ToList();
							break;
						}
					default:
						{
							result = sortorder == "ASC" ? result.OrderBy(t => t.Date).ToList() : result.OrderByDescending(t => t.Date).ToList();
							break;
						}
				}
				#endregion

				return Json(result, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				Sitecore.Diagnostics.Error.LogError(ex.Message);
				return Json(new List<Card>(), JsonRequestBehavior.AllowGet);
			}
		}

		/// <summary>
		/// Method that get the current user Inbox
		/// </summary>
		/// <returns></returns>
		private List<Card> GetCurrentUserInbox()
		{
			Database masterDB = Factory.GetDatabase(DBNames.masterDB);
			List<Card> _trelloCardItem = new List<Card>();
			if (masterDB != null)
			{
				//Get all Card Sitecore items
				List<Sitecore.Data.Items.Item> allTrelloItem = masterDB.GetItem(IDs.TrelloDataFolderId).Axes.GetDescendants()
					.Where(t => t.TemplateID == TemplatesIDs.TrelloCardItemTemplateId)
					.Where(t => t.Fields[FieldsNames.TrtelloCardName.CardMembers].Value.ToLower().Split('|').Contains(Sitecore.Context.User.Profile[FieldsNames.UserProfileFields.TrelloMemberName].ToLower())).ToList();

				if (allTrelloItem != null && allTrelloItem.Count() > 0)
				{
					foreach (var item in allTrelloItem)
					{
						_trelloCardItem.Add(new Card
						{
							ID = item.Fields[FieldsNames.TrtelloCardName.CardId].Value,
							Name = item.Fields[FieldsNames.TrtelloCardName.CardName].Value,
							Desc = item.Fields[FieldsNames.TrtelloCardName.CardDescription].Value,
							Date = Sitecore.DateUtil.IsoDateToDateTime(item.Fields[FieldsNames.TrtelloCardName.CardUpdatedDate].Value).ToString("yyyy-MM-dd HH:mm:ss"),
							URL = item.Fields[FieldsNames.TrtelloCardName.CardURL].Value
						});
					}
				}
			}

			return _trelloCardItem;
		}

		/// <summary>
		/// Sets the card as done by moving it to the Done list
		/// </summary>
		/// <param name="cardId">The card identifier.</param>
		/// <returns></returns>
		public ActionResult SetCardAsDone(string cardId)
		{
			try
			{
				Database masterDB = Factory.GetDatabase(DBNames.masterDB);
				if (masterDB != null)
				{
					Sitecore.Data.Items.Item trelloConfigItem = masterDB.GetItem(IDs.trelloConfig);
					string apiKey = trelloConfigItem.Fields[FieldsNames.TrelloSettings.AppKey].Value;
					string token = trelloConfigItem.Fields[FieldsNames.TrelloSettings.AuthToken].Value;
					string board = trelloConfigItem.Fields[FieldsNames.TrelloSettings.BoardName].Value;
					string doneListName = trelloConfigItem.Fields[FieldsNames.TrelloSettings.DoneListName].Value;

					var apiModel = new ApiModel()
					{
						ApiKey = apiKey,
						Token = token,
						BoardName = board,
						DoneListName = doneListName
					};

					//Prepare Trello context
					TrelloContext trelloContext = new TrelloContext(apiModel);
					//move the current card to the done list
					var result = trelloContext.SetCardAsDone(cardId);

					if (result)
					{
						// Delete corresponding Card Sitecore Item 
						Sitecore.Data.Items.Item cardToDelete = masterDB.GetItem(IDs.TrelloDataFolderId).Axes.GetDescendants()
							.Where(t => t.TemplateID == TemplatesIDs.TrelloCardItemTemplateId)
							.Where(t => t.Fields[FieldsNames.TrtelloCardName.CardMembers].Value.Contains(Sitecore.Context.User.Profile[FieldsNames.UserProfileFields.TrelloMemberName]))
							.Where(t => t.Fields[FieldsNames.TrtelloCardName.CardId].Value == cardId).FirstOrDefault();

						if (cardToDelete != null)
							cardToDelete.Recycle();
					}
				}
			}
			catch (Exception ex)
			{
				Sitecore.Diagnostics.Error.LogError(ex.Message);
			}

			return Json(new object(), JsonRequestBehavior.AllowGet);
		}
	}
}