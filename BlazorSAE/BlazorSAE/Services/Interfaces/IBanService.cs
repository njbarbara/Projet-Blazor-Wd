namespace BlazorSAE.Services.Interfaces
{
    public interface IBanService
    {
        /// <summary>
        /// Bans the user with the specified unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to ban.</param>
        /// <returns>A task that represents the asynchronous ban operation.</returns>
        Task BanUserById(int id);

        /// <summary>
        /// Removes the ban from the user with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to unban.</param>
        /// <returns>A task that represents the asynchronous unban operation.</returns>
        Task UnBanUserById(int id);
    }
}
