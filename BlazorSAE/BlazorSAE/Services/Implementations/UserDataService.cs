using BlazorSAE.Factories;
using BlazorSAE.Models;
using System.Net.Http;
using BlazorSAE.Services.Interfaces;
using BlazorSAE.UIModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Blazored.LocalStorage;

namespace BlazorSAE.Services.Implementations
{
    public class UserDataService : IEditUserService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly NavigationManager _navigationManager;
        private readonly HttpClient _httpClient;

        public UserDataService(IWebHostEnvironment webHostEnvironment, NavigationManager navigationManager, HttpClient httpClient, ILocalStorageService localStorage)
        {
            _webHostEnvironment = webHostEnvironment;
            _navigationManager = navigationManager;
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task Add(UserUiModel model)
        {
            var currentData = await _localStorage.GetItemAsync<List<UserModel>>("data") ?? new List<UserModel>();
            if (currentData == null)
            {
                var originalData = _httpClient.GetFromJsonAsync<UserModel[]>($"{_navigationManager.BaseUri}stub-user.json").Result;
                await _localStorage.SetItemAsync("data", originalData);
            }

            // Simulate the Id (on changera par un appel à l'API 
            model.Id = currentData.Max(s => s.Id) + 1;

            currentData.Add(UserFactory.Create(model));
            model.Id = currentData.Any() ? currentData.Max(s => s.Id) + 1 : 1;

            currentData.Add(new UserModel
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                IsAdmin = model.IsAdmin,
                IsBanned = false,
                ProfilePic = " ",
            });
            await _localStorage.SetItemAsync("data", currentData);
        }

        public async Task<UserModel> GetById(int id)
        {
            var currentData = await _localStorage.GetItemAsync<List<UserModel>>("data") ?? new List<UserModel>();

            if (currentData == null)
            {
                var originalData = _httpClient.GetFromJsonAsync<UserModel[]>($"{_navigationManager.BaseUri}stub-user.json").Result;
                await _localStorage.SetItemAsync("data", originalData);
            }

            var item = currentData.FirstOrDefault(w => w.Id == id);

            if (item == null)
            {
                throw new Exception($"Unable to found the item with ID: {id}");
            }

            return item;
        }

        public async ValueTask<GridItemsProviderResult<UserModel>> GetItems(GridItemsProviderRequest<UserModel> request)
        {
            try
            {
                var response = await _localStorage.GetItemAsync<UserModel[]>("data") ?? Array.Empty<UserModel>();

                var totalCount = response.Length;
                var query = response.AsQueryable();
                query = request.ApplySorting(query).Skip(request.StartIndex);

                if (request.Count.HasValue)
                {
                    query = query.Take(request.Count.Value);
                }

                return new GridItemsProviderResult<UserModel>
                {
                    Items = query.ToList(),
                    TotalItemCount = totalCount
                };
            }
            catch
            {
                return new GridItemsProviderResult<UserModel>
                {
                    Items = new List<UserModel>(),
                    TotalItemCount = 0
                };
            }
        }
        public async Task Delete(int id)
        {
            var currentData = await _localStorage.GetItemAsync<List<UserModel>>("data") ?? new List<UserModel>();
            if (currentData == null)
            {
                var originalData = _httpClient.GetFromJsonAsync<UserModel[]>($"{_navigationManager.BaseUri}stub-user.json").Result;
                await _localStorage.SetItemAsync("data", originalData);
            }

            var item = currentData.FirstOrDefault(w => w.Id == id);

            if (item != null)
            {
                currentData.Remove(item);
                await _localStorage.SetItemAsync("data", currentData);
            }
        }


        public async Task Update(int id, UserUiModel model)
        {
            var currentData = await _localStorage.GetItemAsync<List<UserModel>>("data") ?? new List<UserModel>();
            if (currentData == null)
            {
                var originalData = _httpClient.GetFromJsonAsync<UserModel[]>($"{_navigationManager.BaseUri}stub-user.json").Result;
                await _localStorage.SetItemAsync("data", originalData);
            }

            // Get the item int the list
            var item = currentData.FirstOrDefault(w => w.Id == id);

            // Check if item exist
            if (item == null)
            {
                throw new Exception($"Unable to found the item with ID: {id}");
            }

            UserFactory.Update(item, model);

            // Save the data
            await _localStorage.SetItemAsync("data", currentData);
        }

    }
}
