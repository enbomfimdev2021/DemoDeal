﻿using RestApiModeloDDD.Domain.Core.Interfaces.Repositorys;
using RestApiModeloDDD.Domain.Core.Interfaces.Services;
using System.Collections.Generic;

namespace RestApiModeloDDD.Domain.Service
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private IRepositoryBase<TEntity> _repository;
        public ServiceBase(IRepositoryBase<TEntity> repository  )
        {
            _repository = repository;
        }
        
        public void Add(TEntity obj)
        {
            _repository.Add(obj);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Remove(TEntity obj)
        {
            _repository.Remove(obj);
                
        }

        public void Update(TEntity obj)
        {
            _repository.Update(obj);
        }
    }
}
