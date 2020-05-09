using System.Text.Json.Serialization;

namespace TwitterScraper
{
    public class TwitterResponsePage
    {
        [JsonPropertyName("min_position")]
        public string FirstItem { get; set; }

        [JsonPropertyName("max_position")]
        public string LastItem { get; set; }

        [JsonPropertyName("has_more_items")]
        public bool IsLatestPage { get; set; }

        [JsonPropertyName("items_html")]
        public string ItemsHtml { get; set; }

        [JsonPropertyName("new_latent_count")]
        public int NewLatentCount { get; set; }
    }
}