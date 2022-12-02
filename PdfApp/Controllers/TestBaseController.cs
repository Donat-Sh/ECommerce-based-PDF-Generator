using Microsoft.AspNetCore.Mvc;

namespace PdfApp.Controllers
{
    public class TestBaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
