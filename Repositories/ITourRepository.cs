using System.Collections.Generic;
using TourAgency.Models;

namespace TourAgency.Repositories {
    public interface ITourRepository {
        List<Tour> getAll();
        Tour findById(long id);
        Tour create(Tour newTour);
        Tour update(Tour tour);
        Tour delete(Tour tour);
        List<Tour> findByHot(bool isHot);
        List<Tour> findByUser(User user);
        List<Tour> findByUserEmail(string email);
    }
}