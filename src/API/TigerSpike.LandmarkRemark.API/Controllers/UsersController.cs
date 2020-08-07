using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TigerSpike.LandmarkRemark.Data.Repositories;
using TigerSpike.LandmarkRemark.Domain.Models;

namespace TigerSpike.LandmarkRemark.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly ILandmarkRepository _landmarkRepository;

        public UsersController(ILogger<UsersController> logger, ILandmarkRepository landmarkRepository)
        {
            _logger = logger;
            _landmarkRepository = landmarkRepository;
        }

        [HttpPost("Add")]
        public User Add([FromBody]User newUser)
        {
            // Log the API call
            _logger.LogInformation("Adding a new user to persistent storage");

            // Get or create the user that matches the passed in username
            var user = _landmarkRepository.GetUser(newUser.Username);
            if (user == null)
            {
                user = _landmarkRepository.AddUser(newUser.Username, newUser.Fullname ?? newUser.Username);
            }

            // Return the freshly created user object
            return user;
        }

        [HttpGet("{username}")]
        public User Get([FromRoute]string username)
        {
            // Log the API call
            _logger.LogInformation($"Getting user information for {username} from persistent storage");

            // Return the user data
            return _landmarkRepository.GetUser(username);
        }

        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            // Log the API call
            _logger.LogInformation($"Getting all users from persistent storage");

            // Return all users
            return _landmarkRepository.GetAllUsers();
        }
    }
}
