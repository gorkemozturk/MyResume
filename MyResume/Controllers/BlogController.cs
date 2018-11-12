using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyResume.Models;

namespace MyResume.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            var posts = new[]
            {
                new Post
                {
                    Id = 1,
                    Title = "This is sample Post",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus vitae sapien porttitor, dignissim quam sit ame. Proin vitae tortor nec risus tristique efficitur. Aliquam luctus est urna, id aliquam orci tempus sed. Aenean sit amet leo id enim dapibus eleifend. Phasellus ut erat dapibus, tempor sapien non, porta urna.",
                    Author = "Görkem Öztürk",
                    CreatedAt = DateTime.Now
                },
                new Post
                {
                    Id = 2,
                    Title = "This is second Post",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus vitae sapien porttitor, dignissim quam sit ame. Proin vitae tortor nec risus tristique efficitur. Aliquam luctus est urna, id aliquam orci tempus sed. Aenean sit amet leo id enim dapibus eleifend. Phasellus ut erat dapibus, tempor sapien non, porta urna.",
                    Author = "Görkem Öztürk",
                    CreatedAt = DateTime.Now
                }
            };

            return View(posts);
        }
    }
}