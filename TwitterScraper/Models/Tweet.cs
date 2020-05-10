using System;

namespace TwitterScraper
{
    /// <summary>
    /// Represents a tweet
    /// </summary>
    public class Tweet
    {
        /// <summary>
        /// Tweet ID
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        /// Tweet URL
        /// </summary>
        public string Url { get; internal set; }

        /// <summary>
        /// Tweet Author
        /// </summary>
        public User Author { get; internal set; }

        /// <summary>
        /// Is this tweet a retweet
        /// </summary>
        public bool IsRetweet { get; internal set; }

        /// <summary>
        /// Tweet publish date
        /// </summary>
        public DateTime PublishDate { get; internal set; }

        /// <summary>
        /// Tweet content
        /// </summary>
        public string Text { get; internal set; }

        /// <summary>
        /// Tweet replies count
        /// </summary>
        public int RepliesCount { get; internal set; }

        /// <summary>
        /// Tweet retweets count
        /// </summary>
        public int RetweetsCount { get; internal set; }

        /// <summary>
        /// Tweet likes count
        /// </summary>
        public int LikesCount { get; internal set; }
    }
}