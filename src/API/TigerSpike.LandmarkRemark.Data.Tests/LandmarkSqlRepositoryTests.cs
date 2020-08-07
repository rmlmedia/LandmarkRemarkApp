using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TigerSpike.LandmarkRemark.Data.Contexts;
using TigerSpike.LandmarkRemark.Data.Repositories;
using TigerSpike.LandmarkRemark.Domain.Models;

namespace TigerSpike.LandmarkRemark.Data.Tests
{
    [TestClass]
    public class LandmarkSqlRepositoryTests
    {
        [TestMethod]
        public void UsersController_Adds()
        {
            // Arrange
            var testUser = GetTestUser();
            var testLandmark = GetTestLandmark();
            var dbContextMock = MockDbContext();
            var repository = new LandmarkSqlRepository(dbContextMock);

            // Act
            var user = repository.AddUser(testUser.Username, testUser.Fullname);
            var landmark = repository.AddLandmark(testLandmark.Username, testLandmark.Latitude, testLandmark.Longitude, testLandmark.Comment);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(user.Username, testUser.Username);
            Assert.AreEqual(user.Fullname, testUser.Fullname);
            Assert.IsNotNull(landmark);
            Assert.AreEqual(landmark.Username, testLandmark.Username);
            Assert.AreEqual(landmark.Latitude, testLandmark.Latitude);
            Assert.AreEqual(landmark.Longitude, testLandmark.Longitude);
            Assert.AreEqual(landmark.Comment, testLandmark.Comment);
        }

        [TestMethod]
        public void UsersController_Retrieves()
        {
            // Arrange
            var testUser = GetTestUser();
            var testLandmark = GetTestLandmark();
            var dbContextMock = MockDbContext();
            var repository = new LandmarkSqlRepository(dbContextMock);

            // Act
            var user = repository.GetUser(testUser.Username);
            var users = repository.GetAllUsers();
            var landmark = repository.GetLandmark(testLandmark.Username, testLandmark.Latitude, testLandmark.Longitude);
            var landmarks = repository.GetAllLandmarks();

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(user.Username, testUser.Username);
            Assert.AreEqual(user.Fullname, testUser.Fullname);

            Assert.IsNotNull(users);
            Assert.AreEqual(users.Count(), 1);
            var firstUser = users.FirstOrDefault();
            Assert.IsNotNull(firstUser);
            Assert.AreEqual(firstUser.Username, testUser.Username);
            Assert.AreEqual(firstUser.Fullname, testUser.Fullname);

            Assert.IsNotNull(landmark);
            Assert.AreEqual(landmark.Username, testLandmark.Username);
            Assert.AreEqual(landmark.Latitude, testLandmark.Latitude);
            Assert.AreEqual(landmark.Longitude, testLandmark.Longitude);
            Assert.AreEqual(landmark.Comment, testLandmark.Comment);

            Assert.IsNotNull(landmarks);
            Assert.AreEqual(landmarks.Count(), 1);
            var firstLandmark = landmarks.FirstOrDefault();
            Assert.IsNotNull(firstLandmark);
            Assert.AreEqual(firstLandmark.Username, testLandmark.Username);
            Assert.AreEqual(firstLandmark.Latitude, testLandmark.Latitude);
            Assert.AreEqual(firstLandmark.Longitude, testLandmark.Longitude);
            Assert.AreEqual(firstLandmark.Comment, testLandmark.Comment);
        }

        private User GetTestUser()
        {
            return new User()
            {
                Username = "test.user",
                Fullname = "Test User"
            };
        }

        private Landmark GetTestLandmark()
        {
            return new Landmark()
            {
                Username = "test.user",
                Latitude = 100,
                Longitude = 200,
                Comment = "Test comment"
            };
        }

        private LandmarkDbSqlContext MockDbContext()
        {
            // Insert seed data into the database using one instance of the context
            using (var context = new LandmarkDbSqlContext(true))
            {
                try
                {
                    context.Users.Add(GetTestUser());
                    context.Landmarks.Add(GetTestLandmark());
                    context.SaveChanges();
                }
                catch { }
            }

            // Use a clean instance of the context to run the tests against
            return new LandmarkDbSqlContext(true);
        }
    }
}
