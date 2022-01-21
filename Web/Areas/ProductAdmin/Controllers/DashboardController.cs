using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Areas.ProductAdmin.Controllers
{
    [Area("ProductAdmin")]
    public class DashboardController : Controller
    {
        private readonly ProductManager _productManager;
    public DashboardController(ProductManager productManager)
        {
            _productManager = productManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
