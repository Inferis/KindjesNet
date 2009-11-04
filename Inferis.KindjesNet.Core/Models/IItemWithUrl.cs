using System;

namespace Inferis.KindjesNet.Core.Models
{
    public interface IItemWithUrl
    {
        DateTime DateForUrl { get; }
        string Slug { get; }
    }
}
