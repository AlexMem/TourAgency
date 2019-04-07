using System.Collections.Generic;
using TourAgency.Models;

namespace TourAgency.Services {
    public interface ITourService {
        List<Tour> getAll();
        Tour findById(long id);
        List<Tour> findByHot(bool isHot);
        List<Tour> findByUser(User user);
        Tour create(Tour newTour);
        Tour update(Tour tour);
        Tour delete(Tour tour);
        void verify(Tour tour);
    }
}