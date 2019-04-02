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

        public Tour findById(long id) {
            return tourRepository.findById(id);
        }

        public Tour create(Tour newTour) {
            verify(newTour);
            if (newTour.fromDate.CompareTo(DateTime.Now.ToBinary() - 6*TimeSpan.TicksPerHour) < 0) {
                newTour.isActive = true;
            } else {
                newTour.isActive = false;
            }
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
            if (tour.fromDate.CompareTo(DateTime.Now.ToBinary()) >= 0) {
                throw new Exception("bad time");
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