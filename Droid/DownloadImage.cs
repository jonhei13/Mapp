
using MovieDownload;
using System.Threading.Tasks;
using System.Threading;

namespace MovieSearch.Droid
{
    class DownloadImage
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
            return PosterPath;

        }
    }
}