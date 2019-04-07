using System.Collections.Generic;
using TourAgency.Models;

namespace TourAgency.Repositories {
    public interface ITypeRepository {
        List<Type> getAll();
        Type findById(long id);
        Type findByName(string name);
        Type create(Type newType);
        Type update(Type type);
        Type delete(Type type);
    }
}