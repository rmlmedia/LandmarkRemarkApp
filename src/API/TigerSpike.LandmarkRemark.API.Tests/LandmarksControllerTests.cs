using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using TigerSpike.LandmarkRemark.API.Controllers;
using TigerSpike.LandmarkRemark.Data.Repositories;
using TigerSpike.LandmarkRemark.Domain.Models;

namespace TigerSpike.LandmarkRemark.API.Tests
{
    [TestClass]
    public class LandmarksControllerTests
    {
        [TestMethod]
        public void LandmarksController_Adds()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<LandmarksController>>();
            var repoMock = MockRepository();
            var controller = new LandmarksController(loggerMock.Object, repoMock.Object);

            // Act
            var landmark = GetTestLandmark();
            var result = controller.Add(landmark);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Username, landmark.Username);
            Assert.AreEqual(result.Latitude, landmark.Latitude);
            Assert.AreEqual(result.Longitude, landmark.Longitude);
            Assert.AreEqual(result.Comment, landmark.Comment);
        }

        [TestMethod]
        public void LandmarksController_Retrieves()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<LandmarksController>>();
            var repoMock = MockRepository();
            var controller = new LandmarksController(loggerMock.Object, repoMock.Object);

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 1);

            var firstResult = result.FirstOrDefault();
            var landmark = GetTestLandmark();
            Assert.IsNotNull(firstResult);
            Assert.AreEqual(firstResult.Username, landmark.Username);
            Assert.AreEqual(firstResult.Latitude, landmark.Latitude);
            Assert.AreEqual(firstResult.Longitude, landmark.Longitude);
            Assert.AreEqual(firstResult.Comment, landmark.Comment);
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

        private Mock<ILandmarkRepository> MockRepository()
        {
            var testUser = GetTestUser();
            var testLandmark = GetTestLandmark();
            var mockRepo = new Mock<ILandmarkRepository>();
            mockRepo.Setup(repo => repo.GetUser(testUser.Username))
                .Returns(testUser);
            mockRepo.Setup(repo => repo.AddLandmark(testLandmark.Username, testLandmark.Latitude, testLandmark.Longitude, testLandmark.Comment))
                .Returns(testLandmark);
            mockRepo.Setup(repo => repo.GetAllLandmarks())
                .Returns(new Landmark[1].Select(l => testLandmark));

            return mockRepo;
        }
    }
}
