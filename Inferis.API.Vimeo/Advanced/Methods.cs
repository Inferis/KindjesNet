namespace Inferis.API.Vimeo.Advanced
{
    public class Vimeo
    {
        private readonly string apiKey;
        private readonly string secret;
        private INetwork network; 
        private Test test;
        private Auth auth;
        private Videos videos;

        public INetwork Network { get; private set; }

        public Vimeo(string apiKey, string secret)
        {
            this.apiKey = apiKey;
            this.secret = secret;
            Network = API.Vimeo.Network.Default;
        }

        public Vimeo(string apiKey, string secret, INetwork network)
        {
            this.apiKey = apiKey;
            this.secret = secret;
            this.network = network;
        }

        public Auth Auth
        {
            get
            {
                return auth ?? (auth = new Auth(apiKey, secret) { Network = network });
            }
        }

        public Test Test
        {
            get
            {
                return test ?? (test = new Test(apiKey, secret) { Network = network });
            }
        }

        public Videos Videos
        {
            get
            {
                return videos ?? (videos = new Videos(apiKey, secret) { Network = network });
            }
        }

    }
}
