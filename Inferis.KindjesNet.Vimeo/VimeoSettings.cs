using System;
using System.Configuration;

namespace Inferis.KindjesNet.Vimeo
{
    public class VimeoSettings : IVimeoSettings
    {
        public string ApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["Vimeo_ApiKey"] ?? "";
            }
        }

        public string Secret
        {
            get
            {
                return ConfigurationManager.AppSettings["Vimeo_Secret"] ?? "";
            }
        }

        public string UserId
        {
            get
            {
                return ConfigurationManager.AppSettings["Vimeo_UserId"] ?? "";
            }
        }
    }
}
