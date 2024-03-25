using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlogAss.Pages
{
    public class DetailModel : PageModel
    {
        private FblogContext _context = new FblogContext();

        public Article Article { get; set; }
        public List<Field> Fields { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Fields = _context.Fields.ToList();

            Article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);

            if (Article == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
