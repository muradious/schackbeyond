using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Sitecore.Configuration;
using Sitecore.Data;
using Beyond.feature.TrelloInbox.Constants;
using Beyond.feature.TrelloInbox.Models;
using TrelloConnector;

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
                List<TrelloCardItem> _result = getCurrentUserInbox();

                #region Sorting Logic 
                switch (sortfield)
                {
                    case "Id":
                        {
                            _result = sortorder == "ASC" ? _result.OrderBy(t => t.Id).ToList() : _result.OrderByDescending(t => t.Id).ToList();
                            break;
                        }
                    case "name":
                        {
                            _result = sortorder == "ASC" ? _result.OrderBy(t => t.Name).ToList() : _result.OrderByDescending(t => t.Name).ToList();
                            break;
                        }
                    case "description":
                        {
                            _result = sortorder == "ASC" ? _result.OrderBy(t => t.Description).ToList() : _result.OrderByDescending(t => t.Description).ToList();
                            break;
                        }
                    case "updateddate":
                        {
                            _result = sortorder == "ASC" ? _result.OrderBy(t => t.CardUpdatedDate).ToList() : _result.OrderByDescending(t => t.CardUpdatedDate).ToList();
                            break;
                        }
                    default:
                        {
                            _result = sortorder == "ASC" ? _result.OrderBy(t => t.CardUpdatedDate).ToList() : _result.OrderByDescending(t => t.CardUpdatedDate).ToList();
                            break;
                        }
                } 
                #endregion

                return Json(_result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Error.LogError(ex.Message);
                return Json(new List<TrelloCardItem>(), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Method that get the current user Inbox
        /// </summary>
        /// <returns></returns>
        private List<TrelloCardItem> getCurrentUserInbox()
        {
            Database masterDB = Factory.GetDatabase(DBNames.masterDB);
            List<TrelloCardItem> _trelloCardItem = new List<TrelloCardItem>();
            if (masterDB != null)
            {
                List<Sitecore.Data.Items.Item> allTrelloItem = masterDB.GetItem(IDs.TrelloDataFolderId).Axes.GetDescendants()
                    .Where(t => t.TemplateID == TemplatesIDs.TrelloCardItemTemplateId)
                    .Where(t=>t.Fields[FieldsNames.TrtelloCardName.CardMembers].Value.Contains(Sitecore.Context.User.Profile[FieldsNames.UserProfileFields.TrelloMemberName])).ToList();


                if (allTrelloItem!= null && allTrelloItem.Count() > 0)
                {
                    foreach (var item in allTrelloItem)
                    {
                        _trelloCardItem.Add(new TrelloCardItem
                        {
                            Id = item.Fields[FieldsNames.TrtelloCardName.CardId].Value,
                            Name = item.Fields[FieldsNames.TrtelloCardName.CardName].Value,
                            Description = item.Fields[FieldsNames.TrtelloCardName.CardDescription].Value,
                            CardUpdatedDate = item.Fields[FieldsNames.TrtelloCardName.CardUpdatedDate].Value,
                            CardURL= item.Fields[FieldsNames.TrtelloCardName.CardURL].Value
                        });
                    }
                }
            }

            return _trelloCardItem;
        }

        public void SetCardAsDone(string cardId)
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

                    TrelloContext trelloContext = new TrelloContext();
                    trelloContext.SetCardAsDone(new TrelloConnector.Models.CardSearch()
                    {
                        ApiKey = apiKey,
                        Token = token,
                        BoardName = board,
                        DoneListName = doneListName
                    });

                    // Delete Sitecore Item 

                    Sitecore.Data.Items.Item cardToDelete = masterDB.GetItem(IDs.TrelloDataFolderId).Axes.GetDescendants()
                        .Where(t => t.TemplateID == TemplatesIDs.TrelloCardItemTemplateId)
                        .Where(t => t.Fields[FieldsNames.TrtelloCardName.CardMembers].Value.Contains(Sitecore.Context.User.Profile[FieldsNames.UserProfileFields.TrelloMemberName]))
                        .Where(t=>t.Fields[FieldsNames.TrtelloCardName.CardId].Value == cardId).FirstOrDefault();

                    if (cardToDelete != null)
                        cardToDelete.Recycle();
                }
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Error.LogError(ex.Message);
            }
        }
    }
}