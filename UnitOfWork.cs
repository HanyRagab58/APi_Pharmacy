using Demo.BLL.Entities;
using Demo.BLL.Inerfaces;
using Demo.DAL.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL
{
    internal class UnitOfWork : IUnicOfWork
    {
        private readonly StoreContext _context;
        private Hashtable _repositories;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        public async Task<int> complete()
        =>await _context.SaveChangesAsync();

        public void Dispose()
        =>_context.Dispose();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();
            var type= typeof(TEntity).Name;
            if (_repositories.ContainsKey(type))
            {
                var repositorietype = typeof(GenericRepository<>);
                var repositorieInstance = Activator.CreateInstance(repositorietype.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositorieInstance);
            }
            return (IGenericRepository < TEntity > )_repositories[type];
        }
    }
}
