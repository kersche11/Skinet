using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T>GetByIdAsync(int id);
        Task<IReadOnlyList<T>>ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec); //Query   Specification Pattern!
        Task<IReadOnlyList<T>>ListAsync(ISpecification<T> spec);  //Query
        Task<int> CountAsync(ISpecification<T> spec);

    }
}