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
            var text = tweet.FirstDescendantWithClassOrDefault("tweet-text").InnerText;

            var id = long.Parse(
                tweet.GetAttributeValue("data-item-id", "0"));

            var url = profile.GetAttributeValue("data-permalink-path", null);

            var date = GetDate(tweet);

            var interactions = tweet.Descendants()
                .Where(node => node.HasClass("ProfileTweet-actionCountForAria"));

            return new Tweet
            {
                Id = id,
                Url = url,
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