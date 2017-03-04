using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloConnector.Interfaces;

namespace TrelloConnector.Models
{
    public class MemberData:IMemberData
    {
        public string MemberID { get; set; }
        public string MemberName { get; set; }

    }
}
