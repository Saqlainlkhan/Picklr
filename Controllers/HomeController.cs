using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Picklr.Models;

namespace Picklr.Controllers
{
    public class HomeController : PicklrBaseController
    {
        private readonly PicklrContext context;

        public HomeController(PicklrContext ctx) => context = ctx;

        // Default home page — uses _MainLayout via _ViewStart
        public IActionResult Index()
        {
            return View();
        }

        // Client-facing pages
        public ContentResult About()
        {
            return Content("About page — under construction.");
        }

        public ContentResult Club()
        {
            return Content("Club page — under construction.");
        }

        // if clubId/date aren't passed in, use whatever was picked last time
        public IActionResult Program(int? clubId, string? date)
        {
            var picklrSession = new PicklrSession(HttpContext.Session);

            if (clubId.HasValue)
                picklrSession.SetSelectedClubId(clubId);
            else
                clubId = picklrSession.GetSelectedClubId();

            if (!string.IsNullOrEmpty(date))
                picklrSession.SetSelectedDate(date);
            else
                date = picklrSession.GetSelectedDate();

            // default to today so the date dropdown isn't blank
            if (string.IsNullOrEmpty(date))
            {
                date = DateTime.Today.ToString("yyyy-MM-dd");
                picklrSession.SetSelectedDate(date);
            }

            string? dayOfWeek = null;
            string? friendlyDate = null;
            if (DateTime.TryParse(date, out var parsedDate))
            {
                dayOfWeek = parsedDate.DayOfWeek.ToString();
                friendlyDate = parsedDate.ToString("dddd, MMMM d");
            }

            var query = context.Programs.Include(p => p.Club).AsQueryable();

            if (clubId.HasValue)
                query = query.Where(p => p.ClubID == clubId.Value);

            if (!string.IsNullOrEmpty(dayOfWeek))
                query = query.Where(p => p.AvailableDays.Contains(dayOfWeek));

            var viewModel = new ProgramListViewModel
            {
                SelectedClubId = clubId,
                SelectedDate = date,
                DayOfWeek = dayOfWeek,
                FriendlyDate = friendlyDate,
                Clubs = context.Clubs.OrderBy(c => c.Name).ToList(),
                DateOptions = BuildDateOptions(),
                Programs = query.OrderBy(p => p.Name).ToList(),
                CartCount = picklrSession.GetCartCount()
            };

            return View(viewModel);
        }

        public IActionResult ClearProgramFilter()
        {
            var picklrSession = new PicklrSession(HttpContext.Session);
            picklrSession.SetSelectedClubId(null);
            picklrSession.SetSelectedDate(null);
            return RedirectToAction("Program");
        }

        public IActionResult Shop()
        {
            return RedirectToAction("Index", "Cart");
        }

        private static List<DateOption> BuildDateOptions()
        {
            var options = new List<DateOption>();
            for (int i = 0; i < 14; i++)
            {
                var d = DateTime.Today.AddDays(i);
                string label = i switch
                {
                    0 => $"Today ({d:ddd, MMM d})",
                    1 => $"Tomorrow ({d:ddd, MMM d})",
                    _ => $"{d:ddd, MMM d}"
                };
                options.Add(new DateOption { Value = d.ToString("yyyy-MM-dd"), Label = label });
            }
            return options;
        }
    }
}
