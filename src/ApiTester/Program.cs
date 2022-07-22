using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace Google.Apis.YouTube.Samples
{
    /// <summary>
    /// YouTube Data API v3 sample: search by keyword.
    /// Relies on the Google APIs Client Library for .NET, v1.7.0 or higher.
    /// See https://developers.google.com/api-client-library/dotnet/get_started
    ///
    /// Set ApiKey to the API key value from the APIs & auth > Registered apps tab of
    ///   https://cloud.google.com/console
    /// Please ensure that you have enabled the YouTube Data API for your project.
    /// </summary>
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
                new string[] { YouTubeService.Scope.YoutubeReadonly },
                "user",
                CancellationToken.None
            );

            return credential;
        }

        private async Task Run()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer
            {
                HttpClientInitializer = await AuthorizeAsync(),
                //ApiKey = "AIzaSyCVHeXLIkPuVFYuFAOAl-zpN_8ZY4kB98w",
                ApplicationName = GetType().ToString(),
            });

            var qq = youtubeService.LiveBroadcasts;

            var w = youtubeService.LiveStreams.List(
                new Util.Repeatable<string>(new[]
                {
                    "snippet",
                    "cdn",
                    "contentDetails",
                    "status"
                }));
            w.Mine = true;

            try
            {
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