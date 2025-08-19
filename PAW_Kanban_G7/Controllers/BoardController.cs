using Microsoft.AspNetCore.Mvc;

namespace PAW_Kanban_G7.Controllers
{
    public class BoardController : Controller
    {
        public IActionResult Index(int id = 3) => View(model: id);
    }

}
