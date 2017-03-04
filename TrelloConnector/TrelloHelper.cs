using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore;
using TrelloNet;
using TrelloConnector.Utilities;

namespace TrelloConnector
{
    public class TrelloHelper
    {
        public void GetTrelloItems(string token)
        {
            //Key used by Trello to connect to our app
            var apiKey = Sitecore.Configuration.Settings.GetSetting(Utilities.Constants.ApiKey);
            //Name of our Trello application
            var appName = Sitecore.Configuration.Settings.GetSetting(Utilities.Constants.AppName);
            //initializing the connection with Trello using the App key and the user token
            ITrello trello = new Trello(apiKey);            
            trello.Authorize(token);
            Board mainBoard = trello.Boards.ForMe().DefaultIfEmpty().FirstOrDefault();
            IEnumerable<TrelloNet.List> mainLists = trello.Lists.ForBoard(mainBoard);
            foreach(TrelloNet.List list in mainLists)
            {
                IEnumerable<TrelloNet.Card> cards = trello.Cards.ForList(list);
                foreach (Card card in cards)
                {
                    
                }
            }
            
        }
    }
}
