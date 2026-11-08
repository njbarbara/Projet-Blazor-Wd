using System.Net.Http;
using Blazored.LocalStorage;
using BlazorSAE.Factories;
using BlazorSAE.Models;
using BlazorSAE.Services.Interfaces;
using BlazorSAE.UIModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using static System.Net.WebRequestMethods;

namespace BlazorSAE.Services.Implementations
{
    public class DataLocalService : IDataService
    {
        private readonly ILocalStorageService _localStorage;
         
        public DataLocalService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }


        public async Task AddMessage(MessageUIModels model)
        {
            var currentData = await _localStorage.GetItemAsync<List<MessageModel>>("dataMessage") ?? new List<MessageModel>();

            model.Id = currentData.Any() ? currentData.Max(s => s.Id) + 1 : 1;

            currentData.Add(new MessageModel
            {
                Id = model.Id,
                User = new UserModel
                {
                    Id = model.User.Id,
                    Name = model.User.Name,
                    Email = model.User.Email,
                    ProfilePic = model.User.ProfilePic,
                    IsAdmin = model.User.IsAdmin,
                    IsBanned = model.User.IsBanned
                },
                Content = model.Content,
                Date = model.Date,
                isReport = false,
            });

            await _localStorage.SetItemAsync("dataMessage", currentData);

        }

        public async Task<MessageModel> GetByIdMessage(int id)
        {
            var currentData = await _localStorage.GetItemAsync<List<MessageModel>>("dataMessage") ?? new List<MessageModel>();

            var item = currentData.FirstOrDefault(w => w.Id == id);

            if (item == null)
            {
                throw new Exception($"Unable to found the item with ID: {id}");
            }

            return item;
        }
        // Implémentation ajoutée pour MessageModel
        public async Task<GridItemsProviderResult<MessageModel>> GetItemsMessage(GridItemsProviderRequest<MessageModel> request)
        {
            try
            {
                var response = await _localStorage.GetItemAsync<MessageModel[]>("dataMessage") ?? Array.Empty<MessageModel>();

                var totalCount = response.Length;
                var query = response.AsQueryable();
                query = request.ApplySorting(query).Skip(request.StartIndex);

                if (request.Count.HasValue)
                {
                    query = query.Take(request.Count.Value);
                }

                return new GridItemsProviderResult<MessageModel>
                {
                    Items = query.ToList(),
                    TotalItemCount = totalCount
                };
            }
            catch
            {
                return new GridItemsProviderResult<MessageModel>
                {
                    Items = new List<MessageModel>(),
                    TotalItemCount = 0
                };
            }
        }

        // Récupérer les messages reportés
        public async Task<GridItemsProviderResult<MessageModel>> GetReportMessage(GridItemsProviderRequest<MessageModel> request)
        {
            try
            {
                var response = await _localStorage.GetItemAsync<MessageModel[]>("dataMessage") ?? Array.Empty<MessageModel>();

                // Filtrer les messages reportés
                var reported = response.Where(m => m.isReport).ToArray();

                var totalCount = reported.Length;
                var query = reported.AsQueryable();

                // Appliquer le tri fourni par la requête
                query = request.ApplySorting(query).Skip(request.StartIndex);

                if (request.Count.HasValue)
                {
                    query = query.Take(request.Count.Value);
                }

                return new GridItemsProviderResult<MessageModel>
                {
                    Items = query.ToList(),
                    TotalItemCount = totalCount
                };
            }
            catch
            {
                return new GridItemsProviderResult<MessageModel>
                {
                    Items = new List<MessageModel>(),
                    TotalItemCount = 0
                };
            }
        }



        public async Task DeleteMessage(int id)
        {
            var currentData = await _localStorage.GetItemAsync<List<MessageModel>>("dataMessage") ?? new List<MessageModel>();

            var item = currentData.FirstOrDefault(w => w.Id == id);

            if (item != null)
            {
                currentData.Remove(item);
                await _localStorage.SetItemAsync("dataMessage", currentData);
            }
        }

    }
}
         


        
