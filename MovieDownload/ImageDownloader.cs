using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDownload
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public class ImageDownloader
    {
        private IImageStorage _imageStorage;
        private Dictionary<string,string> _paths;

        public ImageDownloader(IImageStorage imageStorage)
        {
            this._paths = new Dictionary<string, string>();
            this._imageStorage = imageStorage;
        }

        public string LocalPathForFilename(string remoteFilePath)
        {
            if (remoteFilePath == null)
            {
                return string.Empty;
            }
            if (DoesPathExist(remoteFilePath))
            {
                return _paths.GetValueOrDefault(remoteFilePath);
            }

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string localPath = Path.Combine(documentsPath, remoteFilePath.TrimStart('/'));
            return localPath;
                
        
        }

        public async Task DownloadImage(string remoteFilePath, string localFilePath, CancellationToken token)
        {
            if (!DoesPathExist(remoteFilePath))
            {
                var fileStream = new FileStream(
                     localFilePath,
                     FileMode.Create,
                     FileAccess.Write,
                     FileShare.None,
                     short.MaxValue,
                     true);
                try
                {
                    await this._imageStorage.DownloadAsync(remoteFilePath, fileStream, token);
                    _paths.Add(remoteFilePath, localFilePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

        }

        public bool DoesPathExist(string remotePath)
        {
            if (_paths.ContainsKey(remotePath)){
                return true;
            }
            return false;
        }
    }
}
