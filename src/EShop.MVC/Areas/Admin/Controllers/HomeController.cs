using Microsoft.AspNetCore.Mvc;

namespace EShop.MVC.Areas.Admin.Controllers
{
    public class HomeController : _BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}