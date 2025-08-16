using Microsoft.AspNetCore.Mvc;

namespace PAW_Kanban_G7.Controllers
{
    public class BoardController : Controller
    {

        [HttpGet]
        public IActionResult Index(int id = 1) => View(model: id);
    }
}
