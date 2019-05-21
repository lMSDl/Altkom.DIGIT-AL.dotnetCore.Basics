using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;
using Bogus;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.FakeServices
{
    public abstract class FakeBaseService<T> : IBaseService<T> where T : Base
    {
        protected readonly ICollection<T> entities;
        private int _index;

        protected FakeBaseService(Faker<T> faker, int count) {
            entities = faker.Generate(count);
            _index = entities.Max(x => x.Id);
        }
        
        public Task<int> AddAsync(T entity)
        {
            entity.Id = +_index;
            entities.Add(entity);
            return Task.FromResult(_index);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if(entity == null)
                return false;
            return entities.Remove(entity);
        }

        public Task<ICollection<T>> GetAsync()
        {
            return Task.FromResult(entities);
        }

        public Task<T> GetAsync(int id)
        {
           return Task.FromResult(entities.SingleOrDefault(x => x.Id == id));
        }

        public async Task<bool> UpdateAsync(int id, T entity)
        {
            if(await DeleteAsync(id)) {
                entity.Id = id;
                entities.Add(entity);
                return true;
            }
            return false;
        }
    }
}
