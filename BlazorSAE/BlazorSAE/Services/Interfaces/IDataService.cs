using BlazorSAE.Models;
using BlazorSAE.UIModels;
using Microsoft.AspNetCore.Components.QuickGrid;
using System.Threading.Tasks;

namespace BlazorSAE.Services.Interfaces
{
    public interface IDataService
    {
        /// <summary>
        /// Asynchronously adds a new message using the specified message model.
        /// </summary>
        /// <param name="model">The message model containing the details of the message to add. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous add operation.</returns>
        Task AddMessage(MessageUIModels model);

        /// <summary>
        /// Retrieves a message by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the message to retrieve. Must be a positive integer.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the message with the specified
        /// identifier, or null if no such message exists.</returns>
        Task<MessageModel> GetByIdMessage(int id);
       
        /// <summary>
        /// Retrieves a collection of message items that match the specified data request parameters.
        /// </summary>
        /// <param name="request">An object that specifies filtering, sorting, paging, and other criteria for retrieving message items.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see
        /// cref="GridItemsProviderResult{MessageModel}"/> with the retrieved message items and related metadata.</returns>
        Task<GridItemsProviderResult<MessageModel>> GetItemsMessage(GridItemsProviderRequest<MessageModel> request);

        /// <summary>
        /// Deletes the message with the specified identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the message to delete. Must be a positive integer.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        Task DeleteMessage(int id);

        /// <summary>
        /// Retrieves a filtered, sorted, and paged collection of report messages based on the specified grid request
        /// parameters.
        /// </summary>
        /// <param name="request">The grid request containing filtering, sorting, and paging information to apply when retrieving report
        /// messages. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see
        /// cref="GridItemsProviderResult{MessageModel}"/> with the collection of report messages and the total count
        /// matching the request criteria.</returns>
        Task<GridItemsProviderResult<MessageModel>> GetReportMessage(GridItemsProviderRequest<MessageModel> request);
    }
}
