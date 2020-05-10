using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace TwitterScraper.Tests
{
    public class GetTweetTests
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
        
        [Fact]
        public void TestCancellation()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromMilliseconds(25));

            Assert.ThrowsAny<OperationCanceledException>(
                () =>
                {
                    try
                    {
                        ITwitter provider = new Twitter();
                        List<Tweet> items = provider
                            .GetTweets("@realDonaldTrump", token: cts.Token)
                            .Result
                            .ToList();
                    }
                    catch (AggregateException e)
                    {
                        List<OperationCanceledException> canceledExceptions = e.InnerExceptions
                            .Cast<OperationCanceledException>()
                            .ToList();
                        if (canceledExceptions.Any())
                        {
                            throw canceledExceptions.First();
                        }
                    }
                });
        }
    }
}