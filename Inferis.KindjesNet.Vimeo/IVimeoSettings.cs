namespace Inferis.KindjesNet.Vimeo
{
    public interface IVimeoSettings
    {
        string ApiKey { get; }
        string Secret { get; }
        string UserId { get; }
    }
}