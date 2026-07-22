using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Picklr.Models;

namespace Picklr.Controllers
{
    public abstract class PicklrBaseController : Controller
    {
        // so the cart badge in the navbar is up to date on every page
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewBag.CartCount = new PicklrSession(HttpContext.Session).GetCartCount();
        }
    }
}
