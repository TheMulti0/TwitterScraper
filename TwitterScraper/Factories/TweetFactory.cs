using System;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace TwitterScraper
{
    public static class TweetFactory
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);
        private const string InteractionPattern = "[a-zA-Z]+";
        private const string NumberPattern = "[0-9]+";

        public static Tweet CreateTweet(HtmlNode tweet, HtmlNode profile)
        {
            var id = long.Parse(
                tweet.GetAttributeValue("data-item-id", "0"));

            Author author = AuthorFactory.CreateAuthor(profile);
            
            var relativeUrl = profile.GetAttributeValue("data-permalink-path", null);

            string retweetId = profile.GetAttributeValue("date-retweet-id", null);
            var isRetweet = retweetId != null;
            
            var publishDate = GetPublishDate(tweet);

            var text = tweet.FirstDescendantWithClassOrDefault("tweet-text").InnerText;

            var interactions = tweet
                .WhereDescendantWithClass("ProfileTweet-actionCountForAria")
                .Select(node => node.InnerText)
                .ToDictionary(
                    s => Regex.Match(s, InteractionPattern).Value,
                    s => int.Parse(
                        Regex.Match(s.Replace(",", ""), NumberPattern).Value));

            return new Tweet
            {
                Id = id,
                Author = author,
                Url = $"{TwitterConstants.BaseAddress}{relativeUrl}",
                IsRetweet = isRetweet,
                PublishDate = publishDate,
                Text = text,
                RepliesCount = interactions["replies"],
                RetweetsCount = interactions["retweets"],
                LikesCount = interactions["likes"]
            };
        }
        
        private static DateTime GetPublishDate(HtmlNode tweet)
        {
            string ticksString = tweet
                .FirstDescendantWithClassOrDefault("_timestamp")
                .GetAttributeValue("data-time-ms", "0");

            long ticks = long.Parse(ticksString);
            
            return UnixEpoch.AddMilliseconds(ticks).ToLocalTime();
        }
    }
}