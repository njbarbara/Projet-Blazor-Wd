using BlazorSAE.Models;
using BlazorSAE.UIModels;
using Microsoft.AspNetCore.Components.QuickGrid;

namespace BlazorSAE.Services.Interfaces
{
    public interface IEditUserService
    {
        /// <summary>
        /// Asynchronously adds a new user based on the specified user model.
        /// </summary>
        /// <param name="model">The user model containing the details of the user to add. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous add operation.</returns>
        Task Add(UserUiModel model);

        /// <summary>
        /// Deletes the entity with the specified identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        Task Delete(int id);

        /// <summary>
        /// Updates the user with the specified identifier using the provided user data.
        /// </summary>
        /// <param name="id">The unique identifier of the user to update.</param>
        /// <param name="model">The user data to apply to the specified user. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task Update(int id, UserUiModel model);

        /// <summary>
        /// Asynchronously retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to retrieve. Must be a positive integer.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user with the specified
        /// identifier, or null if no user is found.</returns>
        Task<UserModel> GetById(int id);

        /// <summary>
        /// Retrieves a set of user items based on the specified grid request parameters.
        /// </summary>
        /// <param name="request">The request that specifies filtering, sorting, and paging options for retrieving user items. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see
        /// cref="GridItemsProviderResult{UserModel}"/> with the user items matching the request and the total item
        /// count.</returns>
        ValueTask<GridItemsProviderResult<UserModel>> GetItems(GridItemsProviderRequest<UserModel> request);
    }
}
