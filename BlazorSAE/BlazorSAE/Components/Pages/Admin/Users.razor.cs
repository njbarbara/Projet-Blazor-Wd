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

namespace BlazorSAE.Components.Pages.Admin
{
    public partial class Users
    {
        private IQueryable<UserModel> Items { get; set; }   

        // Remplacé l'injection générique par une création ciblée vers l'ancien resx "Components.Pages.List"
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

        [Inject]
        IEditUserService EditUserService { get; set; }

        [CascadingParameter]
        public IModalService Modal { get; set; }

        private PaginationState Pagination { get; set; } = new PaginationState { ItemsPerPage = 5 };

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            // Nom de la ressource actuel (correspond à votre fichier Resources\Components\Pages.List.resx)
            var assemblyName = typeof(Users).Assembly.GetName().Name;
            Localizer = LocalizerFactory.Create("Components.Pages.List", assemblyName);
        }

        private async ValueTask<GridItemsProviderResult<UserModel>> GetItems(GridItemsProviderRequest<UserModel> request)
        {
            return await EditUserService.GetItems(request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        private async void OnDelete(int id)
        {
            var parameters = new ModalParameters
            {
                { nameof(UserModel.Id), id }
            };

            var modal = Modal.Show<DeleteConfirmation>("Delete Confirmation", parameters);
            var result = await modal.Result;

            if (result.Cancelled)
            {
                return;
            }
            await EditUserService.Delete(id);

            NavigationManager.NavigateTo("/");

            // Reload the page
            NavigationManager.NavigateTo("/Admin/users");
        }
    }
}
