using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class AssignedTasksController : Controller
    {
        private readonly TasksDbContext _context;

        public AssignedTasksController(TasksDbContext context)
        {
            _context = context;
        }

        // GET: AssignedTasks
        public async Task<IActionResult> Index()
        {
            var tasksDbContext = _context.AssignedTasks.Include(a => a.employee).Include(a => a.task);
            return View(await tasksDbContext.ToListAsync());
        }

        // GET: AssignedTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignedTask = await _context.AssignedTasks
                .Include(a => a.employee)
                .Include(a => a.task)
                .FirstOrDefaultAsync(m => m.id == id);
            if (assignedTask == null)
            {
                return NotFound();
            }

            return View(assignedTask);
        }

        // GET: AssignedTasks/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "id", "Email");
            ViewData["TaskId"] = new SelectList(_context.Tasks, "id", "name");
            return View();
        }

        // POST: AssignedTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,EmployeeRole,EmployeeId,TaskId")] AssignedTask assignedTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assignedTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "id", "Email", assignedTask.EmployeeId);
            ViewData["TaskId"] = new SelectList(_context.Tasks, "id", "name", assignedTask.TaskId);
            return View(assignedTask);
        }

        // GET: AssignedTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignedTask = await _context.AssignedTasks.FindAsync(id);
            if (assignedTask == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "id", "Email", assignedTask.EmployeeId);
            ViewData["TaskId"] = new SelectList(_context.Tasks, "id", "name", assignedTask.TaskId);
            return View(assignedTask);
        }

        // POST: AssignedTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,EmployeeRole,EmployeeId,TaskId")] AssignedTask assignedTask)
        {
            if (id != assignedTask.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignedTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignedTaskExists(assignedTask.id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "id", "Email", assignedTask.EmployeeId);
            ViewData["TaskId"] = new SelectList(_context.Tasks, "id", "name", assignedTask.TaskId);
            return View(assignedTask);
        }

        // GET: AssignedTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignedTask = await _context.AssignedTasks
                .Include(a => a.employee)
                .Include(a => a.task)
                .FirstOrDefaultAsync(m => m.id == id);
            if (assignedTask == null)
            {
                return NotFound();
            }

            return View(assignedTask);
        }

        // POST: AssignedTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignedTask = await _context.AssignedTasks.FindAsync(id);
            if (assignedTask != null)
            {
                _context.AssignedTasks.Remove(assignedTask);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignedTaskExists(int id)
        {
            return _context.AssignedTasks.Any(e => e.id == id);
        }
    }
}
