using TourAgency.Models;

namespace TourAgency.Services {
    public interface IRoleService {
        Role findById(long id);
        Role findByName(string name);
    }
}