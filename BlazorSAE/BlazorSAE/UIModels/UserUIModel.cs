using System.ComponentModel.DataAnnotations;

namespace BlazorSAE.UIModels
{
    public class UserUiModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Le prénom affiché ne doit pas dépasser 50 caractères.")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "L'adresse email ne doit pas dépasser 100 caractères.")]
        [RegularExpression(@"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$",
            ErrorMessage = "L'adresse email n'est pas valide. Seules les lettres minuscules, chiffres et caractères standards sont autorisés."
        )]
        public string Email { get; set; }

        public string ProfilePic { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsBanned { get; set; }
    }

}
