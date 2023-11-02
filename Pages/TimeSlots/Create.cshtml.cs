using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.TimeSlots
{
    public class CreateModel : PageModel
    {

        private const int CLOSING_TIME_HOUR = 19;
        private const int OPENING_TIME_HOUR = 7;
        private const int BUFFER_MINUTES = 10;
        private const int RESERVATION_TIME_SPAN_HOURS = 5;

        private readonly WebApplication1.Data.CalendarContext _context;
        public List<SelectListItem> CoursesListItems = new List<SelectListItem>();
        public List<SelectListItem> UsersListItems = new List<SelectListItem>();
        public CreateModel(WebApplication1.Data.CalendarContext context)
        {
            _context = context;

            foreach (Course c in _context.Course.ToList())
            {
                CoursesListItems.Add(new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
            }
            foreach (User u in _context.User.ToList())
            {
                UsersListItems.Add(new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            }
        }

        public IActionResult OnGet()
        {

            return Page();
        }

        [BindProperty]
        public TimeSlot TimeSlot { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            /*
            Add 10 minute buffer if not bordering on closing time
            Add overlap check for same User on all courses
            Add overlap check for number of concurrent players (4 or less)
            */

            TimeSpan closing_time = new TimeSpan(CLOSING_TIME_HOUR, 0, 0);
            TimeSpan buffer_time = new TimeSpan(0, BUFFER_MINUTES, 0);

            DateTime pot_end_time = TimeSlot.StartTime + new TimeSpan(RESERVATION_TIME_SPAN_HOURS, 0, 0);

            if (pot_end_time.TimeOfDay < closing_time)
            {
                if (pot_end_time.TimeOfDay < (closing_time - buffer_time))
                {
                    TimeSlot.EndTime = pot_end_time + new TimeSpan(0, BUFFER_MINUTES, 0); ;
                }
                else
                {
                    TimeSlot.EndTime = pot_end_time;
                }
            }
            else
            {
                ModelState.AddModelError("StartTime", "StartTime is to late in the day");
            }

            if (!ModelState.IsValid || _context.TimeSlot == null || TimeSlot == null)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                
                ViewData["ErrorMessage"] = message;
                return Page();
            }

            _context.TimeSlot.Add(TimeSlot);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
