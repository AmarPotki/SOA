﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;

namespace RahyabServices.DataAccess.Core.Bank
{
    public interface IBankRepository<TEntity> where TEntity : class, IBankEntity
    {
        #region Regular
        int Count(Expression<Func<TEntity, bool>> predicate = null);

        int NonQuery(string query, params object[] args);

        TEntity One(
            long id,
            params string[] includes);

        TEntity One(
            Expression<Func<TEntity, bool>> predicate,
            params string[] includes);

        TValue Query<TValue>(
            Func<IQueryable<TEntity>, TValue> query,
            params string[] includes);

        void Remove(TEntity instance);

        void Remove(long id);

        #endregion

        #region Async

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        Task<List<TValue>> NonQueryAsync<TValue>(string query, params object[] args);
        Task<int> NonQExecuteueryAsync(string query, params object[] args);

        Task<TEntity> OneAsync(
            long id,
            params string[] includes);

        Task<TEntity> OneAsync(
            Expression<Func<TEntity, bool>> predicate,
            params string[] includes);

        Task<TValue> QueryAsync<TValue>(
            Func<IQueryable<TEntity>, Task<TValue>> query,
            params string[] includes);


        Task<long> RemoveAsync(TEntity instance);

        Task<long> RemoveAsync(long id);

        #endregion
    }
}
