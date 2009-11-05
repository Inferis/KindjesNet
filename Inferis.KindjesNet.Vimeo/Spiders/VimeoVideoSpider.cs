using System.ComponentModel.Composition;
using Inferis.API.Vimeo;
using Inferis.KindjesNet.Core.Models;
using Inferis.KindjesNet.Vimeo.Managers;
using Inferis.KindjesNet.Vimeo.Models;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Vimeo.Spiders
{
    [Export(typeof(ISpider))]
    public class VimeoVideoSpider : ISpider
    {
        [Import, Dependency]
        public IVimeoSettings Settings { get; set; }

        [Import, Dependency]
        public IVimeoManager VimeoManager { get; set; }

        public string Name
        {
            get { return GetType().Name; }
        }

        public void Execute()
        {
            var vimeo = new API.Vimeo.Advanced.Vimeo(Settings.ApiKey, Settings.Secret);
            for (var currentPage = 1; ; currentPage++) {
                // get all videos
                bool foundNew = false;
                var page = vimeo.Videos.GetAll(Settings.UserId, opt => opt.Sort = Sort.Newest, opt => opt.Page = currentPage, opt => opt.PerPage = 50);

                foreach (var item in page) {
                    if (!VimeoManager.VideoExistsWithVimeoId(item.Id)) {
                        // new video!
                        foundNew = true;

                        var video = new VimeoVideo() {
                            IsHighDefinition = item.IsHd,
                            Title = item.Title,
                            Caption = "",
                            UploadDate = item.UploadDate,
                            Duration = 0,
                            Height = 0,
                            Width = 0,
                            VimeoId = item.Id,
                            VimeoOwnerId = item.Owner,
                            Slug = VimeoManager.Slugify(item.Title, item.UploadDate, null)
                        };
                        VimeoManager.SaveVideo(video);
                    }
                }

                if (!foundNew || page.OnThisPage == 0 || page.OnThisPage < page.PerPage || page.OnThisPage == page.Total)
                    break;
            }
        }
    }
}

