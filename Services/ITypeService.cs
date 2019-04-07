using System.Collections.Generic;
using TourAgency.Models;

namespace TourAgency.Services {
    public interface ITypeService {
        Type findById(long id);
        Type findByName(string name);
        List<Type> getAll();
    }
}