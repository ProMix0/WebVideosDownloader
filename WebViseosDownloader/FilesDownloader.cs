using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebVideosDownloader
{
    public static class FilesDownloader
    {
        public static async Task DownloadFiles(IEnumerable<string> uris, int usedThreads, Func<string, FileInfo> destination)
        {
            await Parallel.ForEachAsync(uris,
                            new ParallelOptions() { MaxDegreeOfParallelism = usedThreads },
                            (uri, token) => DownloadFile(uri, destination(uri), token));
        }

        public static async ValueTask DownloadFile(string url, FileInfo file, CancellationToken token)
        {
            HttpClient client = new();

            Stream stream = await client.GetStreamAsync(url, token);

            using Stream fs = file.Create();
            await stream.CopyToAsync(fs, token);
        }
    }
}
