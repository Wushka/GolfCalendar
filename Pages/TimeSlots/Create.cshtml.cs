using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.TimeSlots
{
    public class CreateModel : PageModel
    {
        private readonly WebApplication1.Data.CalendarContext _context;

        public CreateModel(WebApplication1.Data.CalendarContext context)
        {
            _context = context;
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
          if (!ModelState.IsValid || _context.TimeSlot == null || TimeSlot == null)
            {
                return Page();
            }

            _context.TimeSlot.Add(TimeSlot);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
