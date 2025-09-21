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
            var email = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;
            Console.WriteLine(email);
            var tasks = _context.Tasks
                                .Where(t => t.UserEmail == email)
                                .ToList();
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
            var email = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;
            Console.WriteLine(email);
            if (email == null)
            {
                return Unauthorized(); // user not logged in properly
            }

            task.UserEmail = email;

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState invalid!");
                foreach (var kvp in ModelState)
                {
                    foreach (var error in kvp.Value.Errors)
                    {
                        Console.WriteLine($"{kvp.Key}: {error.ErrorMessage}");
                    }
                }
                return View(task);
            }

            if (ModelState.IsValid)
            {
                Console.WriteLine("ModelState invalid!");
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

                task.Id = updatedTask.Id;
                task.TaskInfo = updatedTask.TaskInfo;
                task.Status = updatedTask.Status;
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
