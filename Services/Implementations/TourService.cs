using System;
using System.Collections.Generic;
using TourAgency.Models;
using TourAgency.Repositories;

namespace TourAgency.Services.Implementations {
    public class TourService : ITourService {
        private readonly ITourRepository tourRepository;

        public TourService(ITourRepository tourRepository) {
            this.tourRepository = tourRepository;
        }

        public List<Tour> getAll() {
            List<Tour> tours = tourRepository.getAll();
            tours.ForEach(t => t.isActive = t.fromDate.CompareTo(DateTime.Now)>0); // need scheduled task
            return tourRepository.getAll();
        }

        public Tour findById(long id) {
            return tourRepository.findById(id);
        }

        public List<Tour> findByHot(bool isHot) {
            return tourRepository.findByHot(isHot);
        }

        public List<Tour> findByUser(User user) {
            return tourRepository.findByUser(user);
        }
        
        public Tour create(Tour newTour) {
            verify(newTour);
            newTour.isActive = newTour.fromDate.CompareTo(DateTime.Now) > 0;
            return tourRepository.create(newTour);
        }

        public Tour update(Tour tour) {
            verify(tour);
            return tourRepository.update(tour);
        }

        public Tour delete(Tour tour) {
            return tourRepository.delete(tour);
        }

        public void verify(Tour tour) {
            if (tour.fromDate.CompareTo(DateTime.Now) <= 0) { // time dont forget
                throw new Exception("bad date");
            }
            if (tour.duration < 0) {
                throw new Exception("bad duration");
            }

            if (tour.hotDiscount < 0 && tour.hotDiscount > 100) {
                throw new Exception("bad hot discount");
            }

            if (tour.discount < 0 && tour.discount > 100) {
                throw new Exception("bad discount");
            }

            if (tour.price < 0) {
                throw new Exception("bad price");
            }

            if (tour.maxAmountOfPeople <= 0) {
                throw new Exception("bad people amount");
            }
        }
    }
}
