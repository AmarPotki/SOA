using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;
using RahyabServices.Business.Domain.Kendo;
using RahyabServices.Business.Domain.Models.VipBanking;
using RahyabServices.Common.Core;
using System.Linq.Dynamic;

namespace RahyabServices.DataAccess.Core.VipBanking{
    public class VipBankingRepositoryBase<TEntity> where TEntity : class, IVipBankingEntity{
        private readonly IDataContextFactory _dataContextFactory;
        public VipBankingRepositoryBase(IDataContextFactory databaseFactory){
            _dataContextFactory = databaseFactory;
        }

        #region Async

        public int Count(Expression<Func<TEntity, bool>> predicate = null){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                IQueryable<TEntity> set = db.CreateSet<TEntity>();
                return predicate == null ? set.Count() : set.Count(predicate);
            }
        }
        public int NonQuery(string query, params object[] args){
            using (var db = _dataContextFactory.GetVipBankingDataContext()) {
                return db.Database.ExecuteSqlCommand(query, args);
            }
        }
        public TEntity One(long id, params string[] includes){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                if (includes == null || includes.Any()) { return db.CreateSet<TEntity>().Find(id); }
                IQueryable<TEntity> set = db.CreateSet<TEntity>().AsNoTracking();
                includes.ForEach(include => set = set.Include(include));
                return set.FirstOrDefault(entity => entity.KeyId == id);
            }
        }
        public TEntity One(Expression<Func<TEntity, bool>> predicate, params string[] includes){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                IQueryable<TEntity> set = db.CreateSet<TEntity>().AsNoTracking();
                includes.ForEach(include => set = set.Include(include));
                return set.FirstOrDefault(predicate);
            }
        }
        public TValue Query<TValue>(Func<IQueryable<TEntity>, TValue> query, params string[] includes){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                IQueryable<TEntity> set = db.CreateSet<TEntity>();
                includes.ForEach(include => set = set.Include(include));
                return query.Invoke(set);
            }
        }
        public void Remove(TEntity instance){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                db.CreateSet<TEntity>().Remove(instance);
                db.SaveChanges();
            }
        }
        public void Remove(long id){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                var set = db.CreateSet<TEntity>();
                var instance = set.Find(id);
                if (instance == null) { return; }
                set.Remove(instance);
                db.SaveChanges();
            }
        }
        public TEntity Save(TEntity instance){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                if (instance.KeyId == 0) {
                    db.CreateSet<TEntity>().Add(instance);
                }
                else{
                    db.MarkAsModified(instance);
                }
                db.SaveChanges();
                return instance;
            }
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                IQueryable<TEntity> set = db.CreateSet<TEntity>();
                if (predicate == null) { return await set.CountAsync(); }
                return await set.CountAsync(predicate);
            }
        }
        public async Task<int> NonQueryAsync(string query, params object[] args){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                var token = new CancellationToken();
                return await db.Database.ExecuteSqlCommandAsync(query, token, args);
            }
        }
        public async Task<TEntity> OneAsync(long id, params string[] includes){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                if (includes == null || includes.Any()) { return await db.CreateSet<TEntity>().FindAsync(id); }
                IQueryable<TEntity> set = db.CreateSet<TEntity>();
                includes.ForEach(include => set = set.Include(include));
                return await set.FirstOrDefaultAsync(entity => entity.KeyId == id);
            }
        }
        public async Task<TEntity> OneAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                IQueryable<TEntity> set = db.CreateSet<TEntity>();
                includes.ForEach(include => set = set.Include(include));
                if (predicate == null) { return await set.FirstOrDefaultAsync(); }
                return await set.FirstOrDefaultAsync(predicate);
            }
        }
        public async Task<TEntity> OneAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false,
            params string[] includes){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                IQueryable<TEntity> set = asNoTracking
                    ? db.CreateSet<TEntity>().AsNoTracking()
                    : db.CreateSet<TEntity>();
                includes.ForEach(include => set = set.Include(include));
                if (predicate == null) { return await set.FirstOrDefaultAsync(); }
                return await set.FirstOrDefaultAsync(predicate);
            }
        }
        public async Task<int> NonQExecuteueryAsync(string query, params object[] args){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                var token = new CancellationToken();
                return await db.Database.ExecuteSqlCommandAsync(query, token, args);
            }
        }
        public async Task<List<TValue>> NonQueryAsync<TValue>(string query, params object[] args){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                var token = new CancellationToken();
                return await db.Database.SqlQuery<TValue>(query, token, args).ToListAsync(token);
            }
        }
        public async Task<TValue> QueryAsync<TValue>(Func<IQueryable<TEntity>, Task<TValue>> query,
            bool asNoTracking = false
            , params string[] includes){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                IQueryable<TEntity> set = asNoTracking
                    ? db.CreateSet<TEntity>().AsNoTracking()
                    : db.CreateSet<TEntity>();
                includes.ForEach(include => set = set.Include(include));
                var value = await query.Invoke(set);
                return value;
            }
        }
        public async Task<int> RemoveAsync(TEntity instance){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                db.CreateSet<TEntity>().Remove(instance);
                return await db.SaveChangesAsync();
            }
        }
        public async Task<int> RemoveAsync(long id){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                var set = db.CreateSet<TEntity>();
                var instance = set.Find(id);
                if (instance == null) { return await new Task<int>(() => 0); }
                set.Remove(instance);
                return await db.SaveChangesAsync();
            }
        }
        public async Task<int> SaveAsync(TEntity instance){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                if (instance.KeyId == 0) {
                    db.CreateSet<TEntity>().Add(instance);
                }
                else{
                    db.MarkAsModified(instance);
                }
                return await db.SaveChangesAsync();
            }
        }
        public async Task<int> SaveAsync(TEntity instance, params string[] parameters){
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                if (instance.KeyId == 0) {
                    db.CreateSet<TEntity>().Add(instance);
                }
                else{
                    db.MarkAsModified(instance, parameters);
                }
                return await db.SaveChangesAsync();
            }
        }
       // Func<IQueryable<TEntity>, Task<TValue>> query,
        public async Task<DataSourceResult> ToDataSourceResult<TValue>( int take, int skip,
             IEnumerable<Sort> sort, Filter filter, bool asNoTracking = false, params string[] includes)
        {
            using (var db = _dataContextFactory.GetVipBankingDataContext()){
                IQueryable<TEntity> queryable = asNoTracking
                    ? db.CreateSet<TEntity>().AsNoTracking()
                    : db.CreateSet<TEntity>();
                includes.ForEach(include => queryable = queryable.Include(include));
                // Filter the data first
                if(filter.Filters.Any())
                queryable = Filter(queryable, filter);

                // Calculate the total number of records (needed for paging)
                var total =await queryable.CountAsync();

                // Sort the data
                queryable = Sort(queryable, sort);

                // Finally page the data
                queryable = Page(queryable, take, skip);

                return new DataSourceResult
                {
                    Data =await queryable.ToListAsync(),
                    Total = total
                };
            }
        }
        private  IQueryable<T> Filter<T>(IQueryable<T> queryable, Filter filter)
        {
            if (filter != null)
            {
                // Collect a flat list of all filters
                var filters = filter.All();

                // Get all filter values as array (needed by the Where method of Dynamic Linq)
                var values = filters.Select(f => f.Value).ToArray();

                // Create a predicate expression e.g. Field1 = @0 And Field2 > @1
                string predicate = filter.ToExpression(filters);
               
                // Use the Where method of Dynamic Linq to filter the data
                queryable = queryable.Where(predicate, values);
              //  queryable = queryable.Where()
            }

            return queryable;
        }

        private  IQueryable<T> Sort<T>(IQueryable<T> queryable, IEnumerable<Sort> sort)
        {
            if (sort != null && sort.Any())
            {
                // Create ordering expression e.g. Field1 asc, Field2 desc
                var ordering = String.Join(",", sort.Select(s => s.ToExpression()));

                // Use the OrderBy method of Dynamic Linq to sort the data
                return queryable.OrderBy(ordering);
            }

            return queryable;
        }

        private static IQueryable<T> Page<T>(IQueryable<T> queryable, int take, int skip)
        {
            return queryable.Skip(skip).Take(take);
        }

        #endregion
    }
}