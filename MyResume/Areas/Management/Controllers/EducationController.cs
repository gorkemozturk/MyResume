using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyResume.Data;
using MyResume.Models;

namespace MyResume.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]
    public class EducationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EducationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var educations = await _context.Educations.OrderByDescending(e => e.CreatedAt).ToListAsync();

            return View(educations);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Education education)
        {
            if (!ModelState.IsValid)
                return View(education);

            education.CreatedAt = DateTime.Now;

            _context.Educations.Add(education);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int ?id)
        {
            if (id == null)
                return NotFound();

            var education = await _context.Educations.FindAsync(id);

            if (education == null)
                return NotFound();

            return View(education);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Education education)
        {
            if (id != education.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return NotFound();

            _context.Entry(education).State = EntityState.Modified;
            _context.Entry(education).Property("CreatedAt").IsModified = false;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var education = await _context.Educations.FindAsync(id);

            if (education == null)
                return NotFound();

            return View(education);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var education = await _context.Educations.FirstOrDefaultAsync(p => p.Id == id);

            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}