using DataAccess.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogAss.Pages
{
    public class FieldModel : PageModel
    {
        private readonly ILogger<FieldModel> _logger;
        private readonly FblogContext _context;

        public FieldModel(ILogger<FieldModel> logger)
        {
            _context = new FblogContext();
            _logger = logger;
        }

        public List<Article> Articles { get; set; }
        public List<Field> Fields { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public int TotalPages { get; set; }
        public int fID { get; set; } // Thêm biến fID

        public async Task<IActionResult> OnGetAsync(int? field)
        {
            int skip = (PageNumber - 1) * PageSize;

            if (field.HasValue)
            {
                fID = field.Value; // Gán giá trị từ tham số field cho fID
                                   // Lọc các bài viết dựa trên giá trị field (nếu field có giá trị)
                Articles = await _context.Articles
                    .Where(x => x.FieldId == fID)
                    .Skip(skip)
                    .Take(PageSize)
                    .ToListAsync();
            }
            else
            {
                fID = 0; // Gán giá trị mặc định (hoặc giá trị khác nếu cần)
                         // Lọc tất cả các bài viết nếu không có giá trị field
                Articles = await _context.Articles
                    .Skip(skip)
                    .Take(PageSize)
                    .ToListAsync();
            }

            // Lấy danh sách các lĩnh vực
            Fields = _context.Fields.ToList();

            int totalArticleCount = await _context.Articles.CountAsync();
            TotalPages = (int)Math.Ceiling((double)totalArticleCount / PageSize);

            return Page();
        }
    }
}
