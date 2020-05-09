using HtmlAgilityPack;

namespace TwitterScraper
{
    public static class AuthorFactory
    {
        public static Author CreateAuthor(HtmlNode profile)
        {
            var id = long.Parse(
                profile.GetAttributeValue("data-user-id", "0"));
            
            var userName = profile.GetAttributeValue("data-screen-name", null);

            var name = profile.GetAttributeValue("data-name", null);

            return new Author
            {
                Id = id,
                Username = userName,
                Name = name
            };
        }
    }
}