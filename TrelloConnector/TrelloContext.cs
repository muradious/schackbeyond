using System;
using System.Collections.Generic;
using System.Linq;
using Beyond.foundation.TrelloConnector.Interfaces;
using Beyond.foundation.TrelloConnector.Models;
using TrelloNet;

namespace Beyond.foundation.TrelloConnector
{
	public class TrelloContext
	{
		private ITrello _trello;
		private ApiModel _apiModel;

		public TrelloContext(ApiModel apiModel)
		{
			_apiModel = apiModel;
			_trello = new Trello(_apiModel.ApiKey);
			_trello.Authorize(_apiModel.Token);
		}
        
		/// <summary>
		/// Retrieve All Cards from a certain list in a predefined board
		/// </summary>
		/// <param name="searchModel"></param>
		/// <returns></returns>
		public List<ICard> GetTrelloItems()
		{
			List<ICard> result = new List<ICard>();
			try
			{                
				//Get current board
				Board mainBoard = _trello.Boards.ForMe().DefaultIfEmpty().FirstOrDefault(x => x.Name == _apiModel.BoardName);

				//Get the To Do list
				TrelloNet.List toDoList = _trello.Lists.ForBoard(mainBoard).DefaultIfEmpty().FirstOrDefault(x => x.Name == _apiModel.ToDoListName);

				//Return the To Do list's cards
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
						//Get Card Members
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
		public bool SetCardAsDone(string cardID)
		{
			try
			{
				//Get the current board
				Board mainBoard = _trello.Boards.ForMe()?.FirstOrDefault(x => x.Name == _apiModel.BoardName);

				//Get the Done List by its name
				TrelloNet.List doneList = _trello.Lists.ForBoard(mainBoard)?.FirstOrDefault(x => x.Name == _apiModel.DoneListName);
				//Get target card by its ID
				TrelloNet.Card targetCard = _trello.Cards.WithId(cardID);

				//Move Card into Done List
				_trello.Cards.Move(targetCard, doneList);
			}
			catch (Exception ex)
			{
				Sitecore.Diagnostics.Log.Error("Error setting Trello card as done Details:\n" + ex.Message, ex);
				return false;
			}

			return true;
		}
    }
}
