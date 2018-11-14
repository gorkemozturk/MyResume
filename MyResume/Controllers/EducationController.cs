using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyResume.Data;

namespace MyResume.Controllers
{
    public class EducationController : Controller
    {

        private readonly ApplicationDbContext _context;

        public EducationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var educations =  await _context.Educations.OrderBy(e => e.StartedAt).ToListAsync();

            return View(educations);
        }
    }
}