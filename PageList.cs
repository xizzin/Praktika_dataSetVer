using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika_5DataSetVer
{
    internal class PageList
    {
        private static AdminPage adminPage = new AdminPage();
        public static AdminPage adminPagePub
        {
            get { return adminPage; }

        }

        private static LoginPage loginPage = new LoginPage();
        public static LoginPage loginPagePub { get { return loginPage; } }
        

        private static NoLogin nologinPage = new NoLogin();
        public static NoLogin nologinPagePub { get { return nologinPage; } }

        private static ControlPagexaml controlPage = new ControlPagexaml();
        public static ControlPagexaml controlPagePub {  get { return controlPage; } }
        
        private static HandlersPagexaml handlersPage = new HandlersPagexaml();

        public static HandlersPagexaml handlerspagePub { get { return handlersPage; } }
    }
}
