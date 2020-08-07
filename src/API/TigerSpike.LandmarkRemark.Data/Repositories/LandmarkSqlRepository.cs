using System;
using System.Linq;
using System.Collections.Generic;
using TigerSpike.LandmarkRemark.Data.Contexts;
using TigerSpike.LandmarkRemark.Domain.Models;

namespace TigerSpike.LandmarkRemark.Data.Repositories
{
    public class LandmarkSqlRepository : ILandmarkRepository
    {
        private LandmarkDbSqlContext _dbContext;

        public LandmarkSqlRepository(LandmarkDbSqlContext dbContext = null)
        {
            _dbContext = dbContext ?? new LandmarkDbSqlContext();
        }

        // Add a new user object to the database
        public User AddUser(string username, string fullname)
        {
            var user = GetUser(username);

            if (user != null)
            {
                user.Fullname = fullname;
                user.Created = DateTime.Now;
            }
            else
            {
                user = _dbContext.Users.Add(new User()
                {
                    Username = username,
                    Fullname = fullname,
                    Created = DateTime.Now
                })?.Entity;
            }

            _dbContext.SaveChanges();
            return user;
        }

        // Get the user object from the database that matches username
        public User GetUser(string username)
        {
            return _dbContext.Users.FirstOrDefault(user => user.Username == username);
        }

        // Get all user objects from the database
        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        // Add a landmark to the database
        public Landmark AddLandmark(string username, double lat, double lng, string comment)
        {
            var landmark = GetLandmark(username, lat, lng);

            if (landmark != null)
            {
                landmark.Comment = comment;
                landmark.Created = DateTime.Now;
            }
            else
            {
                landmark = _dbContext.Landmarks.Add(new Landmark()
                {
                    Username = username,
                    Latitude = lat,
                    Longitude = lng,
                    Comment = comment,
                    Created = DateTime.Now
                })?.Entity;
            }

            _dbContext.SaveChanges();
            return landmark;
        }

        // Get the landmark object from the database that matched the username, lat and lng
        public Landmark GetLandmark(string username, double lat, double lng)
        {
            return _dbContext.Landmarks.FirstOrDefault(landmark => landmark.Username == username && landmark.Latitude == lat && landmark.Longitude == lng);
        }

        // Get all landmarks from the database
        public IEnumerable<Landmark> GetAllLandmarks()
        {
            return _dbContext.Landmarks.ToList();
        }
    }
}
