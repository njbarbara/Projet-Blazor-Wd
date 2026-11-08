using BlazorSAE.Components.Modals;
using BlazorSAE.Components.Pages.Admin;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Globalization;

namespace BlazorSAE.Components.Pages
{
    public partial class Settings : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IStringLocalizerFactory LocalizerFactory { get; set; }
        
        public IStringLocalizer Localizer { get; set; }
        public string CurrentCultureDisplay => $"{CultureInfo.CurrentCulture.DisplayName} ({CultureInfo.CurrentCulture.Name})";
        [Inject]
        private ILogger<BanConfirmation> Logger { get; init; }

        /// <summary>
        /// Initializes the component and configures localization resources.
        /// </summary>
        

        protected override void OnInitialized() { 
            var assemblyName = typeof(Settings).Assembly.GetName().Name;
            Localizer = LocalizerFactory.Create("Components.Pages.List", assemblyName);
        }
        private void SetCulture(string culture)
        {
            var c = culture.ToLower(CultureInfo.InvariantCulture);
            var uri = new Uri(this.NavigationManager.Uri).GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
            var query = $"?culture={Uri.EscapeDataString(c)}&redirectUri={Uri.EscapeDataString(uri)}";

            // Redirige vers le controller qui définit le cookie de culture et recharge la page
            this.NavigationManager.NavigateTo("/Culture/SetCulture" + query, forceLoad: true);

            Logger.LogInformation("Les paramètres ont été modifiés");
        }
    }
}