using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TigerSpike.LandmarkRemark.Domain.Models
{
    [Table("Users", Schema = "dbo")]
    public class User
    {
        // User Details
        [Key]
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Fullname { get; set; }

        // Timestamp
        [Required]
        public DateTime Created { get; set; }
    }
}
