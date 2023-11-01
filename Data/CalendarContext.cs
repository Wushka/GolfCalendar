using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class CalendarContext : DbContext
    {
        public CalendarContext (DbContextOptions<CalendarContext> options)
            : base(options)
        {
        }

        public DbSet<WebApplication1.Models.User> User { get; set; } = default!;
        public DbSet<WebApplication1.Models.Course> Course { get; set; } = default!;
        public DbSet<WebApplication1.Models.TimeSlot> TimeSlot { get; set; } = default!;
    }
}
