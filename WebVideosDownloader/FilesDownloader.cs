using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebVideosDownloader
{
    public class FilesDownloader
    {
        public DirectoryInfo? Directory { get; set; } = null;
        public Func<string, string> UriToFileName { get; set; } = url => url.Split('/').Last();

        public async Task DownloadFiles(IEnumerable<string> uris, int usedThreads = 3)
        {
            await Parallel.ForEachAsync(uris,
                            new ParallelOptions() { MaxDegreeOfParallelism = usedThreads },
                            (uri, token) => DownloadFile(uri, UriToFileName(uri), token));
        }

        public async ValueTask DownloadFile(string url, string fileName, CancellationToken token)
        {
            using HttpClient client = new();

            using Stream stream = await client.GetStreamAsync(url, token);

            if (!Directory!.Exists) Directory.Create();

            using Stream fs = File.Create($"{Directory.FullName}/{fileName}");
            await stream.CopyToAsync(fs, token);
        }
    }
}
