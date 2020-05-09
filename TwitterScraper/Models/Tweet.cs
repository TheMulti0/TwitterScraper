using System;

namespace TwitterScraper
{
    public class Tweet
    {
        public long Id { get; internal set; }

        public string Url { get; internal set; }

        public string AuthorUsername { get; internal set; }

        public bool IsRetweet { get; internal set; }

        public DateTime PublishDate { get; internal set; }

        public string Text { get; internal set; }

        public int RepliesCount { get; internal set; }

        public int RetweetsCount { get; internal set; }

        public int LikesCount { get; internal set; }
    }
}