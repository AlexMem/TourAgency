using System;
using System.Collections.Generic;
using TourAgency.Models;
using TourAgency.Repositories;

namespace TourAgency.Services {
    public class TourService {
        private readonly TourRepository tourRepository;

        public TourService(TourRepository tourRepository) {
            this.tourRepository = tourRepository;
        }

        public List<Tour> getAll() {
            return tourRepository.getAll();
        }

        public Tour get(object[] keyValues) {
            Tour tour = tourRepository.get(keyValues);
            if (tour == null) {
                //TODO
                throw new Exception("Tour not found");
            }

            return tour;
        }

        public Tour create(Tour newTour) {
            verify(newTour);
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
            //TODO verification
            throw new NotImplementedException();
        }
    }
}