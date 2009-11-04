using System;
using System.Collections.Generic;

namespace Inferis.KindjesNet.Core.Models
{
    public interface IFeedItem
    {
        int Order { get; }
        string Provider { get; }

        string Url { get; }
        string Title { get; }
        string Body { get; }
        DateTime Date { get; }
        IEnumerable<IFeedAttachment> Attachments { get; }

        string Color { get; }
        string Icon { get; }

        //bool CanComment { get; }
    }

}
