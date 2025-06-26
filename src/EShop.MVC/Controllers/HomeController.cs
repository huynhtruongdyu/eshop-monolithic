using System.Diagnostics;
using EShop.MVC.Models;
using EShop.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace EShop.MVC.Controllers
{
    public class HomeController(ILogger<HomeController> logger, ICatalogService catalogService) : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            var products = await catalogService.GetAllProductsAsync();
            if (!products.Any())
            {
                await catalogService.CreateProductAsync(new Data.Domain.Product
                {
                    Name = "Sample product 1",
                    Price = (decimal)99.0
                });
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}