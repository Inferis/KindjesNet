namespace Inferis.KindjesNet.Core.Models
{
    public interface ISpider
    {
        string Name { get; }
        void Execute();
    }
}
