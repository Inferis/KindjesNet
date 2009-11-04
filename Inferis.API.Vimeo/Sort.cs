using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inferis.Core;

namespace Inferis.API.Vimeo
{
    public enum Sort
    {
        [ApiValue("newest")]
        Newest,

        [ApiValue("oldest")]
        Oldest,

        [ApiValue("most_played")]
        MostPlayed,

        [ApiValue("most_commented")]
        MostCommented,

        [ApiValue("most_liked")]
        MostLiked
    }
}
