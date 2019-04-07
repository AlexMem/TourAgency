using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TourAgency.Contexts;
using TourAgency.Models;

namespace TourAgency.Repositories.Implementations {
    public class TourRepository : ITourRepository {

        public List<Tour> getAll() {
            List<Tour> tours;
            using (var dbContext = new TourAgencyDbContext()) {
                tours = dbContext.tours
                                 .Include(t => t.type)
                                 .Include(t => t.user)
                                 .ToList();
            }
            return tours;
        }
        
        public Tour findById(long id) {
            Tour tour;
            using (var dbContext = new TourAgencyDbContext()) {
                tour = dbContext.tours
                                .Include(t => t.type)
                                .Include(t => t.user)
                                .ToList()
                                .Find(t => t.tourId == id);
            }
            return tour;
        }
        
        public Tour create(Tour newTour) {
            EntityEntry<Tour> tour;
            using (var dbContext = new TourAgencyDbContext()) {
                dbContext.types.Attach(newTour.type);
                dbContext.users.Attach(newTour.user);
                tour = dbContext.tours.Add(newTour);
                dbContext.SaveChanges();
            }
            return tour.Entity;
        }

        public Tour update(Tour tour) {
            EntityEntry<Tour> updatedTour;
            using (var dbContext = new TourAgencyDbContext()) {
                dbContext.types.Attach(tour.type);
                dbContext.users.Attach(tour.user);
                updatedTour = dbContext.tours.Update(tour);
                dbContext.SaveChanges();
            }
            return updatedTour.Entity;
        }

        public Tour delete(Tour tour) {
            EntityEntry<Tour> removedTour;
            using (var dbContext = new TourAgencyDbContext()) {
                dbContext.types.Attach(tour.type);
                dbContext.users.Attach(tour.user);
                removedTour = dbContext.tours.Remove(tour);
                dbContext.SaveChanges();
            }
            return removedTour.Entity;
        }

        public List<Tour> findByHot(bool isHot) {
            List<Tour> tours;
            using (var dbContext = new TourAgencyDbContext()) {
                tours = dbContext.tours
                                 .Include(t => t.type)
                                 .Include(t => t.user)
                                 .ToList()
                                 .FindAll(t => t.isHot.Equals(isHot));
            }
            return tours;
        }

        public List<Tour> findByUser(User user) {
            List<Tour> tours;
            using (var dbContext = new TourAgencyDbContext()) {
                tours = dbContext.tours
                                 .Include(t => t.type)
                                 .Include(t => t.user)
                                 .ToList()
                                 .FindAll(t => t.user.userId.Equals(user.userId));
            }
            return tours;
        }
        
        public List<Tour> findByUserEmail(string email) {
            List<Tour> tours;
            using (var dbContext = new TourAgencyDbContext()) {
                tours = dbContext.tours
                                 .Include(t => t.type)
                                 .Include(t => t.user)
                                 .ToList()
                                 .FindAll(t => t.user.email.Equals(email));
            }
            return tours;
        }
    }
}
