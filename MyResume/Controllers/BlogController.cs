using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyResume.Data;
using MyResume.Models;

namespace MyResume.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IActionResult Index()
        {
            var posts = _context.Posts.ToList();

            return View(posts);
        }

        public IActionResult Post()
        { 
            return View();
        }
    }
}