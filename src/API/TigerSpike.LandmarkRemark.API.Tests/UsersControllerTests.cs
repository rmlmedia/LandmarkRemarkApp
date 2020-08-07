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
    public class UsersControllerTests
    {
        [TestMethod]
        public void UsersController_Adds()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<UsersController>>();
            var repoMock = MockRepository();
            var controller = new UsersController(loggerMock.Object, repoMock.Object);

            // Act
            var user = GetTestUser();
            var result = controller.Add(user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Username, user.Username);
            Assert.AreEqual(result.Fullname, user.Fullname);
        }

        [TestMethod]
        public void UsersController_Retrieves()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<UsersController>>();
            var repoMock = MockRepository();
            var controller = new UsersController(loggerMock.Object, repoMock.Object);

            // Act
            var user = GetTestUser();
            var result = controller.Get(user.Username);
            var results = controller.GetAll();

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 1);
            var firstResult = results.FirstOrDefault();
            Assert.IsNotNull(firstResult);
            Assert.AreEqual(firstResult.Username, user.Username);
            Assert.AreEqual(firstResult.Fullname, user.Fullname);
        }

        private User GetTestUser()
        {
            return new User()
            {
                Username = "test.user",
                Fullname = "Test User"
            };
        }

        private Mock<ILandmarkRepository> MockRepository()
        {
            var testUser = GetTestUser();
            var mockRepo = new Mock<ILandmarkRepository>();
            mockRepo.Setup(repo => repo.GetUser(testUser.Username))
                .Returns(testUser);
            mockRepo.Setup(repo => repo.AddUser(testUser.Username, testUser.Fullname))
                .Returns(testUser);
            mockRepo.Setup(repo => repo.GetAllUsers())
                .Returns(new User[1].Select(l => testUser));

            return mockRepo;
        }
    }
}
