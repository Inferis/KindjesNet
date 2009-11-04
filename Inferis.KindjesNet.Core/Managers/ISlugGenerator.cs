namespace Inferis.KindjesNet.Core.Managers
{
    public interface ISlugGenerator {
        string GenerateSlug(string source);
        string RemoveAccent(string txt);
    }
}