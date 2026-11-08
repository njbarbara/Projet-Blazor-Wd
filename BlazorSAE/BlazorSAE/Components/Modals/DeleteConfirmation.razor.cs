using Blazored.Modal;
using Blazored.Modal.Services;
using BlazorSAE.Models;
using BlazorSAE.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BlazorSAE.Components.Modals
{
    public partial class DeleteConfirmation
    {
        [CascadingParameter]
        public BlazoredModalInstance? ModalInstance { get; set; }

        [Inject]
        IEditUserService? EditUserService { get; set; }

        [Parameter]
        public int Id { get; set; }

        private UserModel Item { get; set; } = new UserModel();

        [Inject]
        private ILogger<BanConfirmation>? Logger { get; init; }

        protected override async Task OnInitializedAsync()
        {
            // Get the item
            Item = await EditUserService.GetById(Id);
        }

        private void ConfirmDelete()
        {
            ModalInstance.CloseAsync(ModalResult.Ok(true));
            Logger.LogInformation(DateTime.Now + " : L'utilisateur a bien été supprimé");
        }

        private void Cancel()
        {
            ModalInstance.CancelAsync();
            Logger.LogWarning(DateTime.Now + " : La supression a été annulé");
        }
    }
}
