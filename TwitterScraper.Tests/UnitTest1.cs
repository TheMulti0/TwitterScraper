using System.Threading.Tasks;
using Xunit;

namespace TwitterScraper.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            ITwitter twitter = new Twitter();

            var tweets = await twitter.GetTweets("@realDonaldTrump", 2);
        }
    }
}