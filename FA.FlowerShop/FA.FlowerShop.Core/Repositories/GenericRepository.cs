using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FA.FlowerShop.Core;

namespace FA.FlowerShop.Core
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly FlowerShopContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public GenericRepository(FlowerShopContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public virtual int Add(TEntity entity)
        {
            _dbSet.Add(entity);
            return _context.SaveChanges();
        }

        public virtual bool Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual bool Update(TEntity entity)
        {
            _dbSet.AddOrUpdate(entity);
            return _context.SaveChanges() > 0;
        }
        public virtual IQueryable<TEntity> Get()
        {
            return _dbSet;
        }
    }
}
