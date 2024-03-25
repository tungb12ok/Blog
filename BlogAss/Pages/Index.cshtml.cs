using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlogAss.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly FblogContext _context; // Thay FblogContext bằng DbContext của bạn
        public IndexModel(ILogger<IndexModel> logger)
        {
            _context = new FblogContext();
            _logger = logger;
        }

        public List<Article> Articles { get; set; }
        public List<Field> Fields { get; set; } 
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6; // Số lượng bài viết trên mỗi trang
        public int TotalPages { get; set; }


        public async Task<IActionResult> OnGetAsync(int? page)
        {
            if (Request.Query.ContainsKey("page"))
            {
                PageNumber = int.Parse(Request.Query["page"]);
            }
            else
            {
                PageNumber = 1; // Giá trị mặc định nếu không có query parameter.
            }

            int skip = (PageNumber - 1) * PageSize;

            Fields =  _context.Fields.ToList();
            //HttpContext.Session.Set<List<Field>>("Fields", Fields);

            Articles = await _context.Articles
                .Where(a => a.StatusId == 2)
                .Skip(skip)
                .Take(PageSize)
                .ToListAsync();

            int totalArticleCount = await _context.Articles.CountAsync();
            TotalPages = (int)Math.Ceiling((double)totalArticleCount / PageSize);

            return Page();
        }

    }
}