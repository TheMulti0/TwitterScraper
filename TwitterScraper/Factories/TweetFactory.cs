using System;
using System.Linq;
using HtmlAgilityPack;

namespace TwitterScraper
{
    public static class TweetFactory
    {
        private static DateTime _unixEpoch = new DateTime(1970, 1, 1);

        public static Tweet CreateTweet(HtmlNode tweet, HtmlNode profile)
        {
            var id = long.Parse(
                tweet.GetAttributeValue("data-item-id", "0"));

            Author author = AuthorFactory.CreateAuthor(profile);
            
            var relativeUrl = profile.GetAttributeValue("data-permalink-path", null);
            
            var date = GetDate(tweet);
            

            var text = tweet.FirstDescendantWithClassOrDefault("tweet-text").InnerText;

            var interactions = tweet.Descendants()
                .Where(node => node.HasClass("ProfileTweet-actionCountForAria"));

            return new Tweet
            {
                Id = id,
                Author = author,
                Url = $"{TwitterConstants.BaseAddress}{relativeUrl}",
                PublishDate = date,
                Text = text
            };
        }
        
        private static DateTime GetDate(HtmlNode tweet)
        {
            string ticksString = tweet
                .FirstDescendantWithClassOrDefault("_timestamp")
                .GetAttributeValue("data-time-ms", "0");

            long ticks = long.Parse(ticksString);
            
            return _unixEpoch.AddMilliseconds(ticks).ToLocalTime();
        }
    }
}