using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;
using Talabat.Repository.SpecificationEvaluator;

namespace Talabat.Repository
{
    public class IGenericRepository<T> : IGenericRepositories<T> where T : BaseEntity
    {
        public StoreDbcontext _StoreDbcontext { get; }
        public IGenericRepository(StoreDbcontext storeDbcontext)
        {
            _StoreDbcontext = storeDbcontext;
        }

     
        public async Task<IReadOnlyList<T>> GetAllAsync()
        { 
            if (typeof(T) == typeof(Product))
            {
                return (IReadOnlyList<T>) await  _StoreDbcontext.Products.Include(p => p.ProductBrand).Include(p=> p.ProductType).ToListAsync();
            }
            else 
            return  await _StoreDbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
         => await _StoreDbcontext.Set<T>().FindAsync(id);

      

        public async Task<IReadOnlyList<T>> GetAllAsyncWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec  )
        {
            return EspicificationEvaluator<T>.GetQuery(_StoreDbcontext.Set<T>(), spec);
        }

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task Add(T entity)
           => await _StoreDbcontext.Set<T>().AddAsync(entity);
        

        public void Update(T entity)
          => _StoreDbcontext.Set<T>().Update(entity);

        public void Delete(T entity)
         => _StoreDbcontext.Set<T>().Remove(entity);
    }
}
