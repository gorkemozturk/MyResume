using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyResume.Data;
using MyResume.Models;

namespace MyResume.Areas.Management.Controllers
{
    [Area("Management")]
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var posts = _context.Posts.ToList();

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
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }
    }
}