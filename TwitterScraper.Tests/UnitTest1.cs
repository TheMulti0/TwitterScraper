using System.Threading.Tasks;
using Xunit;

namespace TwitterScraper.Tests
{
    public class UnitTest1
    {
        [Fact]
        public Task Test1()
        {
            return new Twitter().Operate();
        }
    }
}