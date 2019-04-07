using System.Collections.Generic;
using TourAgency.Models;

namespace TourAgency.Services {
    public interface IUserService {
        User authenticate(User user);
        List<User> getAll();
        User findById(long id);
        User findByEmail(string email);
        User createUser(User newUser);
        User createAgent(User newUser);
        User createAdmin(User newUser);
        User create(User newUser);
        User update(User user);
        User delete(User user);
        void verify(User user);
    }
}