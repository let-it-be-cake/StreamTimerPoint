using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace Google.Apis.YouTube.Samples
{
    internal class Search
    {
        static async Task Main(string[] args)
        {
            try
            {
                await new Search().Run();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }

        private async Task<UserCredential> AuthorizeAsync()
        {
            using var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read);

            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                (await GoogleClientSecrets.FromStreamAsync(stream)).Secrets,
                new string[] {
                    YouTubeService.Scope.YoutubeReadonly,
                    YouTubeService.Scope.Youtube,
                    YouTubeService.Scope.YoutubeForceSsl,
                },
                "user",
                CancellationToken.None
            );

            return credential;
        }

        private async Task Run()
        {
            //var youtubeService = new YouTubeService(new BaseClientService.Initializer
            //{
            //    HttpClientInitializer = await AuthorizeAsync(),
            //    //ApiKey = "AIzaSyCVHeXLIkPuVFYuFAOAl-zpN_8ZY4kB98w",
            //    ApplicationName = GetType().ToString(),
            //});

            var youtubeService = new YouTubeService();

            //youtubeService

            var qq = youtubeService.LiveBroadcasts.List(
                new Util.Repeatable<string>(new[]
                {
                    //"snippet",
                    //"cdn",
                    //"contentDetails",
                    "status"
                }));

            qq.Mine = true;

            var w = new LiveStreamsResource.ListRequest(new YouTubeService(),
                new Util.Repeatable<string>(new[]
                {
                    "id",
                    "snippet",
                    "cdn",
                    "contentDetails",
                    "status"
                }));

            //var w = youtubeService.LiveBroadcasts.List(
            //    new Util.Repeatable<string>(new[]
            //    {
            //        "snippet",
            //        //"cdn",
            //        //"contentDetails",
            //        //"status"
            //    }));
            w.Mine = true;

            w.Credential = await AuthorizeAsync();
            //w.AddCredential(await AuthorizeAsync());

            try
            {
                //var t = qq.Execute();
                //Stopwatch stopwatch = new Stopwatch();
                //Console.WriteLine("Execution using Thread");
                //stopwatch.Start();

                //new Thread(() =>
                //{
                //    for (int i = 0; i < 10; i++)
                //    {
                //        var e = w.Execute();
                //        Thread.Sleep(100);
                //        Console.WriteLine("First " + i);
                //    }
                //}).Join();

                //stopwatch.Stop();
                //Console.WriteLine("Time consumed by MethodWithThread is : " +
                //                     stopwatch.ElapsedTicks.ToString());

                //stopwatch.Reset();
                //Console.WriteLine("Execution using Thread Pool");
                //stopwatch.Start();

                //new Thread(async () =>
                //{
                //    for (int i = 0; i < 10; i++)
                //    {
                //        var e = await w.ExecuteAsync();
                //        await Task.Delay(100);
                //        Console.WriteLine("Second " + i);
                //    }


                //}).Join();


                //stopwatch.Stop();
                //Console.WriteLine("Time consumed by MethodWithThreadPool is : " +
                //                     stopwatch.ElapsedTicks.ToString());


                var e = w.Execute();
                if (qq == null)
                {
                    return;
                }

                //var searchListRequest = youtubeService.Search.List("snippet");
                //searchListRequest.Q = "Google"; // Replace with your search term.
                //searchListRequest.MaxResults = 50;

                //// Call the search.list method to retrieve results matching the specified query term.
                //var searchListResponse = await searchListRequest.ExecuteAsync();

                //List<string> videos = new List<string>();
                //List<string> channels = new List<string>();
                //List<string> playlists = new List<string>();

                //// Add each result to the appropriate list, and then display the lists of
                //// matching videos, channels, and playlists.
                //foreach (var searchResult in searchListResponse.Items)
                //{
                //    switch (searchResult.Id.Kind)
                //    {
                //        case "youtube#video":
                //            videos.Add(string.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                //            break;

                //        case "youtube#channel":
                //            channels.Add(string.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                //            break;

                //        case "youtube#playlist":
                //            playlists.Add(string.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                //            break;
                //    }
                //}

                //Console.WriteLine(string.Format("Videos:\n{0}\n", string.Join("\n", videos)));
                //Console.WriteLine(string.Format("Channels:\n{0}\n", string.Join("\n", channels)));
                //Console.WriteLine(string.Format("Playlists:\n{0}\n", string.Join("\n", playlists)));


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}