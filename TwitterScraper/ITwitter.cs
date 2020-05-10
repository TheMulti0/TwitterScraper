using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TwitterScraper
{
    public interface ITwitter
    {
        Task<IEnumerable<Tweet>> GetTweets(
            string query,
            int pageCount = 1,
            CancellationToken token = default);
    }
}