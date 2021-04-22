using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext _context;

        private DbSet<T> _dbSet;
        
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        
        public IEnumerable<T> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        public T FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Create(T item)
        {
            _dbSet.Add(item);
        }
        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
        public void Remove(T item)
        {
            _dbSet.Remove(item);         
        }

        public IQueryable<T> Select()
        {
            return _context.Set<T>();
        }
    }
}
