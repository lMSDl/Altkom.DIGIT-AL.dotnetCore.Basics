using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.IServices
{
    public interface IBaseService<T> where T : Base
    {
        Task<ICollection<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
