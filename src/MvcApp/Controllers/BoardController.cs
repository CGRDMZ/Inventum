using Microsoft.AspNetCore.Mvc;

namespace MvcApp.Controllers
{
    public class BoardController: Controller {
        public IActionResult Index() {
            return View();
        }
    }
}