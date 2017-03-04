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
    }
}