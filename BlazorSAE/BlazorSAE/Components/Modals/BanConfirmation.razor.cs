using Blazored.Modal.Services;
using Blazored.Modal;
using BlazorSAE.Models;
using Microsoft.AspNetCore.Components;
using BlazorSAE.Services.Interfaces;

namespace BlazorSAE.Components.Modals
{
    public partial class BanConfirmation
    {
        [CascadingParameter]
        public BlazoredModalInstance? ModalInstance { get; set; }

        [Inject]
        public IDataService? DataService { get; set; }

        [Parameter]
        public int Id { get; set; }

        [Inject]
        IEditUserService? EditUserService { get; set; }

        private UserModel Item { get; set; } = new UserModel();



        [Inject]
        private ILogger<BanConfirmation>? Logger { get; init; }

        protected override async Task OnInitializedAsync()
        {
            // Get the item
            Item = await EditUserService.GetById(Id);
        }

        private void ConfirmBan()
        {
            ModalInstance.CloseAsync(ModalResult.Ok(true));
            Logger.LogInformation(DateTime.Now + " Le bannissement a bien eu lieu");
        }

       

        private void Cancel()
        {
            ModalInstance.CancelAsync();
            Logger.LogWarning(DateTime.Now +" Annulation du bannissement");
        }
    }
}
