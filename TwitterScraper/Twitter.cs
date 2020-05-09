using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace TwitterScraper
{
    public class Twitter
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
            _client.DefaultRequestHeaders.Add("Referer", "https://twitter.com/realDonaldTrump");
            _client.DefaultRequestHeaders.Add(
                "User-Agent",
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/603.3.8 (KHTML, like Gecko) Version/10.1.2 Safari/603.3.8");
            _client.DefaultRequestHeaders.Add("X-Twitter-Active-User", "yes");
            _client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            _client.DefaultRequestHeaders.Add("Accept-Language", "en-US");
        }

        public async Task Operate()
        {
            var page = await JsonSerializer
                .DeserializeAsync<TwitterResponsePage>(await GetResponseStream());

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(page.ItemsHtml);
            
            var firstTweetElement = htmlDocument.DocumentNode.FirstDescendantWithClass("stream-item"); // Throw exception if null
            var firstProfileElement = htmlDocument.DocumentNode.FirstDescendantWithClass("js-profile-popup-actionable");
            
            var firstTweet = TweetFactory.CreateTweet(firstTweetElement, firstProfileElement);
        }

        private async Task<Stream> GetResponseStream()
        {
            HttpResponseMessage response = await _client.GetAsync(
                "https://twitter.com/i/profiles/show/realDonaldTrump/timeline/tweets?");
            return await response.Content.ReadAsStreamAsync();
        }
    }
}