using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.Services
{
    public abstract class BaseEntityService<T> : BaseService, IBaseService<T> where T : Base
    {
        public BaseEntityService(HttpClient client) : base(client){
        }

        public async Task<ICollection<T>> GetAsync()
        {
            try {
            var response = await Client.GetAsync(Path);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<ICollection<T>>();
            }
            catch
            {
                return null;
            }
        }

        public async Task<T> GetAsync(int id)
        {
            try {
                var response = await Client.GetAsync($"{Path}/{id}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<T>();
            }
            catch
            {
                return default(T);
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            try {
                var response = await Client.PostAsJsonAsync(Path, entity);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<T>();
            }
            catch
            {
                return default(T);
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try {
                var response = await Client.PutAsJsonAsync($"{Path}/{entity.Id}", entity);
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try {
                var response = await Client.DeleteAsync($"{Path}/{id}");
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
