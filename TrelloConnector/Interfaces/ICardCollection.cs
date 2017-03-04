using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloConnector.Models;

namespace TrelloConnector.Interfaces
{
    public interface ICardCollection
    {
        string ID { get; set; }

        string Name { get; set; }

        string Desc { get; set; }

        List<MemberData> Members { get; set; }
    }
}
