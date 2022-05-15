using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebVideosDownloader
{
    public class HtmlParser
    {
        public static async Task<IEnumerable<Match>> Parse(string url, Regex regex)
        {
            HttpClient client = new();
            string html = await client.GetStringAsync(url);

            return regex.Matches(html);
        }

        public static Task<IEnumerable<Match>> Parse(string url, string pattern) =>
            Parse(url, new Regex(pattern));
    }
}
