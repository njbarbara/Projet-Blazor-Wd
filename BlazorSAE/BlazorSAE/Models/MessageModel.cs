using System.Text.Json.Serialization;
//Fautes de temps nous n'avons pas pu implémenter le DTO dans le code
namespace BlazorSAE.Models
{
    public class MessageModel
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
