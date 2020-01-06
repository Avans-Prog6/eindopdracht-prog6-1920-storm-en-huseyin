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
    public class IndexModel : PageModel
    {
        private readonly BeestjeOpJeFeestje.Data.BeestjeOpJeFeestjeContext _context;

        public IndexModel(BeestjeOpJeFeestje.Data.BeestjeOpJeFeestjeContext context)
        {
            _context = context;
        }

        public IList<Animal> Animal { get;set; }

        public async Task OnGetAsync()
        {
            Animal = await _context.Animal.ToListAsync();
        }
    }
}
