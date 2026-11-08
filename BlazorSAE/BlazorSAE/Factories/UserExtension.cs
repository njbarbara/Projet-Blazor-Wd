using BlazorSAE.Components.Pages.Admin;
using BlazorSAE.Models;
using BlazorSAE.Models.DTO;

namespace BlazorSAE.Factories
{
    public static class UserExtension
    {
        public static UserDTO ToDTO(this UserModel user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfilePic = user.ProfilePic,
                IsAdmin = user.IsAdmin,
                IsBanned = user.IsBanned
            };

        }

        public static UserModel FromDTO(this UserDTO dto)
        {
            return new UserModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                ProfilePic = dto.ProfilePic,
                IsAdmin = dto.IsAdmin,
                IsBanned = dto.IsBanned
            };
        }
    }
}
