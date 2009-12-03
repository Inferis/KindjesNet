using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook.Rest;
using Facebook.Session;
using Inferis.KindjesNet.Core.Managers;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Core.Connections.Facebook
{
    public class FacebookConnection : IFacebookConnection
    {
        private Api api;
        private ConnectSession session;

        [Dependency]
        public ISettingsManager SettingsManager { get; set; }

        public string ApiKey
        {
            get { return SettingsManager.For<FacebookConnection>().Get<string>("ApiKey"); }
            set { SettingsManager.For<FacebookConnection>().Set("ApiKey", value); }
        }

        public string ApiSecret
        {
            get { return SettingsManager.For<FacebookConnection>().Get<string>("ApiSecret"); }
            set { SettingsManager.For<FacebookConnection>().Set("ApiSecret", value); }
        }

        public Api Api
        {
            get
            {
                EnsureApi();
                return api;
            }
        }

        public bool IsConnected()
        {
            EnsureSession();
            return session.IsConnected();
        }

        public void Login()
        {
            EnsureSession();
            session.Login();
        }

        public void Logout()
        {
            EnsureSession();
            session.Logout();
        }

        private void EnsureSession()
        {
            if (session != null) return;
            session = new ConnectSession(ApiKey, ApiSecret);
        }

        private void EnsureApi()
        {
            if (api != null) return;
            EnsureSession();
            api = new Api(session);
        }

    }
}
