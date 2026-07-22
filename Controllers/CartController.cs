using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Picklr.Models;

namespace Picklr.Controllers
{
    public class CartController : PicklrBaseController
    {
        private readonly PicklrContext context;

        public CartController(PicklrContext ctx) => context = ctx;

        private PicklrSession Session => new PicklrSession(HttpContext.Session);

        // GET /Cart
        public IActionResult Index()
        {
            return View(Session.GetCart());
        }

        [HttpPost]
        public IActionResult Add(int programId, string selectedDate)
        {
            var program = context.Programs.Include(p => p.Club)
                .FirstOrDefault(p => p.ProgramID == programId);

            if (program != null)
            {
                Session.AddToCart(new CartItem
                {
                    ProgramID = program.ProgramID,
                    ProgramName = program.Name,
                    ClubName = program.Club?.Name ?? string.Empty,
                    Fee = program.Fee,
                    SelectedDate = selectedDate
                });
                TempData["message"] = $"'{program.Name}' was added to your cart.";
            }

            return RedirectToAction("Program", "Home", new { clubId = program?.ClubID, date = selectedDate });
        }

        [HttpPost]
        public IActionResult ClearAll()
        {
            Session.ClearCart();
            TempData["message"] = "Cart cleared.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(string id)
        {
            Session.RemoveItem(id);
            TempData["message"] = "Item removed from your cart.";
            return RedirectToAction("Index");
        }

        // saves everything in the cart as a reservation, then clears it
        [HttpPost]
        public IActionResult PayAndConfirm()
        {
            var cart = Session.GetCart();

            if (cart.Count == 0)
            {
                TempData["message"] = "Your cart is empty.";
                return RedirectToAction("Index");
            }

            foreach (var item in cart)
            {
                context.Reservations.Add(new Reservation
                {
                    ProgramID = item.ProgramID,
                    ProgramName = item.ProgramName,
                    ClubName = item.ClubName,
                    Fee = item.Fee,
                    SelectedDate = item.SelectedDate
                });
            }
            context.SaveChanges();

            Session.ClearCart();
            TempData["message"] = $"Payment successful! {cart.Count} reservation(s) confirmed.";

            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
