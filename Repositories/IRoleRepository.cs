using System.Collections.Generic;
using TourAgency.Models;

namespace TourAgency.Repositories {
    public interface IRoleRepository {
        List<Role> getAll();
        Role findById(long id);
        Role findByName(string name);
        Role create(Role newRole);
        Role update(Role role);
        Role delete(Role role);
    }
}