using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorSAE.UIModels
{
    public class MessageUIModels
    {
        public int Id { get; set; }

        public UserUiModel User { get; set; } = new UserUiModel();

        [Required]
        [StringLength(300, ErrorMessage = "Le message affiché ne doit pas dépasser 300 caractères.")]
        public string Content { get; set; }



        public DateOnly Date { get; set; }

        public bool isReport { get; set; }
    }
}
