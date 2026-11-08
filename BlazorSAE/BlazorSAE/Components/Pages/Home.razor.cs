using Blazored.LocalStorage;
using BlazorSAE.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace BlazorSAE.Components.Pages
{
    public partial class Home
    {
        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        [Inject]
        public IStringLocalizerFactory LocalizerFactory { get; set; }

        // Renommage du champ pour éviter l'ambiguïté avec la propriété générée par Blazor
        public IStringLocalizer ListLocalizer { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            var currentData = await LocalStorage.GetItemAsync<UserModel[]>("data");

            if (currentData == null)
            {
                var originalData = Http.GetFromJsonAsync<UserModel[]>($"{NavigationManager.BaseUri}stub-user.json").Result;
                await LocalStorage.SetItemAsync("data", originalData);
            }
        }

        /// <summary>
        /// Initializes the component and configures localization resources before rendering.
        /// </summary>
        protected override void OnInitialized()
        {
            var assemblyName = typeof(Home).Assembly.GetName().Name ?? string.Empty;
            ListLocalizer = LocalizerFactory.Create("Components.Pages.List", assemblyName);
        }
    }
}
