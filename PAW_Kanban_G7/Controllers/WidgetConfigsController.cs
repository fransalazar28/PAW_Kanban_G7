using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using K.Models;
using K.Repositories;

namespace PAW_Kanban_G7.Controllers
{
    [Authorize]
    public class WidgetConfigsController : Controller
    {
        private readonly IWidgetConfigRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;

        public WidgetConfigsController(IWidgetConfigRepository repository, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var widgets = await _repository.GetAllAsync(userId);
            return View(widgets);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WidgetConfig widget)
        {
            if (ModelState.IsValid)
            {
                widget.UsuarioId = _userManager.GetUserId(User);
                await _repository.AddAsync(widget);
                return RedirectToAction(nameof(Index));
            }
            return View(widget);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var widget = await _repository.GetByIdAsync(id.Value);
            if (widget == null || widget.UsuarioId != _userManager.GetUserId(User))
                return Unauthorized();

            return View(widget);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WidgetConfig widget)
        {
            if (id != widget.Id) return NotFound();

            if (ModelState.IsValid)
            {
                widget.UsuarioId = _userManager.GetUserId(User);
                await _repository.UpdateAsync(widget);
                return RedirectToAction(nameof(Index));
            }
            return View(widget);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var widget = await _repository.GetByIdAsync(id.Value);
            if (widget == null || widget.UsuarioId != _userManager.GetUserId(User))
                return Unauthorized();

            return View(widget);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
