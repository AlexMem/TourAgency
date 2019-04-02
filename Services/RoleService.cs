using TourAgency.Models;
using TourAgency.Repositories;

namespace TourAgency.Services {
    public class RoleService {
        private readonly RoleRepository roleRepository;

        public RoleService(RoleRepository roleRepository) {
            this.roleRepository = roleRepository;
        }

        public Role findById(long id) {
            return roleRepository.findById(id);
        }

        public Role findByName(string name) {
            return roleRepository.getAll().Find(role => role.name.Equals(name));
        }
    }
}
