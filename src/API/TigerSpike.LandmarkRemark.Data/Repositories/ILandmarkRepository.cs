using System.Collections.Generic;
using TigerSpike.LandmarkRemark.Domain.Models;

namespace TigerSpike.LandmarkRemark.Data.Repositories
{
    public interface ILandmarkRepository
    {
        User AddUser(string username, string fullname);

        User GetUser(string username);

        IEnumerable<User> GetAllUsers();

        Landmark AddLandmark(string username, double lat, double lng, string comment);

        IEnumerable<Landmark> GetAllLandmarks();

    }
}
