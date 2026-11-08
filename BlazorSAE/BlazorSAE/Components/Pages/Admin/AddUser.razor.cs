using Blazored.LocalStorage;
using BlazorSAE.Components.Modals;
using BlazorSAE.Models;
using BlazorSAE.Services.Interfaces;
using BlazorSAE.UIModels;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BlazorSAE.Components.Pages.Admin
{
    public partial class AddUser
    {
        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        IStringLocalizerFactory LocalizerFactory { get; set; }

        [Inject]
        IEditUserService EditUserService { get; set; }

        [Inject]
        private ILogger<BanConfirmation> Logger { get; init; }


        public IStringLocalizer Localizer;

        private UserUiModel Item { get; set; } = new();

        private async Task HandleValidSubmit()
        {
            /*
            // Get the current data
            var currentData = await LocalStorage.GetItemAsync<List<UserModel>>("data");

            // Simulate the Id (on changera par un appel à l'API 
            Item.Id = currentData.Max(s => s.Id) + 1;

            currentData.Add(new UserModel
            {
                Id = Item.Id,
                Name = Item.Name,
                Email = Item.Email,
                IsAdmin = Item.IsAdmin,
                IsBanned = false,
                ProfilePic = " ",
            });

            // Save the data
            await LocalStorage.SetItemAsync("data", currentData);
            */
            await EditUserService.Add(Item);

            NavigationManager.NavigateTo("/Admin/users");
            Logger.LogInformation(DateTime.Now + " : L'utilsateur a été créé");
        }

        
        /// <summary>
        /// Initializes the component and sets up required resources before rendering.
        /// </summary>
        protected override void OnInitialized()
        {
            var assemblyName = typeof(AddUser).Assembly.GetName().Name;
            Localizer = LocalizerFactory.Create("Components.Pages.List", assemblyName);
        }

    }
}
