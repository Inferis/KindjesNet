namespace Inferis.API.Vimeo.Advanced
{
    public interface IApiSettings
    {
        string ApiKey { get; }
        string BaseUri { get; }
        INetwork Network { get; }
    }
}
