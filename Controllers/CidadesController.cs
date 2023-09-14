using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HojeEuCaso.Controllers
{
    public class CidadesController : Controller
    {
        // GET: CidadesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CidadesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CidadesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CidadesController/Create
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

        // GET: CidadesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CidadesController/Edit/5
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

        // GET: CidadesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CidadesController/Delete/5
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
