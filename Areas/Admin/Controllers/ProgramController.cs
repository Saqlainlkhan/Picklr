using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Picklr.Models;

namespace Picklr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProgramController : Controller
    {
        private PicklrContext context;

        public ProgramController(PicklrContext ctx)
        {
            context = ctx;
        }

        // GET /Admin/Program/List
        public IActionResult List()
        {
            var programs = context.Programs.Include(p => p.Club).OrderBy(p => p.Name).ToList();
            return View(programs);
        }

        // GET /Admin/Program/AddEdit        — blank form (Add)
        // GET /Admin/Program/AddEdit/2      — loads existing record (Edit)
        [HttpGet]
        public IActionResult AddEdit(int? id)
        {
            var program = (id == null)
                ? new PicklrProgram()
                : context.Programs.Find(id) ?? new PicklrProgram();

            ViewBag.Action = (id == null) ? "Add" : "Edit";
            ViewBag.Clubs = context.Clubs.OrderBy(c => c.Name).ToList();
            return View(program);
        }

        [HttpPost]
        public IActionResult AddEdit(PicklrProgram program, string[] days)
        {
            program.AvailableDays = string.Join(",", days ?? Array.Empty<string>());

            if (ModelState.IsValid)
            {
                if (program.ProgramID == 0)
                    context.Programs.Add(program);
                else
                    context.Programs.Update(program);

                context.SaveChanges();
                TempData["message"] = $"'{program.Name}' was saved successfully.";
                return RedirectToAction("List"); // PRG
            }

            ViewBag.Action = (program.ProgramID == 0) ? "Add" : "Edit";
            ViewBag.Clubs = context.Clubs.OrderBy(c => c.Name).ToList();
            return View(program);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var program = context.Programs.Find(id) ?? new PicklrProgram();
            return View(program);
        }

        [HttpPost]
        public IActionResult Delete(PicklrProgram program)
        {
            context.Programs.Remove(program);
            context.SaveChanges();
            TempData["message"] = $"'{program.Name}' was deleted.";
            return RedirectToAction("List"); // PRG
        }
    }
}
