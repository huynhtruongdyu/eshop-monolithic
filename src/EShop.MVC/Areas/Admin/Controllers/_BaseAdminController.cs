using Microsoft.AspNetCore.Mvc;

namespace EShop.MVC.Areas.Admin.Controllers
{
    [Area(AreaConstant.Admin)]
    [Route("[area]/[controller]/[action]")]
    public abstract class _BaseAdminController : Controller
    {
    }
}