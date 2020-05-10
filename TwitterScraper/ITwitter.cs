using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TwitterScraper
{
    /// <summary>
    /// Twitter provider
    /// </summary>
    public interface ITwitter
    {
        /// <summary>
        /// Gets the latest tweets with a certain query asynchronously
        /// </summary>
        /// <param name="query">
        /// Query may be any string, if the query starts with a '@'
        /// the provider will treat the query as a username,
        /// and get the latest tweets by that user
        /// </param>
        /// <param name="pageCount">Amount of requests to execute, requesting more will give older tweets too</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Task of tweets collection</returns>
        Task<IEnumerable<Tweet>> GetTweets(
            string query,
            int pageCount = 1,
            CancellationToken token = default);
    }
}