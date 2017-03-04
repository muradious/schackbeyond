using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore;
using TrelloNet;
using TrelloConnector.Utilities;
using TrelloConnector.Models;
using TrelloConnector.Interfaces;

namespace TrelloConnector
{
    public class TrelloHelper
    {
        public ITrello trello;
        public IEnumerable<ICardCollection> GetTrelloItems(ApiModel searchModel)
        {
            //initializing the connection with Trello using the App key and the user token
            try
            {
                trello = new Trello(searchModel.ApiKey);
                trello.Authorize(searchModel.Token);
                Board mainBoard = trello.Boards.ForMe().DefaultIfEmpty().FirstOrDefault(x => x.Name == searchModel.BoardName);
                TrelloNet.List toDoList = trello.Lists.ForBoard(mainBoard).DefaultIfEmpty().FirstOrDefault(x => x.Name == searchModel.ToDoListName);
                return GetCards(trello.Cards.ForList(toDoList));
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Error retrieving Trello Items Details:\n" + ex.Message,ex);
                return null;
            }
        }

        public IEnumerable<ICardCollection> GetCards(IEnumerable<TrelloNet.Card> cards)
        {
            List<CardCollection> result = new List<CardCollection>();
            try
            {
                if (cards != null && cards.Any())
                {
                    foreach (Card card in cards)
                    {
                        List<MemberData> members = new List<MemberData>();
                        foreach (string memberID in card.IdMembers)
                        {
                            members.Add(new MemberData
                            {
                                MemberID = memberID,
                                MemberName = trello.Members.WithId(memberID)?.Username
                            }
                            );
                        }
                        result.Add(new CardCollection
                        {
                            ID = card.Id,
                            Name = card.Name,
                            Desc = card.Desc,
                            Members = members
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Error in Getting Card related data Details:\n"+ex.Message,ex);
            }
            return result;
        }

    }
}
