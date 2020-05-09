using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TwitterScraper.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task TestWithQuery()
        {
            ITwitter twitter = new Twitter();

            var tweets = await twitter.GetTweets("Test", 2);
            
            Assert.NotEmpty(tweets);
        }
        
        [Fact]
        public async Task TestWithAuthor()
        {
            ITwitter twitter = new Twitter();

            IEnumerable<Tweet> tweets = await twitter.GetTweets("@realDonaldTrump", 2);
            
            Assert.NotEmpty(tweets);
        }

        [Fact]
        public async Task TestWithHashtag()
        {
            ITwitter twitter = new Twitter();
            
            IEnumerable<Tweet> tweets = await twitter.GetTweets("#COVID-19", 2);
            
            Assert.NotEmpty(tweets);
        }
    }
}