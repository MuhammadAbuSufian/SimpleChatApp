using ChatApi.Models;
using DotNetCoreApiStarter.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Data.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetById(string id);
        T GetByIdSync(Guid id);
        IQueryable<T> GetAll();
        Task<T> Add(T entity);
        T AddSync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }


    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        public BusinessDbContext _context;

        public BaseRepository(BusinessDbContext appContext)
        {
            _context = appContext;
        }
        public async virtual  Task<TEntity> GetById(string id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public  virtual TEntity GetByIdSync(Guid id)
        {
            return  _context.Set<TEntity>().ToList().Where(x=> x.Id == id).FirstOrDefault();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsQueryable();
        }


        public async Task<TEntity> Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public TEntity AddSync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }
    }

}
