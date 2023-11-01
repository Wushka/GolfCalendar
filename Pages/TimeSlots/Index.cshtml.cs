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
    public class IndexModel : PageModel
    {
        private readonly WebApplication1.Data.CalendarContext _context;

        public IndexModel(WebApplication1.Data.CalendarContext context)
        {
            _context = context;
        }

        public IList<TimeSlot> TimeSlot { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.TimeSlot != null)
            {
                TimeSlot = await _context.TimeSlot.ToListAsync();
            }
        }
    }
}
