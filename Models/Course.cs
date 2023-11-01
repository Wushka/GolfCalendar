using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public ICollection<TimeSlot>? TimeSlots {  get; }
    }
}
