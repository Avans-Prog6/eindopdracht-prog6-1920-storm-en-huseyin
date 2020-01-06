using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeestjeOpJeFeestje.Data;
using BeestjeOpJeFeestje.Models;

namespace BeestjeOpJeFeestje
{
    public class DetailsModel : PageModel
    {
        private readonly BeestjeOpJeFeestje.Data.BeestjeOpJeFeestjeContext _context;

        public DetailsModel(BeestjeOpJeFeestje.Data.BeestjeOpJeFeestjeContext context)
        {
            _context = context;
        }

        public Animal Animal { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Animal = await _context.Animal.FirstOrDefaultAsync(m => m.ID == id);

            if (Animal == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
