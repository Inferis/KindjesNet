using System;
using System.Configuration;
using System.Linq;
using Inferis.API.Vimeo.Advanced.OAuth;
using Inferis.KindjesNet.Core.Data;
using Inferis.KindjesNet.Vimeo.Models;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Vimeo
{
    public class VimeoSettingsManager : IVimeoSettingsManager
    {
        public VimeoSettings model;

        [Dependency]
        public IRepository Repository { protected get; set; }

        public string Secret
        {
            get
            {
                return ConfigurationManager.AppSettings["Vimeo_Secret"] ?? "";
            }
        }

        public string ConsumerKey
        {
            get
            {
                return Model.ConsumerKey;
            }
            set
            {
                PutModelValue(m => m.ConsumerSecret = value);
            }
        }

        public string ConsumerSecret
        {
            get
            {
                return Model.ConsumerSecret;
            }
            set
            {
                PutModelValue(m => m.ConsumerSecret = value);
            }
        }

        public string AuthToken
        {
            get
            {
                return Model.AuthToken;
            }
            set
            {
                PutModelValue(m => m.AuthToken = value);
            }
        }

        public string AuthSecret
        {
            get
            {
                return Model.AuthSecret;
            }
            set
            {
                PutModelValue(m => m.AuthSecret = value);
            }
        }

        public string UserId
        {
            get
            {
                return Model.UserId;
            }
            set
            {
                PutModelValue(m => m.UserId = value);
            }
        }

        public IAuthorizedRequirements GetRequirements()
        {
            return new AuthorizedRequirements(ConsumerKey, ConsumerSecret, AuthToken, AuthSecret);
        }


        private VimeoSettings Model
        {
            get
            {
                if (model == null)
                    model = Repository.Query<VimeoSettings>().FirstOrDefault() ?? new VimeoSettings();
                return model;
            }
        }

        private void PutModelValue(Action<VimeoSettings> modelSetter)
        {
            modelSetter(Model);
            Repository.SaveOrUpdate(model);
        }

    }
}
