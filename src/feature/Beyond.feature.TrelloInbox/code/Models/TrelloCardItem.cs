using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Beyond.feature.TrelloInbox.Models
{
    internal class TrelloCardItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CardUpdatedDate { get; set; }
        public string CardURL { get; set; }
    }
}