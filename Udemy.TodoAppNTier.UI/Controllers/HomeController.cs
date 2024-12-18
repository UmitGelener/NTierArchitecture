using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Business.Interfaces;

namespace Udemy.TodoAppNTier.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkService _workService;

        public HomeController(IWorkService workService)
        {
            _workService = workService;
        }

        public async Task<IActionResult> Index()
        {
            var workList = await _workService.GetAll();
            return View(workList);
        }
    }
}
