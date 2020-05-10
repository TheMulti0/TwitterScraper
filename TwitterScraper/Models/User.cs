namespace TwitterScraper
{
    /// <summary>
    /// Represents a Twitter user
    /// </summary>
    public class User
    {
        /// <summary>
        /// User id
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        /// Username (starts with a '@')
        /// </summary>
        public string Username { get; internal set; }
        
        
        /// <summary>
        /// Screename / display name
        /// </summary>
        public string Name { get; internal set; }
    }
}