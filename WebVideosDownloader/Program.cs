// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Text.RegularExpressions;
using WebVideosDownloader;

Console.WriteLine("Hello, World!");

FilesDownloader downloader = new();

foreach (var item in new DownloadInfo[]
{
    new(){
        Path=@"https://sviyash-center.ru/videoteka/videopraktikumy-proshcheniya/effektivnoe-proshchenie/",
        Directory="E:/Видео/Эффективное прощение"
    }
})
{
    downloader.Directory = new(item.Directory);
    await downloader.DownloadFiles((await HtmlParser.Parse(item.Path,
    @"'([a-zA-Z_\/:\.0-9\%]*\.mp4)'")).Select(match => match.Groups[1].Value));
}

Console.WriteLine("Completed");

class DownloadInfo
{
    public string Path, Directory;
}