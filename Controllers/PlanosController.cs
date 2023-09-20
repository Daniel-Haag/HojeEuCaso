using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HojeEuCaso.Controllers
{
    public class PlanosController : Controller
    {
        // GET: PlanosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PlanosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PlanosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlanosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection) //Alterar para a entidade
        {
            try
            {
                TempData["SuccessMessage"] = "Salvo com sucesso!";
                return View();
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // GET: PlanosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlanosController/Edit/5
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

        // GET: PlanosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlanosController/Delete/5
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
