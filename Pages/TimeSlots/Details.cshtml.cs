using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.TimeSlots
{
    public class DetailsModel : PageModel
    {
        private readonly WebApplication1.Data.CalendarContext _context;

        public DetailsModel(WebApplication1.Data.CalendarContext context)
        {
            _context = context;
        }

      public TimeSlot TimeSlot { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TimeSlot == null)
            {
                return NotFound();
            }

            var timeslot = await _context.TimeSlot
                .Where(ts => ts.Id == id)
                .FirstOrDefaultAsync();
            if (timeslot == null)
            {
                return NotFound();
            }
            else 
            {
                TimeSlot = timeslot;
            }
            return Page();
        }
    }
}
