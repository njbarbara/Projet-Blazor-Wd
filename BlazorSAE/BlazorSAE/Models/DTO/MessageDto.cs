using System.Text.Json.Serialization;

namespace BlazorSAE.Models.DTO
{
    public class MessageDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("user")]
        public UserModel User { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("date")]
        public DateOnly Date { get; set; }

        [JsonPropertyName("report")]
        public bool isReport { get; set; }

    }
}
