using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook.Rest;

namespace Inferis.KindjesNet.Core.Connections.Facebook
{
    public interface IFacebookConnection
    {
        string ApiKey { get; set;  }
        string ApiSecret { get; set; }
        Api Api { get; }

        bool IsConnected();

        void Login();
        void Logout();
    }
}
