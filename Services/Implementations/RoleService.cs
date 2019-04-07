using TourAgency.Models;
using TourAgency.Repositories;

namespace TourAgency.Services.Implementations {
    public class RoleService : IRoleService {
        private readonly IRoleRepository roleRepository;

        public RoleService(IRoleRepository roleRepository) {
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
