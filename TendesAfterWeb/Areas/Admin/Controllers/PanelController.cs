using Microsoft.AspNetCore.Mvc;

namespace TendesAfterWeb.Areas.Admin.Controllers
{
    public class PanelController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
