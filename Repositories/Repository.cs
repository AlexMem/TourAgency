using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TourAgency.Contexts;

namespace TourAgency.Repositories {
    public abstract class Repository<T> where T : class {
        protected readonly TourAgencyDbContext dbContext;

        protected Repository(TourAgencyDbContext dbContext) {
            this.dbContext = dbContext;
        }

        protected abstract DbSet<T> getDbSet();
        
        public List<T> getAll() {
            return getDbSet().ToList();
        }

        public T get(object[] keyValues) {
            return getDbSet().Find(keyValues);
        }
        
        public T create(T newEntity) {
            EntityEntry<T> entityEntry = getDbSet().Add(newEntity);
            dbContext.SaveChanges();
            return entityEntry.Entity;
        }

        public T update(T entity) {
            EntityEntry<T> entityEntry = getDbSet().Update(entity);
            dbContext.SaveChanges();
            return entityEntry.Entity;
        }

        public T delete(T entity) {
            EntityEntry<T> entityEntry = getDbSet().Remove(entity);
            dbContext.SaveChanges();
            return entityEntry.Entity;
        }
    }
}