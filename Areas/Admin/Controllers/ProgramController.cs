using Microsoft.AspNetCore.Mvc;
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
            var programs = context.Programs.OrderBy(p => p.Name).ToList();
            return View(programs);
        }

        // GET /Admin/Program/AddEdit        — blank form (Add)
        // GET /Admin/Program/AddEdit/2      — loads existing record (Edit)
        [HttpGet]
        public IActionResult AddEdit(int? id)
        {
            var program = (id == null)
                ? new PicklProgram()
                : context.Programs.Find(id) ?? new PicklProgram();

            ViewBag.Action = (id == null) ? "Add" : "Edit";
            return View(program);
        }

        [HttpPost]
        public IActionResult AddEdit(PicklProgram program)
        {
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
            return View(program);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var program = context.Programs.Find(id) ?? new PicklProgram();
            return View(program);
        }

        [HttpPost]
        public IActionResult Delete(PicklProgram program)
        {
            context.Programs.Remove(program);
            context.SaveChanges();
            TempData["message"] = $"'{program.Name}' was deleted.";
            return RedirectToAction("List"); // PRG
        }
    }
}
