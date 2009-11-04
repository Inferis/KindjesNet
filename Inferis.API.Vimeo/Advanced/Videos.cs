using System;
using System.Collections.Generic;
using System.Linq;
using Inferis.API.Vimeo.Advanced.Responses;
using Inferis.Core;

namespace Inferis.API.Vimeo.Advanced
{
    public class Videos : ApiBaseWithSecret
    {
        public Videos(string key, string secret)
            : base(key, secret)
        {

        }

        public Page<VideoSummary> GetAll(string userId, params Action<GetAllOptions>[] options)
        {
            var parameters = Options.Merge(options);
            parameters["user_id"] = userId;

            var xml = Signed.WithSecret(Secret).Call(MethodName(), parameters);
            var videos = xml.Descendants("videos").First();

            var result = new Page<VideoSummary>(
                int.Parse(videos.Attribute("page").Value),
                int.Parse(videos.Attribute("perpage").Value),
                int.Parse(videos.Attribute("total").Value));

            foreach (var video in videos.Descendants("video")) {
                var item = MapFromXml<VideoSummary>(video);
                result.Add(item);
            }

            return result;
        }



        public class GetAllOptions : Options
        {
            [ApiName("sort")]
            public Sort? Sort { get; set; }

            [ApiName("page")]
            public int? Page { get; set; }

            [ApiName("per_page")]
            public int? PerPage { get; set; }
        }


    }
}
