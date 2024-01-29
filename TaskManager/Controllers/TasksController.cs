using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TaskManager.Models;
using Newtonsoft.Json;

namespace TaskManager.Controllers
{
    public class TasksController : Controller
    {
        private readonly TasksDbContext _context;

        public TasksController(TasksDbContext context)
        {
            _context = context;
        }

        //// GET: Tasks
        public async Task<IActionResult> Index()
        {
            var tasksDbContext = _context.Tasks.Include(t => t.Employee);
            return View(await tasksDbContext.ToListAsync());
        }

        // GET: Tasks
        [HttpPost]
        public async Task<string> Index([FromBody] Dictionary<string, int> parameters)
        {
            IQueryable<TaskManager.Models.Task> tasksDbContext = _context.Tasks.Include(t => t.Employee);
            //IQueryable<TaskManager.Models.Task> tasksDbContext = from task in _context.Tasks
            //                                                     join employee in _context.Employees on task.EmployeeId equals employee.id
            //                                                     select new { id = task.id, Name = task.Name, CompletionStatus = task.CompletionStatus,
            //                                                     Created = task.Created, Deadline = task.Deadline, Description = task.Description, 
            //                                                     EmployeeId = task.EmployeeId, Employee = employee};

            if (parameters["sortOption"] > 0)
            {
                if (parameters["sortOrderOption"] == 0)
                {
                    if (parameters["sortOption"] == 1)
                    {
                        tasksDbContext = tasksDbContext.OrderBy(t => t.Deadline);
                    }
                    else
                    {
                        tasksDbContext = tasksDbContext.OrderBy(t => t.Created);
                    }
                }
                else
                {
                    if (parameters["sortOption"] == 2)
                    {
                        tasksDbContext = tasksDbContext.OrderByDescending(t => t.Deadline);
                    }
                    else
                    {
                        tasksDbContext = tasksDbContext.OrderByDescending(t => t.Created);
                    }
                }
            }
            var resultData = await tasksDbContext.ToListAsync();

            string result = JsonConvert.SerializeObject(resultData, Formatting.Indented);
            
            return result;
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "id", "Email");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,CompletionStatus,Created,Deadline,Description,EmployeeId")] TaskManager.Models.Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "id", "Email", task.EmployeeId);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "id", "Email", task.EmployeeId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,CompletionStatus,Created,Deadline,Description,EmployeeId")] TaskManager.Models.Task task)
        {
            if (id != task.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "id", "Email", task.EmployeeId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.id == id);
        }
    }
}
