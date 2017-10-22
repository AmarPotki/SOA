using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.Common.Core;

namespace RahyabServices.DataAccess.Core.Bank
{
    public class AbisRepositoryBase<TEntity> where TEntity : class, IBankEntity
    {
        private readonly IDataContextFactory _dataContextFactory;
        public AbisRepositoryBase(IDataContextFactory databaseFactory)
        {
            _dataContextFactory = databaseFactory;
        }

        #region Async
        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            using (var db = _dataContextFactory.GetAbisLoanDataContext())
            {
                IQueryable<TEntity> set = db.CreateSet<TEntity>();
                return (predicate == null) ? set.Count() : set.Count(predicate);
            }
        }

        public int NonQuery(string query, params object[] args)
        {
            using (var db = _dataContextFactory.GetAbisLoanDataContext())
            {
                return db.Database.ExecuteSqlCommand(query, args);
            }
        }

        public TEntity One(long id, params string[] includes)
        {
            using (var db = _dataContextFactory.GetAbisLoanDataContext())
            {
                if (includes == null || includes.Any())
                {
                    return db.CreateSet<TEntity>().Find(id);
                }

                IQueryable<TEntity> set = db.CreateSet<TEntity>().AsNoTracking();

                includes.ForEach(include => set = set.Include(include));
                return set.FirstOrDefault(entity => entity.Id == id);
            }
        }

        public TEntity One(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            using (var db = _dataContextFactory.GetAbisLoanDataContext())
            {
                IQueryable<TEntity> set = db.CreateSet<TEntity>().AsNoTracking();
                includes.ForEach(include => set = set.Include(include));
                return set.FirstOrDefault(predicate);
            }
        }

        public TValue Query<TValue>(Func<IQueryable<TEntity>, TValue> query, params string[] includes)
        {
            using (var db = _dataContextFactory.GetAbisLoanDataContext())
            {
                IQueryable<TEntity> set = db.CreateSet<TEntity>();

                includes.ForEach(include => set = set.Include(include));
                return query.Invoke(set);
            }
        }

        public void Remove(TEntity instance)
        {
            using (var db = _dataContextFactory.GetAbisLoanDataContext())
            {
                db.CreateSet<TEntity>().Remove(instance);
                db.SaveChanges();
            }
        }

        public void Remove(long id)
        {
            using (var db = _dataContextFactory.GetAbisLoanDataContext())
            {
                var set = db.CreateSet<TEntity>();
                var instance = set.Find(id);
                if (instance == null)
                {
                    return;
                }
                set.Remove(instance);
                db.SaveChanges();
            }
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            using (var db = _dataContextFactory.GetAbisLoanDataContext())
            {
                IQueryable<TEntity> set = db.CreateSet<TEntity>();
                if (predicate == null)
                {
                    return await set.CountAsync();
                }

                return await set.CountAsync(predicate);
            }
        }

        public async Task<int> NonQExecuteueryAsync(string query, params object[] args)
        {
            using (var db = _dataContextFactory.GetAbisLoanDataContext())
            {
                var token = new CancellationToken();
                return await db.Database.ExecuteSqlCommandAsync(query, token, args);
            }
        }

        public async Task<List<TValue>> NonQueryAsync<TValue>(string query, params object[] args)
        {
            using (var db = _dataContextFactory.GetAbisLoanDataContext())
            {
                var token = new CancellationToken();
                return await db.Database.SqlQuery<TValue>(query, token, args).ToListAsync(token);
            }
        }

        public async Task<TEntity> OneAsync(long id, params string[] includes)
        {
            using (var db = _dataContextFactory.GetAbisLoanDataContext())
            {
                if (includes == null || includes.Any())
                {
                    return await db.CreateSet<TEntity>().FindAsync(id);
                }

                IQueryable<TEntity> set = db.CreateSet<TEntity>();
                includes.ForEach(include => set = set.Include(include));
                return await set.FirstOrDefaultAsync(entity => entity.Id == id);
            }
        }

        public async Task<TEntity> OneAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            using (var db = _dataContextFactory.GetAbisLoanDataContext())
            {
                IQueryable<TEntity> set = db.CreateSet<TEntity>();
                includes.ForEach(include => set = set.Include(include));
                if (predicate == null)
                {
                    return await set.FirstOrDefaultAsync();
                }

                return await set.FirstOrDefaultAsync(predicate);
            }
        }

        public async Task<TValue> QueryAsync<TValue>(Func<IQueryable<TEntity>, Task<TValue>> query,
            params string[] includes)
        {
            using (var db = _dataContextFactory.GetAbisLoanDataContext())
            {

                IQueryable<TEntity> set = db.CreateSet<TEntity>();
                includes.ForEach(include => set = set.Include(include));
                var value = await query.Invoke(set);
                return value;
            }
        }

        public async Task<long> RemoveAsync(TEntity instance)
        {
            using (var db = _dataContextFactory.GetAbisLoanDataContext())
            {

                db.CreateSet<TEntity>().Remove(instance);
                return await db.SaveChangesAsync();
            }
        }

        public async Task<long> RemoveAsync(long id)
        {
            using (var db = _dataContextFactory.GetAbisLoanDataContext())
            {
                var set = db.CreateSet<TEntity>();
                var instance = set.Find(id);

                if (instance == null)
                {
                    return await (new Task<int>(() => 0));
                }

                set.Remove(instance);

                return await db.SaveChangesAsync();
            }
        }
        #endregion
    }
}
