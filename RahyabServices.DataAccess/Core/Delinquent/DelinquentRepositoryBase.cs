using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Common.Core;

namespace RahyabServices.DataAccess.Core.Delinquent
{
    public class DelinquentRepositoryBase<TEntity> where TEntity : class , IDelinquentEntity
    {
        private readonly IDataContextFactory _dataContextFactory;
        public DelinquentRepositoryBase(IDataContextFactory databaseFactory)
        {
            _dataContextFactory = databaseFactory;
        }

        #region Async
        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
            {
                IQueryable<TEntity> set = db.CreateSet<TEntity>();
                return (predicate == null) ? set.Count() : set.Count(predicate);
            }
        }

        public int NonQuery(string query, params object[] args)
        {
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
            {
                return db.Database.ExecuteSqlCommand(query, args);
            }
        }

        public TEntity One(long id, params string[] includes)
        {
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
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
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
            {
                IQueryable<TEntity> set = db.CreateSet<TEntity>().AsNoTracking();
                includes.ForEach(include => set = set.Include(include));
                return set.FirstOrDefault(predicate);
            }
        }

        public TValue Query<TValue>(Func<IQueryable<TEntity>, TValue> query, params string[] includes)
        {
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
            {
                IQueryable<TEntity> set = db.CreateSet<TEntity>();

                includes.ForEach(include => set = set.Include(include));
                return query.Invoke(set);
            }
        }

        public void Remove(TEntity instance)
        {
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
            {
                db.CreateSet<TEntity>().Remove(instance);
                db.SaveChanges();
                
            }
        }

        public void Remove(long id)
        {
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
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

        public TEntity Save(TEntity instance)
        {
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
            {
                if (instance.Id == 0)
                {
                    db.CreateSet<TEntity>().Add(instance);
                }
                else
                {
                    db.MarkAsModified(instance);
                }
                db.SaveChanges();

                return instance;
            }
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
            {
                IQueryable<TEntity> set = db.CreateSet<TEntity>();
                if (predicate == null)
                {
                    return await set.CountAsync();
                }

                return await set.CountAsync(predicate);
            }
        }

        public async Task<int> NonQueryAsync(string query, params object[] args)
        {
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
            {
                var token = new CancellationToken();
                return await db.Database.ExecuteSqlCommandAsync(query, token, args);
            }
        }

        public async Task<TEntity> OneAsync(long id, params string[] includes)
        {
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
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
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
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
        public async Task<TEntity> OneAsync(Expression<Func<TEntity, bool>> predicate,bool asNoTracking=false, params string[] includes)
        {
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
            {
                IQueryable<TEntity> set = asNoTracking ? db.CreateSet<TEntity>().AsNoTracking() : db.CreateSet<TEntity>();
                includes.ForEach(include => set = set.Include(include));
                if (predicate == null)
                {
                    return await set.FirstOrDefaultAsync();
                }

                return await set.FirstOrDefaultAsync(predicate);
            }
        }
        public async Task<int> NonQExecuteueryAsync(string query, params object[] args)
        {
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
            {
                var token = new CancellationToken();
                return await db.Database.ExecuteSqlCommandAsync(query, token, args);
            }
        }

        public async Task<List<TValue>> NonQueryAsync<TValue>(string query, params object[] args)
        {
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
            {
                var token = new CancellationToken();
                return await db.Database.SqlQuery<TValue>(query, token, args).ToListAsync(token);
            }
        }

        public async Task<TValue> QueryAsync<TValue>(Func<IQueryable<TEntity>, Task<TValue>> query,bool asNoTracking=false
           , params string[] includes)
        {
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
            {
                
                IQueryable<TEntity> set =asNoTracking ? db.CreateSet<TEntity>().AsNoTracking() : db.CreateSet<TEntity>();
                includes.ForEach(include => set = set.Include(include));
                var value = await query.Invoke(set);
                return value;
            }
        }

        public async Task<int> RemoveAsync(TEntity instance)
        {
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
            {

                db.CreateSet<TEntity>().Remove(instance);
                return await db.SaveChangesAsync();
            }
        }

        public async Task<int> RemoveAsync(long id)
        {
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
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

        public async Task<int> SaveAsync(TEntity instance)
        {
           
                using (var db = _dataContextFactory.GetRahyabServicesDataContext())
                {
                    if (instance.Id == 0) { db.CreateSet<TEntity>().Add(instance); }
                    else { db.MarkAsModified(instance); }
                    return await db.SaveChangesAsync();
                }
            
          
        }

        public async Task<int> SaveAsync(TEntity instance, params string[] parameters)
        {
            using (var db = _dataContextFactory.GetRahyabServicesDataContext())
            {
                if (instance.Id == 0)
                {
                    db.CreateSet<TEntity>().Add(instance);
                }
                else
                {
                    db.MarkAsModified(instance, parameters);
                }
                return await db.SaveChangesAsync();
            }
        }
        #endregion
    }
}
