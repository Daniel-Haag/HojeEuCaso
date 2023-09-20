using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HojeEuCaso.Controllers
{
    public class TendenciasController : Controller
    {
        // GET: TendenciasController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TendenciasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TendenciasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TendenciasController/Create
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

        // GET: TendenciasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TendenciasController/Edit/5
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

        // GET: TendenciasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TendenciasController/Delete/5
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
