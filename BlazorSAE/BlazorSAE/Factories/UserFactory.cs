using BlazorSAE.Components.Pages.Admin;
using BlazorSAE.Models;
using BlazorSAE.UIModels;

namespace BlazorSAE.Factories
{
    public static class UserFactory
    {
        
        /// <summary>
        /// Converts a UserModel instance to a UserUiModel for use in the user interface.
        /// </summary>
        /// <param name="user">The user data model to convert. Cannot be null.</param>
        /// <returns>A <see cref="UserUiModel"/> populated with values from the specified <paramref name="user"/>.</returns>
        public static UserUiModel ToUiModel(UserModel user)
        {
            return new UserUiModel()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                IsBanned = user.IsBanned,
                IsAdmin = user.IsAdmin,
                ProfilePic = user.ProfilePic,
            };
        }

        /// <summary>
        /// Creates a new instance of the UserModel class based on the specified user interface model.
        /// </summary>
        /// <param name="user">The user interface model containing the data to initialize the UserModel. Cannot be null.</param>
        /// <returns>A new UserModel instance populated with values from the specified user interface model.</returns>
        public static UserModel Create(UserUiModel user)
        {
            return new UserModel()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                IsBanned = user.IsBanned,
                IsAdmin = user.IsAdmin,
                ProfilePic = user.ProfilePic,
            };
        }

        public static void Update(UserModel item, UserUiModel model)
        {
            item.Name = model.Name;
            item.Email = model.Email;
            item.Id = model.Id;
            item.IsBanned = model.IsBanned;
            item.IsAdmin = model.IsAdmin;
            item.ProfilePic = model.ProfilePic;
        }
    }
}
