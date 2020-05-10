using System.IO;
using HtmlAgilityPack;
using Xunit;

namespace TwitterScraper.Tests
{
    public class TweetsHtmlParsingTests
    {
        [Fact]
        public void Test1()
        {
            var html = File.ReadAllText("../../../tweets1.html");
            var document = new HtmlDocument();
            document.LoadHtml(html);

            var tweets = Twitter.ParseHtml(document);
            
            Assert.NotEmpty(tweets);
        }
    }
}