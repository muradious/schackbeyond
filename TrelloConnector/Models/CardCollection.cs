using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloConnector.Interfaces;

namespace TrelloConnector.Models
{
    public class CardCollection:ICardCollection
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public List<MemberData> Members { get; set; }
    }
}
