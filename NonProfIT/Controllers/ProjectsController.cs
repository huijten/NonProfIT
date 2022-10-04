using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NonProfIT.Areas.Identity.Data;
using NonProfIT.Models;

namespace NonProfIT.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Project.Include(p => p.Creator).Include(p => p.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Project == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(p => p.Creator)
                .Include(p => p.Student)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles = "Employee")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> SignUp([Bind("ProjectId")] int ProjectId)
        {
            Project project = await _context.Project.FindAsync(ProjectId);
            ApplicationUser student = await GetCurrentUserAsync();
            project.Student = student;
            project.StudentId = student.Id;

            _context.Add(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Compensation")] Project project)
        {
            project.Student = null;
            project.Available = true;
            ApplicationUser creator = await GetCurrentUserAsync();
            project.CreatorId = creator.Id;
            project.Creator = creator;

            // This method always returns false, dont know why

            _context.Add(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            return View(project);
        }

        // Method to get the current user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Projects/Edit/5
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Project == null)
            {
                return NotFound();
            }

            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "Id", project.CreatorId);
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Id", project.StudentId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,Name,Description,Compensation,CreatorId,StudentId,Available")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "Id", project.CreatorId);
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Id", project.StudentId);
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Project == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(p => p.Creator)
                .Include(p => p.Student)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [Authorize(Roles = "Employee")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Project == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Project'  is null.");
            }

            // Check if person made the project, if not it cannot be deleted.
            var project = await _context.Project.FindAsync(id);
            if (project.Creator != await GetCurrentUserAsync())
            {
                return Problem("You do not have permission to remove this project.");
            }

            if (project != null)
            {
                _context.Project.Remove(project);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
          return _context.Project.Any(e => e.ProjectId == id);
        }
    }
}
