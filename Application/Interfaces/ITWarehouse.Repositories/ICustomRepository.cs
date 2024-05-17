using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ITWarehouse.Repositories;
public interface ICustomRepository<T> where T : class
{
    Task<ICollection<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> CreateAsync(T t);
    Task<T> UpdateAsync(int id, T t);
    Task<int> DeleteAsync(int id);
}
