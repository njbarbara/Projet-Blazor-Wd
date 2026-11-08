using BlazorSAE.Models;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using Blazored.Modal;
using BlazorSAE.Components.Modals;
using System.Reflection;
using Blazored.Modal.Services;
using BlazorSAE.Services.Interfaces;

namespace BlazorSAE.Components.Pages
{
    public partial class View
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IBanService BanService { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        public IModalService Modal { get; set; }


        [Inject]
        IEditUserService EditUserService { get; set; }


        private UserModel Person { get; set; }

        public Boolean isFile { get; set; }

        public string Img;

        public string Content { get; set; }

        /// <summary>
        /// Asynchronously initializes the component and loads user data for editing.
        /// </summary>
        /// <remarks>This method retrieves the user information based on the specified identifier and sets
        /// up related properties such as the profile image path and ban status label.
        /// <returns>A task that represents the asynchronous initialization operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            Person = await EditUserService.GetById(Id);
            if (File.Exists($"{WebHostEnvironment.WebRootPath}/images/{Person.ProfilePic}"))
            {
                Img = "images/" + Person.ProfilePic;
            }
            else
            {
                Img = "images/default.png";
            }

            if (!Person.IsBanned)
            {
                Content = "Ban User";
            }
            else
            {
                Content = "UnBan User";
            }
        }

        private async void OnBanned()
        {
            var id = Person.Id;
            var parameters = new ModalParameters
            {
                { nameof(UserModel.Id), id }
            };

            var modal = Modal.Show<BanConfirmation>("Ban Confirmation", parameters);
            var result = await modal.Result;

            if (result.Cancelled)
            {
                return;
            }

            if(Content == "Ban User")
            {
                await BanService.BanUserById(id);
            }
            else
            {
                await BanService.UnBanUserById(id);
            }

            NavigationManager.NavigateTo("/");

            // Reload the page
            NavigationManager.NavigateTo("/user/"+Id);
        }

    }
}
