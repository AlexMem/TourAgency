using TourAgency.Models;
using TourAgency.Repositories;

namespace TourAgency.Services {
    public class TypeService {
        private readonly TypeRepository typeRepository;

        public TypeService(TypeRepository typeRepository) {
            this.typeRepository = typeRepository;
        }
        
        public Type findByName(string name) {
            return typeRepository.getAll().Find(role => role.name.Equals(name));
        }
    }
}
