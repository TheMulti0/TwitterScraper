using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;

namespace TwitterScraper
{
    public class Twitter : ITwitter
    {
        private readonly HttpClient _client;

        public Twitter(HttpClient client = null)
        {
            _client = client ?? new HttpClient();

            PrepareClientHeaders();
        }

        private void PrepareClientHeaders()
        {
            _client.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
            _client.DefaultRequestHeaders.Add(
                "User-Agent",
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/603.3.8 (KHTML, like Gecko) Version/10.1.2 Safari/603.3.8");
            _client.DefaultRequestHeaders.Add("X-Twitter-Active-User", "yes");
            _client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            _client.DefaultRequestHeaders.Add("Accept-Language", "en-US");
        }

        public async Task<IEnumerable<Tweet>> GetTweets(string query, int pages = 1)
        {
            var totalTweets = new List<Tweet>();

            long lastItemId = 0;
            int remainingPages = pages;
            
            while (remainingPages > 0)
            {
                HtmlDocument html = await GetPageHtml(query, lastItemId);

                var tweets = GetTweetsAndProfilesNodes(html.DocumentNode)
                    .Select(nodes => TweetFactory.CreateTweet(nodes.tweetNode, nodes.profileNode))
                    .Where(tweet => tweet != null)
                    .ToList();
                
                totalTweets.AddRange(tweets);

                lastItemId = tweets.Last().Id;
                remainingPages--;
            }

            return totalTweets;
        }

        private async Task<HtmlDocument> GetPageHtml(string query, long lastItemId)
        {
            _client.DefaultRequestHeaders.Remove("Referer");
            _client.DefaultRequestHeaders.Add("Referer", $"{TwitterConstants.BaseAddress}/realDonaldTrump");

            var page = await JsonSerializer
                .DeserializeAsync<TwitterResponsePage>(await GetResponseStream(query, lastItemId));

            var html = new HtmlDocument();
            html.LoadHtml(page.ItemsHtml);
            return html;
        }

        private async Task<Stream> GetResponseStream(string query, long lastItemId)
        {
            query = HttpUtility.UrlEncode(query);
            
            string requestUri = query.StartsWith("@") 
                ? $"{TwitterConstants.BaseAddress}/i/profiles/show/{query}/timeline/tweets?" 
                : $"{TwitterConstants.BaseAddress}/i/search/timeline?f=tweets&vertical=default&q={query}&src=tyah&reset_error_state=false&";
            
            if (lastItemId != 0)
            {
                requestUri += $"max_position={lastItemId}";
            }
            
            HttpResponseMessage response = await _client.GetAsync(requestUri);
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<(HtmlNode tweetNode, HtmlNode profileNode)> GetTweetsAndProfilesNodes(
            HtmlNode root)
        {
            var tweetNodes = root.WhereDescendantWithClass("stream-item");
            var profileNodes = root.WhereDescendantWithClass("js-profile-popup-actionable");
            return tweetNodes.Zip(
                profileNodes,
                (tweetNode, profileNode) => (tweetNode, profileNode));
        }
    }
}