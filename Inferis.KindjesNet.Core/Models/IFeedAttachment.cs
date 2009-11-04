namespace Inferis.KindjesNet.Core.Models
{
    public interface IFeedAttachment {
        string Thumbnail { get; }
        string Render();
    }
}
