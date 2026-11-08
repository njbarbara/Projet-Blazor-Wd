using Blazored.LocalStorage;
using BlazorSAE.Models;
using BlazorSAE.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BlazorSAE.Services.Implementations
{
    public class BanDataLocalService : IBanService
    {
        private readonly ILocalStorageService _localStorage;

        public BanDataLocalService(
            ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public async Task BanUserById(int id)
        {
            var currentData = await _localStorage.GetItemAsync<List<UserModel>>("data") ?? new List<UserModel>();

            var item = currentData.FirstOrDefault(w => w.Id == id);

            if (item != null)
            {
                item.IsBanned = true;
                await _localStorage.SetItemAsync("data", currentData);
            }
        }

        public async Task UnBanUserById(int id)
        {
            var currentData = await _localStorage.GetItemAsync<List<UserModel>>("data") ?? new List<UserModel>();

            var item = currentData.FirstOrDefault(w => w.Id == id);

            if (item != null)
            {
                item.IsBanned = false;
                await _localStorage.SetItemAsync("data", currentData);
            }
        }
    }
}
