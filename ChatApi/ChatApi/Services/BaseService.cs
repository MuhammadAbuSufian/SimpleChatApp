using ChatApi.Data.Repositories;
using ChatApi.Models;
using ChatApi.Models.RequestModels;
using ChatApi.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Services
{
    public interface IBaseService<T, TVm, TRm>
        where T : BaseEntity
        where TVm : BaseViewModel<T>
        where TRm : BaseRequestModel<T>
    {
        Task<List<TVm>> GetAll();
        Task<TVm> Get(string id);
        T GetSync(Guid id);
        Task<int> Count();
        Task<TVm> Add(T entity);
        TVm AddSync(T entity);
        void Edit(T entity);
        void Delete(T entity);
    }
    public abstract class BaseService<TEntity, TViewModel, TRequestModel> : IBaseService<TEntity, TViewModel, TRequestModel>
    where TEntity : BaseEntity
    where TViewModel : BaseViewModel<TEntity>
    where TRequestModel : BaseRequestModel<TEntity>
    {
        protected IBaseRepository<TEntity> Repository;

        protected BaseService(IBaseRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<TViewModel>> GetAll()
        {
            var entityList = await Repository.GetAll().ToListAsync();
            var entityViewsList = entityList.ConvertAll(x => (TViewModel)Activator.CreateInstance(typeof(TViewModel), x));
            return entityViewsList;
        }

        public virtual async Task<TViewModel> Get(string id)
        {
            var entity = await Repository.GetById(id);
            var entityViewModel = (TViewModel)Activator.CreateInstance(typeof(TViewModel), entity);
            return entityViewModel;
        }

        public virtual TEntity GetSync(Guid id)
        {
            var entity = Repository.GetByIdSync(id);
            return entity;
        }

        public virtual async Task<TViewModel> Add(TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Id.ToString())) entity.Id = Guid.NewGuid();
            var savedEntity = await Repository.Add(entity);
            return (TViewModel)Activator.CreateInstance(typeof(TViewModel), savedEntity);
        }
        public TViewModel AddSync(TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Id.ToString())) entity.Id = Guid.NewGuid();
            var savedEntity = Repository.AddSync(entity);
            return (TViewModel)Activator.CreateInstance(typeof(TViewModel), savedEntity);

        }

        public virtual void Edit(TEntity entity)
        {
            Repository.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            Repository.Delete(entity);
        }

        public virtual async Task<int> Count()
        {
            return await Repository.GetAll().CountAsync();
        }

    }
}
