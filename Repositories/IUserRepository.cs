using System.Collections.Generic;
using TourAgency.Models;

namespace TourAgency.Repositories {
    public interface IUserRepository {
        List<User> getAll();
        User findById(long id);
        User findByEmail(string email);
        User create(User newUser);
        User update(User user);
        User delete(User user);
    }
}