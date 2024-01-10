using Ecommerce.Data;
using Ecommerce.GenericRepository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace Ecommerce.GenericRepository.Implementation
{
    public class GenericRepository<T>:IGenericReopsitory<T> where T : class
    {
        private readonly EcommerceDbContext _dbContext;
        private DbSet<T> _dbSet;
        public GenericRepository(EcommerceDbContext dbContext)
    
        { 
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id)!;
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            _dbSet.Remove(_dbSet.Find(id)!);
            _dbContext.SaveChanges();
        }

    }
}
