using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HojeEuCaso.Controllers
{
    public class NovaController : Controller
    {
        // GET: NovaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: NovaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NovaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NovaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NovaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NovaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NovaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NovaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
