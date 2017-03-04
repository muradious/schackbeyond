using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloConnector.Interfaces
{
    public interface IMemberData
    {
        string MemberID { get; set; }
        string MemberName { get; set; }
    }
}
