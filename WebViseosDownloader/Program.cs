// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Text.RegularExpressions;
using WebVideosDownloader;

Console.WriteLine("Hello, World!");

await FilesDownloader.DownloadFiles((await HtmlParser.Parse(@"https://sviyash-center.ru/videoteka/zhizn-po-metodike-a-sviyasha/akademiya-razumnoy-zhizni-2020/",
    @"'([a-zA-Z_\/:\.0-9]*\.mp4)'")).Select(match => match.Groups[1].Value),
    3,
    url => new($"E:/Видео/Академия разумной жизни/{url.Split('/').Last()}"));

Console.WriteLine("Completed");