using System.Collections.Generic;
using System.Threading.Tasks;

namespace TwitterScraper
{
    public interface ITwitter
    {
        Task<IEnumerable<Tweet>> GetTweets(string query, int pages = 1);
    }
}