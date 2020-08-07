using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TigerSpike.LandmarkRemark.Data.Repositories;
using TigerSpike.LandmarkRemark.Domain.Models;

namespace TigerSpike.LandmarkRemark.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LandmarksController : ControllerBase
    {
        private readonly ILogger<LandmarksController> _logger;
        private readonly ILandmarkRepository _landmarkRepository;

        public LandmarksController(ILogger<LandmarksController> logger, ILandmarkRepository landmarkRepository)
        {
            _logger = logger;
            _landmarkRepository = landmarkRepository;
        }

        [HttpPost("Add")]
        public Landmark Add([FromBody]Landmark landmark)
        {
            // Log the API call
            _logger.LogInformation("Adding a landmark to persistent storage");

            // Get or create the user that matches the passed in username
            var user = _landmarkRepository.GetUser(landmark.Username);
            if (user == null)
            {
                throw new Exception($"User '{landmark.Username}' not found");
            }

            // Add the landmark
            return _landmarkRepository.AddLandmark(user.Username, landmark.Latitude, landmark.Longitude, landmark.Comment);
        }

        [HttpGet]
        public IEnumerable<Landmark> GetAll()
        {
            // Log the API call
            _logger.LogInformation("Retrieving all landmarks from persistent storage");

            // Return the landmarks
            return _landmarkRepository.GetAllLandmarks();
        }
    }
}
