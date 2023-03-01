using DasBookStore.Domain.Interfaces;
using DasBookStore.Domain;
using DasBookStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DasBookStore.Infrastructure.Repositories
{
   public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity //his class cannot be instantiated, it can only be inherited. All specific repositories classes that we are going to create, will inherit from this main class. 
        {
            protected readonly BookStoreDbContext Db;

            protected readonly DbSet<TEntity> DbSet;

            protected Repository(BookStoreDbContext db)
            {
                Db = db;
                DbSet = db.Set<TEntity>();
            }

            public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
            {
                return await DbSet.AsNoTracking().Where(predicate).ToListAsync(); //No tracking queries are useful when the results are used in a read-only scenario. They’re quicker to execute because there’s no need to set up the change tracking information. If you don’t need to update the entities retrieved from the database, then a no-tracking query should be used.
        }

            public virtual async Task<TEntity> GetById(int id)
            {
                return await DbSet.FindAsync(id);
            }

            public virtual async Task<List<TEntity>> GetAll()
            {
                return await DbSet.ToListAsync();
            }

            public virtual async Task Add(TEntity entity)
            {
                DbSet.Add(entity);
                await SaveChanges();
            }

            public virtual async Task Update(TEntity entity) //The virtual keyword is used to modify a method, property, indexer, or event declaration and allow for it to be overridden in a derived class.
        {
                DbSet.Update(entity);
                await SaveChanges();
            }

            public virtual async Task Remove(TEntity entity)
            {
                DbSet.Remove(entity);
                await SaveChanges();
            }

            public async Task<int> SaveChanges()
            {
                return await Db.SaveChangesAsync();
            }

            public void Dispose()
            {
                Db?.Dispose();
            }
        }
    
}
