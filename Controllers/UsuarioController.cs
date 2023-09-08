using Microsoft.AspNetCore.Mvc;
using HojeEuCaso.Models;

namespace HojeEuCaso.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
