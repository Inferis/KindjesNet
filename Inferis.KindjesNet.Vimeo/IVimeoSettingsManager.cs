using Inferis.API.Vimeo.Advanced.OAuth;

namespace Inferis.KindjesNet.Vimeo
{
    public interface IVimeoSettingsManager
    {
        string ConsumerKey { get; set; }
        string ConsumerSecret { get; set; }
        string AuthToken { get; set; }
        string AuthSecret { get; set; }
        string UserId { get; set; }

        IAuthorizedRequirements GetRequirements();
    }
}