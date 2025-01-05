using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbcontext _DbContext;

        private Hashtable _Repositories;

        public UnitOfWork( StoreDbcontext storeDbcontext)
        {
            _DbContext = storeDbcontext;
            _Repositories = new Hashtable();
        }
        public async Task<int> Complete()
        {
           return await _DbContext.SaveChangesAsync();
        }

        public  async  ValueTask DisposeAsync()
        {
            await _DbContext.DisposeAsync();
        }

        public IGenericRepositories<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var Type = typeof(TEntity).Name;
            if (!_Repositories.ContainsKey(Type))
            {  
                var repository = new IGenericRepository<TEntity>(_DbContext); 
                _Repositories.Add(Type, repository);
            }
            return (IGenericRepositories<TEntity>)_Repositories[Type];

         
        } 

       
    }
}
