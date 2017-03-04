using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Beyond.feature.TrelloInbox.Constants
{
    internal static class FieldsNames
    {
        internal static class TrtelloCardName
        {
            internal static string CardId = "Card ID";
            internal static string CardName = "Card Name";
            internal static string CardDescription = "Card Description";
            internal static string CardMembers = "Card Members";
            internal static string CardUpdatedDate = "Card Updated Date";
            internal static string CardURL = "Card URL";
        }

        internal static class UserProfileFields
        {
            internal static string TrelloMemberName = "Trello Member Username";
        }

        internal static class TrelloSettings
        {
            internal static string AppKey = "App Key";
            internal static string AuthToken = "Auth Token";
            internal static string BoardName = "Board Name";
            internal static string DoneListName = "Done List Name";
        }
    }
}