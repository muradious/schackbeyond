﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyond.foundation.TrelloConnector.Models
{
    public class CardSearch
    {
        public string BoardName { get; set; }
        public string DoneListName { get; set; }
        public string CardID { get; set; }
        public string ApiKey { get; set; }
        public string Token { get; set; }
    }
}
