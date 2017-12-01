using System;
using System.Threading;
using System.Threading.Tasks;
using MovieDownload;

namespace MovieSearch.iOS.ApiService
{
    public class DownloadImage
    {
        private readonly ImageDownloader _download;
        public DownloadImage(ImageDownloader downloader)
        {
            this._download = downloader;
        }

        public async Task<string> Download(string PosterPath)
        {
            var sourceToken = new CancellationTokenSource();
            var localPath = _download.LocalPathForFilename(PosterPath);
            if (_download.DoesPathExist(PosterPath))
            {
                sourceToken.Cancel();
            }
            await _download.DownloadImage(PosterPath, localPath, sourceToken.Token);
            return localPath;

        }
    }
}
