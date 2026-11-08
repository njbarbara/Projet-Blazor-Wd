using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BlazorSAE.Components.Shared
{
    public partial class NavMenu
    {
        private IStringLocalizer Localizer;

        [Inject]
        public IStringLocalizerFactory LocalizerFactory { get; set; }

        protected override void OnInitialized()
        {
            var assemblyName = typeof(NavMenu).Assembly.GetName().Name;
            Localizer = LocalizerFactory.Create("Components.Pages.List", assemblyName);
        }

    }
}
