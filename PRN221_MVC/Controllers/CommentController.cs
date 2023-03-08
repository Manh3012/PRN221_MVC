using Microsoft.AspNetCore.Mvc;
using PRN221_MVC.Models;

namespace PRN221_MVC.Controllers {
    public class CommentController : Controller {
        [HttpPost]
        public IActionResult Create(CommentViewModel comment) {

            return View(comment);
        }
    }
}
