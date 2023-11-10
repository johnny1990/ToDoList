using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ToDoListWebApp.Data;
using ToDoListWebApp.Models;

namespace ToDoListWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ToDoListContext _context;

        public HomeController(ILogger<HomeController> logger, ToDoListContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            var mc = new Category();
            var model = new ToDoVM();
            model.SearchFilters = new Filter(id);
            model.Categories = _context.Categories.ToList();
            model.Statuses = _context.Statuses.ToList();
            model.DueFilters = Filter.DueValues;

            IQueryable<ToDo> results = _context.ToDos.Include(c => c.Category).Include(s => s.Status);

            if (model.SearchFilters.HasDue)
            {
                var today = DateTime.Today;

                if (model.SearchFilters.IsPast)
                    results = results.Where(t => t.DueDate < today);
                else if (model.SearchFilters.IsFuture)
                    results = results.Where(t => t.DueDate > today);
                else if (model.SearchFilters.IsToday)
                    results = results.Where(t => t.DueDate == today);
            }

            var tasks = results.OrderBy(t => t.DueDate).ToList();

            model.Tasks = tasks;

            return View(model);
        }


        [HttpGet]
        public IActionResult New()
        {
            var model = new ToDoVM();
            model.Categories = _context.Categories.ToList();
            model.Statuses = _context.Statuses.ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult New(ToDoVM model)
        {

                _context.ToDos.Add(model.CurrentTask);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public IActionResult EditDelete([FromRoute] string id, ToDo selected)
        {
            if (selected.StatusId == null)
                _context.ToDos.Remove(selected);
            else
            {
                int newStatusId = 2;
                selected = _context.ToDos.Find(selected.Id);
                selected.StatusId = newStatusId;
                _context.ToDos.Update(selected);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Home", new { ID = id });
        }

        [HttpPost]
        public IActionResult FilterAction(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index", "Home", new { ID = id });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}