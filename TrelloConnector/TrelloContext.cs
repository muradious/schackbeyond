using System;
using System.Collections.Generic;
using System.Linq;
using TrelloConnector.Interfaces;
using TrelloConnector.Models;
using TrelloNet;

namespace TrelloConnector
{
	public class TrelloContext
	{
		private ITrello _trello;

        private Sitecore.Data.Database master = Sitecore.Configuration.Factory.GetDatabase("master");
        private Sitecore.Data.ID trelloConfig = new Sitecore.Data.ID("{6A175091-968F-4E73-B5DD-AF66C99FA029}");

		/// <summary>
		/// Retrieve All Cards from a certain list in a predefined board
		/// </summary>
		/// <param name="searchModel"></param>
		/// <returns></returns>
		public List<ICard> GetTrelloItems(ApiModel searchModel)
		{
			List<ICard> result = new List<ICard>();
			try
			{
                _trello = new Trello(searchModel.ApiKey);
				_trello.Authorize(searchModel.Token);
				Board mainBoard = _trello.Boards.ForMe().DefaultIfEmpty().FirstOrDefault(x => x.Name == searchModel.BoardName);
				TrelloNet.List toDoList = _trello.Lists.ForBoard(mainBoard).DefaultIfEmpty().FirstOrDefault(x => x.Name == searchModel.ToDoListName);
				return GetCards(_trello.Cards.ForList(toDoList));
			}
			catch (Exception ex)
			{
				Sitecore.Diagnostics.Log.Error("Error retrieving Trello Items Details:\n" + ex.Message, ex);
			}
			return result;
		}

        

        private List<ICard> GetCards(IEnumerable<TrelloNet.Card> cards)
		{
			List<ICard> result = new List<ICard>();
			try
			{
				if (cards != null && cards.Any())
				{
					foreach (TrelloNet.Card card in cards)
					{
						List<MemberData> members = new List<MemberData>();
						foreach (string memberID in card.IdMembers)
						{
							members.Add(new MemberData { MemberID = memberID, MemberName = _trello.Members.WithId(memberID)?.Username });
						}

						//Map cards into a local format
						result.Add(new Models.Card
						{
							ID = card.Id,
							Name = card.Name,
							Date = Sitecore.DateUtil.ToIsoDate(card.DateLastActivity),
							Desc = card.Desc,
							URL = card.Url,
							Members = string.Join("|", members.Select(s => s.MemberName))
						});
					}
				}
			}
			catch (Exception ex)
			{
				Sitecore.Diagnostics.Log.Error("Error in Getting Card related data Details:\n" + ex.Message, ex);
			}
			return result;
		}

		/// <summary>
		/// Update a certain Trello Cards status to done
		/// </summary>
		/// <param name="searchModel"></param>
		public void SetCardAsDone(CardSearch searchModel)
		{
			try
			{
				_trello = new Trello(searchModel.ApiKey); //GetTrelloItem();
                _trello.Authorize(searchModel.Token);

				Board mainBoard = _trello.Boards.ForMe()?.FirstOrDefault(x => x.Name == searchModel.BoardName);

				TrelloNet.List doneList = _trello.Lists.ForBoard(mainBoard)?.FirstOrDefault(x => x.Name == searchModel.DoneListName);
				TrelloNet.Card targetCard = _trello.Cards.WithId(searchModel.CardID);

				_trello.Cards.Move(targetCard, doneList);
			}
			catch (Exception ex)
			{
				Sitecore.Diagnostics.Log.Error("Error setting Trello card as done Details:\n" + ex.Message, ex);
			}
		}

        public void SetCardAsDone(string cardID)
        {
            try
            {
                Sitecore.Data.Items.Item trelloConfigItem = master.GetItem(trelloConfig);
                Sitecore.Data.Fields.TextField apiKey = trelloConfigItem.Fields["App Key"];
                Sitecore.Data.Fields.TextField token = trelloConfigItem.Fields["Auth Token"];
                Sitecore.Data.Fields.TextField board = trelloConfigItem.Fields["Board Name"];
                Sitecore.Data.Fields.TextField doneListName = trelloConfigItem.Fields["Done List Name"];

                _trello = new Trello(apiKey.Value);
                _trello.Authorize(token.Value);
                Board mainBoard = _trello.Boards.ForMe()?.FirstOrDefault(x => x.Name == board.Value);

                TrelloNet.List doneList = _trello.Lists.ForBoard(mainBoard)?.FirstOrDefault(x => x.Name == doneListName.Value);
                TrelloNet.Card targetCard = _trello.Cards.WithId(cardID);

                _trello.Cards.Move(targetCard, doneList);
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Error setting Trello card as done Details:\n" + ex.Message, ex);
            }
        }
    }
}
