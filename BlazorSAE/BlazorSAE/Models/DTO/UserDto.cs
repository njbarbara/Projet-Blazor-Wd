using System.Text.Json.Serialization;

namespace BlazorSAE.Models.DTO
{
    public class UserDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("profilePic")]
        public string ProfilePic { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("isAdmin")]
        public bool IsAdmin { get; set; }

        [JsonPropertyName("isBanned")]
        public bool IsBanned { get; set; }
    }
}
