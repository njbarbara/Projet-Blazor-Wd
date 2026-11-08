using Blazored.LocalStorage;
using BlazorSAE.Components.Modals;
using BlazorSAE.Services.Interfaces;
using BlazorSAE.UIModels;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace BlazorSAE.Components.Pages.Admin.Edits
{
    public partial class Edits
    {
        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        IEditUserService EditUserService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        private UserUiModel user { get; set; } = new UserUiModel();

        [Parameter]
        public int Id { get; set; }

        [Inject]
        private ILogger<BanConfirmation> Logger { get; init; }

        protected override async Task OnInitializedAsync()
        {
            var item = await EditUserService.GetById(Id);
            user = new UserUiModel () 
            {
                Id = item.Id,
                Name = item.Name,
                Email = item.Email,
                IsBanned = item.IsBanned, 
                IsAdmin = item.IsAdmin, 
                ProfilePic = item.ProfilePic,
            };
        }

        private async Task HandleValidSubmit()
        {
            await EditUserService.Update(Id, user);

            NavigationManager.NavigateTo("/Admin/users");
            Logger.LogInformation(DateTime.Now + " : "+user.Id +" "+ user.Name+" a été édité");
        }
    }
}
