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
    public static class TrelloHelper
    {
        /// <summary>
        /// initializing the connection with Trello using the App key and the user token
        /// </summary>
        /// <param name="trello"></param>
        /// <param name="apiKey"></param>
        /// <param name="token"></param>
        public static void InitializeTrello(this ITrello trello, string apiKey, string token)
        {
            trello = new Trello(apiKey); // Application API Key
            trello.Authorize(token); // User Token
        }

        /// <summary>
        /// Retrieve All Cards from a certain list in a predefined board
        /// </summary>
        /// <param name="trello"></param>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public static IEnumerable<ICardCollection> GetTrelloItems(this ITrello trello, ApiModel searchModel)
        {            
            try
            {
                Board mainBoard = trello.Boards.ForMe().DefaultIfEmpty().FirstOrDefault(x => x.Name == searchModel.BoardName);
                TrelloNet.List toDoList = trello.Lists.ForBoard(mainBoard).DefaultIfEmpty().FirstOrDefault(x => x.Name == searchModel.ToDoListName);
                return GetCards(trello,trello.Cards.ForList(toDoList));
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Error retrieving Trello Items Details:\n" + ex.Message,ex);
                return null;
            }
        }

        public static IEnumerable<ICardCollection> GetCards(ITrello trello, IEnumerable<TrelloNet.Card> cards)
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

        /// <summary>
        /// Update a certain Trello Cards status to done
        /// </summary>
        /// <param name="trello"></param>
        /// <param name="searchModel"></param>
        public static void SetCardAsDone(this ITrello trello, CardSearch searchModel)
        {
            try
            {                
                Board mainBoard = trello.Boards.ForMe()?.FirstOrDefault(x => x.Name == searchModel.BoardName);
                TrelloNet.List doneList = trello.Lists.ForBoard(mainBoard)?.FirstOrDefault(x => x.Name == searchModel.DoneListName);
                Card targetCard = trello.Cards.WithId(searchModel.CardID);
                trello.Cards.Move(targetCard, doneList);
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Error setting Trello card as done Details:\n" + ex.Message, ex);
            }
        }

    }
}
