using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_SeverConfig
    {
        public static List<Server_Configs> GetServerList(string userid)
        {
           return DL_ServerConfig.GetSeverList(userid);
        }

        public static bool  InsertSever(Server_Configs server)
        {
            return DL_ServerConfig.InsertSeverConfig(server);
        }

        public static bool UpdateServer(Server_Configs server)
        {
            return DL_ServerConfig.UpdateSeverConfig(server);
        }
    }
}
