using ManagerQM.Data;
using ManagerQM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ManagerQM.Controllers
{
    public class TaskController : Controller
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        //[Authorize]
        // ---------------- READ ----------------
        [Route("tasks/gettasks")]
        public IActionResult GetTasks()
        {
            var tasks = _context.Tasks.ToList();
            return View(tasks);  // Pass tasks to a View
        }

        // ---------------- CREATE ----------------
        [HttpGet]
        [Route("tasks/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("tasks/create")]
        public IActionResult Create(UserTask task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction("GetTasks");
            }
            return View(task);
        }

        // ---------------- UPDATE ----------------
        [HttpGet]
        [Route("tasks/edit/{id}")]
        public IActionResult Edit(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();

            return View(task);
        }

        [HttpPost]
        [Route("tasks/edit/{id}")]
        public IActionResult Edit(int id, UserTask updatedTask)
        {

            if (id != updatedTask.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var task = _context.Tasks.Find(id);
                if (task == null) return NotFound();

                task = updatedTask;
                _context.SaveChanges();

                return RedirectToAction("GetTasks");
            }
            return View(updatedTask);
        }



        // ---------------- DELETE ----------------
        [HttpGet]
        [Route("tasks/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [Route("tasks/delete/{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return RedirectToAction("GetTasks");
        }

    }
}
