using Demo.BLL.Entities;
using Demo.BLL.Entities.Order;
using Demo.BLL.Inerfaces;
using Demo.BLL.Specification;
using Demo.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL
{
    public class GenericRepository <T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext context;
        private StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        => _context.Set<T>().Add(entity);

        public async Task<int> CountAsync(ISpecification<T> Spec)
        => await ApplySpecification(Spec).CountAsync();

        public void Delete(T entity)
        => _context.Set<T>().Remove(entity);
        public async Task<T> GetByIdAsync(int id)
        =>await _context.Set<T>().FindAsync(id);

        public async Task<T> GetEntityWithSpec(ISpecification<T> Spec)
       => await ApplySpecification(Spec).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<T>> ListAllAsync()
        =>await _context.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> ListWithspacAsync(ISpecification<T> Spec)
        => await ApplySpecification(Spec).ToListAsync();

        public void Update(T entity)
        => _context.Set<T>().Update(entity);
        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
            => SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specification);

        Task<Order> IGenericRepository<T>.GetEntityWithSpec(ISpecification<T> Spec)
        {
            throw new NotImplementedException();
        }
    }
}
