using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
 

namespace BlazorSAE.Components.Layout
{
    
    public partial class MainLayout
    {
        [Inject]
        public IStringLocalizerFactory? LocalizerFactory { get; set; }

        public IStringLocalizer? Localizer;

        protected override void OnInitialized()
        {
            var assemblyName = typeof(MainLayout).Assembly.GetName().Name;

            Localizer = LocalizerFactory?.Create("Components.Pages.List", assemblyName);

        }
    }
}
