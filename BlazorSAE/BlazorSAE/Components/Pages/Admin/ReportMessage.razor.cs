using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Modal.Services;
using BlazorSAE.Components.Modals;
using BlazorSAE.Models;
using BlazorSAE.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Reflection;
using static System.Net.WebRequestMethods;

namespace BlazorSAE.Components.Pages.Admin
{
    public partial class ReportMessage
    {

        private IQueryable<MessageModel> Items { get; set; }
        [Inject]
        public IStringLocalizerFactory LocalizerFactory { get; set; }
        public IStringLocalizer Localizer { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public HttpClient Http { get; set; }
        [Inject]
        public ILocalStorageService LocalStorage { get; set; }
        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }
        [Inject]
        public IDataService DataService { get; set; }
        [CascadingParameter]
        public IModalService Modal { get; set; }
        private PaginationState Pagination { get; set; } = new PaginationState { ItemsPerPage = 5 };
        
        /// <summary>
        /// Asynchronously initializes the component and ensures that required message data is available in local
        /// storage.
        /// </summary>
        /// <remarks>If message data is not present in local storage, this method loads default data from
        /// a JSON file and stores it locally before the component completes initialization. This ensures that
        /// subsequent operations relying on message data can proceed without error.</remarks>
        /// <returns>A task that represents the asynchronous initialization operation.</returns>
        protected override async Task OnInitializedAsync()
        {

            var assemblyName = typeof(Messages).Assembly.GetName().Name;
            Localizer = LocalizerFactory.Create("Components.Pages.List", assemblyName);
            var currentData = await LocalStorage.GetItemAsync<MessageModel[]>("dataMessage");

            // Check if data exist in the local storage
            if (currentData == null)
            {
                // this code add in the local storage the fake data (we load the data sync for initialize the data before load the OnReadData method)
                var originalData = Http.GetFromJsonAsync<MessageModel[]>($"{NavigationManager.BaseUri}stub-message.json").Result;
                await LocalStorage.SetItemAsync("dataMessage", originalData);
            }
        }

        
        private async ValueTask<GridItemsProviderResult<MessageModel>> GetReportMessage(GridItemsProviderRequest<MessageModel> request)
        {
            return await DataService.GetReportMessage(request);
        }

        private async void OnDeleteMessage(int id)
        {
            var parameters = new ModalParameters
            {
                { nameof(MessageModel.Id), id }
            };

            var modal = Modal.Show<DeleteConfirmation>("Delete Confirmation", parameters);
            var result = await modal.Result;

            if (result.Cancelled)
            {
                return;
            }
            await DataService.DeleteMessage(id);

            NavigationManager.NavigateTo("/");

            // Reload the page
            NavigationManager.NavigateTo("/Admin/users");
        }
    }
}
