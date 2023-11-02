using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace WebApplication1.Models
{
    public class TimeSlot
    {
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int UserId { get; set; }


/*
convert to user TimeOnly and Date to get validation "for free" rather than code it
*/

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public User? User { get; private set; }
        public Course? Course { get; private set; }
    }
}
