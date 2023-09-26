using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HojeEuCaso.Controllers
{
    public class AgendaController : Controller
    {
        // GET: AgendaController
        public ActionResult Index(int? month, int? year)
        {
            DateTime currentDate = DateTime.Now;

            if (month.HasValue && year.HasValue)
            {
                currentDate = new DateTime(year.Value, month.Value, 1);

                // Tratar casos onde o mês ou ano excedem os limites
                if (currentDate.Month == 0)
                {
                    currentDate = currentDate.AddYears(-1).AddMonths(12);
                }
                else if (currentDate.Month == 13)
                {
                    currentDate = currentDate.AddYears(1).AddMonths(-12);
                }
            }

            ViewBag.CurrentDate = currentDate;

            return View();
        }

        // GET: AgendaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AgendaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgendaController/Create
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

        // GET: AgendaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AgendaController/Edit/5
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

        // GET: AgendaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AgendaController/Delete/5
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
