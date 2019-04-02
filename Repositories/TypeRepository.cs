using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TourAgency.Contexts;
using TourAgency.Models;

namespace TourAgency.Repositories {
    public class TypeRepository {

        public List<Type> getAll() {
            List<Type> types;
            using (var dbContext = new TourAgencyDbContext()) {
                types = dbContext.types.ToList();
            }
            return types;
        }
        
        public Type findById(long id) {
            Type type;
            using (var dbContext = new TourAgencyDbContext()) {
                type = dbContext.types
                                .ToList()
                                .Find(t => t.typeId == id);
            }
            return type;
        }
        
        public Type findByName(string name) {
            Type Type;
            using (var dbContext = new TourAgencyDbContext()) {
                Type = dbContext.types
                                .ToList()
                                .Find(t => t.name.Equals(name));
            }
            return Type;
        }
        
        public Type create(Type newType) {
            EntityEntry<Type> type;
            using (var dbContext = new TourAgencyDbContext()) {
                type = dbContext.types.Add(newType);
                dbContext.SaveChanges();
            }
            return type.Entity;
        }

        public Type update(Type type) {
            EntityEntry<Type> updatedType;
            using (var dbContext = new TourAgencyDbContext()) {
                updatedType = dbContext.types.Update(type);
                dbContext.SaveChanges();
            }
            return updatedType.Entity;
        }

        public Type delete(Type type) {
            EntityEntry<Type> removedType;
            using (var dbContext = new TourAgencyDbContext()) {
                removedType = dbContext.types.Remove(type);
                dbContext.SaveChanges();
            }
            return removedType.Entity;
        }
    }
}
