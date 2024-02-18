using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABCBank.Dependencies.GenericRepository.Interfaces
{
    public interface IGenericRepository<T> where T:class
    {
        Task<T> Add(T data);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid Id);
        Task<T> Update(Guid Id,T data);
        Task<bool> Delete(Guid Id);
    }
}