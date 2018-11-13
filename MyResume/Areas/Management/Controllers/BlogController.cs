using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyResume.Data;
using MyResume.Models;
using MyResume.Models.ViewModels;

namespace MyResume.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _context.Posts
                        .Include(p => p.User)
                        .OrderByDescending(p => p.CreatedAt)
                        .ToListAsync();

            return View(posts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Post post)
        {
            if (!ModelState.IsValid)
                return View(post);

            var identity = (ClaimsIdentity)this.User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            post.UserId = claim.Value;
            post.CreatedAt = DateTime.Now;

            _context.Posts.Add(post);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int ?id)
        {
            if (id == null)
                return NotFound();

            var post = _context.Posts.FirstOrDefault(p => p.Id == id);

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Post post)
        {
            if (!ModelState.IsValid)
                return View();

            _context.Entry(post).State = EntityState.Modified;
            _context.Entry(post).Property("CreatedAt").IsModified = false;
            _context.Entry(post).Property("UserId").IsModified = false;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }
    }
}