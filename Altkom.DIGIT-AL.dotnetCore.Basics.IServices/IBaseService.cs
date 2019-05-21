using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.IServices
{
    public interface IBaseService<T>
    {
        Task<ICollection<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task<int> AddAsync(T entity);
        Task<bool> UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id);
    }
}
