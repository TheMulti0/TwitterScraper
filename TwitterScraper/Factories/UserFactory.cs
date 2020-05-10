using HtmlAgilityPack;

namespace TwitterScraper
{
    internal static class UserFactory
    {
        public static User CreateAuthor(HtmlNode profile)
        {
            var id = long.Parse(
                profile.GetAttributeValue("data-user-id", "0"));
            
            var userName = profile.GetAttributeValue("data-screen-name", null);

            var name = profile.GetAttributeValue("data-name", null);

            return new User
            {
                Id = id,
                Username = userName,
                Name = name
            };
        }
    }
}