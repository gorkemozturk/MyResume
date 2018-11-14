using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyResume.Data;
using MyResume.Models;

namespace MyResume.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 0)
        {
            var pageSize = 2;
            var totalPosts = _context.Posts.Count();
            var totalPages = totalPosts / pageSize;
            var previousPage = page - 1;
            var nextPage = page + 1;

            ViewBag.PreviousPage = previousPage;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.NextPage = nextPage;
            ViewBag.HasNextPage = nextPage < totalPages;

            var posts = await _context.Posts
                        .Include(p => p.User)
                        .OrderByDescending(p => p.CreatedAt)
                        .Skip(pageSize * page)
                        .Take(pageSize)
                        .ToListAsync();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(posts);

            return View(posts);
        }

        [Route("Blog/Post/{year:min(2000)}/{month:range(1,12)}/{id}")]
        public async Task<IActionResult> Post(int ?year, int ?month, int ?id)
        {
            if (year == null || month == null || id == null)
                return NotFound();

            var post = await _context.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);

            return View(post);
        }
    }
}