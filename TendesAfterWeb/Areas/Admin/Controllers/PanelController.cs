using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TendesAfter.Utility;

namespace TendesAfterWeb.Areas.Admin.Controllers
{
    public class PanelController : Controller
    {
        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
