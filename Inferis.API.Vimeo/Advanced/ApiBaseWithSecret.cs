namespace Inferis.API.Vimeo.Advanced
{
    public abstract class ApiBaseWithSecret : ApiBase
    {
        protected ApiBaseWithSecret(string key, string secret) : base(key)
        {
            Secret = secret;
        }

        public string Secret { get; set; }
    }
}
