﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloConnector.Models
{
    public class ApiModel
    {
        public string MyProperty { get; set; }
        public string BoardName { get; set; }
        public string ToDoListName { get; set; }
        public string DoneListName { get; set; }
        public string ApiKey { get; set; }
        public string Token { get; set; }
    }
}
