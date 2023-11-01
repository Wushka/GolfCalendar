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
    public class DeleteModel : PageModel
    {
        private readonly WebApplication1.Data.CalendarContext _context;

        public DeleteModel(WebApplication1.Data.CalendarContext context)
        {
            _context = context;
        }

        [BindProperty]
      public TimeSlot TimeSlot { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TimeSlot == null)
            {
                return NotFound();
            }

            var timeslot = await _context.TimeSlot.FirstOrDefaultAsync(m => m.Id == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TimeSlot == null)
            {
                return NotFound();
            }
            var timeslot = await _context.TimeSlot.FindAsync(id);

            if (timeslot != null)
            {
                TimeSlot = timeslot;
                _context.TimeSlot.Remove(TimeSlot);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
