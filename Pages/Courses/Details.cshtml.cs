using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly WebApplication1.Data.CalendarContext _context;

        public DetailsModel(WebApplication1.Data.CalendarContext context)
        {
            _context = context;
        }

        public Course Course { get; set; } = default!;
        public List<TimeSlot> TimeSlots = default!;
        public int Overlapping = default!;
        public DateTime start_time = DateTime.Now;

        public async Task<IActionResult> OnGetAsync(int? id, DateTime start_time)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            else
            {
                Course = course;
                TimeSlots = TimeSlotsForDate(course.Id, start_time);
                
                DateTime start = new DateTime(2023, 10, 1, 1, 0, 0);
                DateTime end = new DateTime(2023, 10, 1, 6, 0, 0);
                Overlapping = OverlappingTimeSlots(1, start, end);
            }
            return Page();
        }

        public List<TimeSlot> TimeSlotsForDate(int course_id, DateTime date)
        {
            //FormattableString query = $"select * from TimeSlot where CourseId = {course_id} and StartTime between {date.Date} and {date.Date.AddDays(1)}";
            //return _context.TimeSlot.FromSql(query).ToList();
            var queryable = _context.TimeSlot.Where(ts => ts.CourseId == course_id)
                .Where(ts => ts.StartTime > date.Date)
                .Where(ts => ts.StartTime < date.Date.AddDays(1));
            return queryable.ToList();
        }
        public int OverlappingTimeSlots(int user_id, DateTime start, DateTime end)
        {
            var queryable = _context.TimeSlot.Where(ts => ts.UserId == user_id)
            .Where(ts => ts.StartTime < end)
            .Where(ts => ts.EndTime > start);
            return queryable.Count();
        }
    }
}
