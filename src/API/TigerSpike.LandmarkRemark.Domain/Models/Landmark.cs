using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TigerSpike.LandmarkRemark.Domain.Models
{
    [Table("Landmarks", Schema ="dbo")]
    public class Landmark
    {
        // ID
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // User
        [ForeignKey("User")]
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        // Location
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

        // Comment
        [MaxLength(500)]
        public string Comment { get; set; }

        // Timestamp
        [Required]
        public DateTime Created { get; set; }
    }
}
