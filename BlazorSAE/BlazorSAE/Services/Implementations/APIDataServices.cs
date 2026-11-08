using BlazorSAE.Factories;
using BlazorSAE.Models;
using BlazorSAE.UIModels;
using Microsoft.AspNetCore.Components.QuickGrid;
using BlazorSAE.Services.Interfaces;

namespace BlazorSAE.Services.Implementations
{
    
    public class APIDataServices : IEditUserService
    {

        private String API_adress = "https://192.168.126.96:8888/api";
        private readonly HttpClient _http;

        public APIDataServices(HttpClient http)
        {
            _http = http;
        }
        public async Task Add(UserUiModel model)
        {
            //Get the data 
            var item = UserFactory.Create(model);

            // Save the data
            await _http.PostAsJsonAsync(API_adress+"/users", item);
        }

        public async Task Delete(int id)
        {
              await _http.DeleteAsync(API_adress+$"/user/{id}");
        }

        public async Task Update(int id, UserUiModel model)
        {
            // Get the item
            var item = UserFactory.Create(model);

            await _http.PutAsJsonAsync($"{id}", item);
        }

        public async Task<UserModel> GetById(int id)
        {
             return await _http.GetFromJsonAsync<UserModel>(API_adress + $"/user/{id}");
        }

        public async ValueTask<GridItemsProviderResult<UserModel>> GetItems(GridItemsProviderRequest<UserModel> request)
        {
            var items = await _http.GetFromJsonAsync<List<UserModel>>(API_adress + $"/?currentPage={request.StartIndex}&pageSize={request.Count.Value}");
            var count = await _http.GetFromJsonAsync<int>(API_adress + $"/count?currentPage={request.StartIndex}&pageSize={request.Count.Value}");

            return new GridItemsProviderResult<UserModel>
            {
                Items = items,
                TotalItemCount = count
            };
        }


    }
}
